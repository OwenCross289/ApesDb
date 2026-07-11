using ApesDb.Domain.Entities;
using ApesDb.Igdb.Sdk.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed partial class CatalogStageRunnerTests
{
    [Fact]
    public async Task BootstrapStage_UsesAnUpperOnlySynchronizationWindow()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var service = new FakeIgdbService();
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.Genres]
        );

        await CreateRunner(dbContext, service).RunAsync(runId, IgdbSyncStageKind.Genres, retryCount: 1);

        var request = Assert.Single(service.GenreRequests);
        Assert.Equal(-1, request.AfterId);
        Assert.Equal(new IgdbSyncWindow(null, new DateTimeOffset(SynchronizedAt)), request.Window);
    }

    [Fact]
    public async Task InvolvedCompaniesStage_PersistsValidRowsAndRecordsEveryMissingDependency()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_500;
        const long companyId = 90;
        dbContext.AddRange(
            CreateStoredGame(gameId, "Stored Game"),
            Stamp(new Company { Id = companyId, Name = "Stored Company" })
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            InvolvedCompanies =
            [
                new IgdbInvolvedCompany(100, gameId, companyId, true, false, false, false, null, IgdbUpdatedAt),
                new IgdbInvolvedCompany(101, 32501, 404, false, true, false, false, null, IgdbUpdatedAt),
                new IgdbInvolvedCompany(102, gameId, 405, false, false, false, true, null, IgdbUpdatedAt),
            ],
        };
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.InvolvedCompanies]
        );

        await CreateRunner(dbContext, service).RunAsync(runId, IgdbSyncStageKind.InvolvedCompanies, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var persisted = await dbContext.GameCompanies.AsNoTracking().SingleAsync();
        Assert.Equal(100, persisted.Id);
        Assert.Equal(gameId, persisted.GameId);
        Assert.Equal(companyId, persisted.CompanyId);

        var stage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        var run = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        Assert.Equal(IgdbSyncStageStatus.Succeeded, stage.Status);
        Assert.Equal(102, stage.PageCursor);
        Assert.Equal(1, stage.PagesProcessed);
        Assert.Equal(3, stage.RowsProcessed);
        Assert.Equal(2, stage.RowsSkipped);
        Assert.Equal(3, run.RowsProcessed);
        Assert.Equal(2, run.RowsSkipped);

        var skippedRows = (await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync())
            .OrderBy(value => value.EntityId)
            .ThenBy(value => value.Reason)
            .ToArray();
        Assert.Collection(
            skippedRows,
            value => AssertSkipped(value, stage.Id, 101, IgdbSyncSkipReason.MissingGame, 32501),
            value => AssertSkipped(value, stage.Id, 101, IgdbSyncSkipReason.MissingCompany, 404),
            value => AssertSkipped(value, stage.Id, 102, IgdbSyncSkipReason.MissingCompany, 405)
        );
    }

    [Fact]
    public async Task ExternalGamesStage_SkipsOrphansAndPreservesValidUrlOnlyStoreRows()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_500;
        dbContext.AddRange(
            CreateStoredGame(gameId, "Stored Game"),
            Stamp(new ExternalGameSource { Id = 1, Name = "Steam" }),
            Stamp(new ExternalGameSource { Id = 31, Name = "Xbox Marketplace" }),
            Stamp(new ExternalGameSource { Id = 36, Name = "Playstation Store US" }),
            Stamp(new Platform { Id = 6, Name = "PC" })
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            ExternalGames =
            [
                ExternalGame(100, gameId, 1, 6, null, "https://store.steampowered.com/app/100"),
                ExternalGame(101, gameId, 36, null, null, "https://store.playstation.com/product/PPSA100"),
                ExternalGame(102, gameId, 31, null, null, "https://www.xbox.com/games/store/game/XBOX100"),
                ExternalGame(4662, 32501, 1, null, "32501", "https://store.steampowered.com/app/32501"),
                ExternalGame(4663, gameId, 999, null, null, "https://store.example/missing-source"),
                ExternalGame(4664, gameId, 1, 999, null, "https://store.example/missing-platform"),
                ExternalGame(4665, 32502, 998, 997, null, "https://store.example/all-missing"),
            ],
        };
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.ExternalGames]
        );

        await CreateRunner(dbContext, service).RunAsync(runId, IgdbSyncStageKind.ExternalGames, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var externalGames = await dbContext.ExternalGames.AsNoTracking().OrderBy(value => value.Id).ToArrayAsync();
        Assert.Equal([100L, 101L, 102L], externalGames.Select(value => value.Id).ToArray());
        Assert.All(externalGames, value => Assert.Null(value.ExternalId));
        Assert.All(externalGames, value => Assert.False(string.IsNullOrWhiteSpace(value.Url)));

        var stage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        var run = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        Assert.Equal(IgdbSyncStageStatus.Succeeded, stage.Status);
        Assert.Equal(4665, stage.PageCursor);
        Assert.Equal(7, stage.RowsProcessed);
        Assert.Equal(4, stage.RowsSkipped);
        Assert.Equal(4, run.RowsSkipped);

        var skippedRows = (await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync())
            .OrderBy(value => value.EntityId)
            .ThenBy(value => value.Reason)
            .ToArray();
        Assert.Collection(
            skippedRows,
            value => AssertSkipped(value, stage.Id, 4662, IgdbSyncSkipReason.MissingGame, 32501),
            value => AssertSkipped(value, stage.Id, 4663, IgdbSyncSkipReason.MissingExternalGameSource, 999),
            value => AssertSkipped(value, stage.Id, 4664, IgdbSyncSkipReason.MissingPlatform, 999),
            value => AssertSkipped(value, stage.Id, 4665, IgdbSyncSkipReason.MissingGame, 32502),
            value => AssertSkipped(value, stage.Id, 4665, IgdbSyncSkipReason.MissingExternalGameSource, 998),
            value => AssertSkipped(value, stage.Id, 4665, IgdbSyncSkipReason.MissingPlatform, 997)
        );
    }

    [Fact]
    public async Task ExternalGamesStage_RetryPreservesCommittedCursorAndDoesNotDuplicateSkippedRows()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_500;
        dbContext.AddRange(
            CreateStoredGame(gameId, "Stored Game"),
            Stamp(new ExternalGameSource { Id = 1, Name = "Steam" })
        );
        await dbContext.SaveChangesAsync();

        var firstPage = Enumerable
            .Range(1, 500)
            .Select(id =>
                ExternalGame(id, id == 1 ? 32501 : gameId, 1, null, null, $"https://store.steampowered.com/app/{id}")
            )
            .ToArray();
        var secondPageOrphan = ExternalGame(501, 32502, 1, null, null, "https://store.steampowered.com/app/501");
        var invalidSecondPageRow = new IgdbExternalGame(
            502,
            gameId,
            1,
            null,
            null,
            "Invalid year",
            "https://store.steampowered.com/app/502",
            10000,
            null,
            IgdbUpdatedAt
        );
        var service = new FakeIgdbService { ExternalGames = [.. firstPage, secondPageOrphan, invalidSecondPageRow] };
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.ExternalGames]
        );
        var runner = CreateRunner(dbContext, service);

        await Assert.ThrowsAnyAsync<Exception>(() =>
            runner.RunAsync(runId, IgdbSyncStageKind.ExternalGames, retryCount: 1)
        );

        dbContext.ChangeTracker.Clear();
        var failedStage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        var failedRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        Assert.Equal(IgdbSyncStageStatus.Failed, failedStage.Status);
        Assert.Equal(500, failedStage.PageCursor);
        Assert.Equal(1, failedStage.PagesProcessed);
        Assert.Equal(500, failedStage.RowsProcessed);
        Assert.Equal(1, failedStage.RowsSkipped);
        Assert.Equal(1, failedRun.RowsSkipped);
        Assert.Equal(499, await dbContext.ExternalGames.CountAsync());
        var firstSkippedRow = await dbContext.IgdbSyncSkippedRows.AsNoTracking().SingleAsync();
        AssertSkipped(firstSkippedRow, failedStage.Id, 1, IgdbSyncSkipReason.MissingGame, 32501);

        service.ExternalGames =
        [
            .. firstPage,
            secondPageOrphan,
            ExternalGame(502, gameId, 1, null, null, "https://store.steampowered.com/app/502"),
        ];
        await runner.RunAsync(runId, IgdbSyncStageKind.ExternalGames, retryCount: 2);
        await runner.RunAsync(runId, IgdbSyncStageKind.ExternalGames, retryCount: 3);

        dbContext.ChangeTracker.Clear();
        var succeededStage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        var succeededRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        Assert.Equal(IgdbSyncStageStatus.Succeeded, succeededStage.Status);
        Assert.Equal(502, succeededStage.PageCursor);
        Assert.Equal(2, succeededStage.PagesProcessed);
        Assert.Equal(502, succeededStage.RowsProcessed);
        Assert.Equal(2, succeededStage.RowsSkipped);
        Assert.Equal(2, succeededRun.RowsSkipped);
        Assert.Equal(500, await dbContext.ExternalGames.CountAsync());
        Assert.Equal(2, await dbContext.IgdbSyncSkippedRows.CountAsync());
        var skippedEntityIds = await dbContext
            .IgdbSyncSkippedRows.AsNoTracking()
            .OrderBy(value => value.EntityId)
            .Select(value => value.EntityId)
            .ToArrayAsync();
        Assert.Equal([1L, 501L], skippedEntityIds);
    }

    private static IgdbExternalGame ExternalGame(
        long id,
        long gameId,
        long sourceId,
        long? platformId,
        string? uid,
        string url
    )
    {
        return new IgdbExternalGame(
            id,
            gameId,
            sourceId,
            platformId,
            uid,
            "Store Game",
            url,
            2026,
            null,
            IgdbUpdatedAt
        );
    }

    private static void AssertSkipped(
        IgdbSyncSkippedRow actual,
        Guid stageId,
        long entityId,
        IgdbSyncSkipReason reason,
        long missingDependencyId
    )
    {
        Assert.Equal(stageId, actual.StageId);
        Assert.Equal(entityId, actual.EntityId);
        Assert.Equal(reason, actual.Reason);
        Assert.Equal(missingDependencyId, actual.MissingDependencyId);
        Assert.Equal(SynchronizedAt, actual.CreatedAt);
    }
}
