using ApesDb.Domain.Entities;
using ApesDb.Igdb.Sdk.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed partial class CatalogStageRunnerTests
{
    [Fact]
    public async Task BootstrapUsesCreatedAtSoExistingParentsAreAvailableBeforeOlderDependents()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_699;
        const long companyId = 89;
        const long involvedCompanyId = 189;
        const long externalGameId = 289;
        var bootstrapThrough = SynchronizedAt;
        var gameCreatedAt = new DateTimeOffset(bootstrapThrough.AddDays(-30));
        var gameUpdatedAt = new DateTimeOffset(bootstrapThrough.AddMinutes(1));
        var dependentCreatedAt = new DateTimeOffset(bootstrapThrough.AddDays(-20));
        var dependentUpdatedAt = new DateTimeOffset(bootstrapThrough.AddMinutes(-1));
        var catchUpThrough = bootstrapThrough.AddMinutes(10);
        var stages = new[]
        {
            IgdbSyncStageKind.Games,
            IgdbSyncStageKind.InvolvedCompanies,
            IgdbSyncStageKind.ExternalGames,
        };

        dbContext.AddRange(
            Stamp(new Company { Id = companyId, Name = "Existing Company" }),
            Stamp(new ExternalGameSource { Id = 1, Name = "Steam" })
        );
        await dbContext.SaveChangesAsync();

        var game = CreateWindowedGame(gameId, gameUpdatedAt);
        var service = new FakeIgdbService
        {
            Games = [game],
            InvolvedCompanies =
            [
                new IgdbInvolvedCompany(
                    involvedCompanyId,
                    gameId,
                    companyId,
                    true,
                    false,
                    false,
                    false,
                    null,
                    dependentUpdatedAt
                ),
            ],
            ExternalGames =
            [
                new IgdbExternalGame(
                    externalGameId,
                    gameId,
                    1,
                    null,
                    null,
                    "Existing Game",
                    "https://store.steampowered.com/app/5000000699",
                    2026,
                    null,
                    dependentUpdatedAt
                ),
            ],
        };
        service.GameCreatedAt[gameId] = gameCreatedAt;
        service.InvolvedCompanyCreatedAt[involvedCompanyId] = dependentCreatedAt;
        service.ExternalGameCreatedAt[externalGameId] = dependentCreatedAt;
        var runner = CreateRunner(dbContext, service);
        var bootstrapRunId = await CreateRunAsync(dbContext, IgdbSyncRunMode.Bootstrap, null, bootstrapThrough, stages);

        foreach (var stage in stages)
        {
            await runner.RunAsync(bootstrapRunId, stage, retryCount: 1);
        }

        dbContext.ChangeTracker.Clear();
        Assert.True(await dbContext.Games.AnyAsync(value => value.Id == gameId));
        Assert.True(await dbContext.GameCompanies.AnyAsync(value => value.Id == involvedCompanyId));
        Assert.True(await dbContext.ExternalGames.AnyAsync(value => value.Id == externalGameId));
        Assert.Empty(await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync());
        var bootstrapRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == bootstrapRunId);
        Assert.Equal(3, bootstrapRun.RowsProcessed);
        Assert.Equal(0, bootstrapRun.RowsSkipped);
        await CompleteRunAsync(dbContext, bootstrapRunId);

        service.Games = [game with { Name = "Existing Game Re-upserted" }];
        var catchUpRunId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            bootstrapThrough,
            catchUpThrough,
            stages
        );
        foreach (var stage in stages)
        {
            await runner.RunAsync(catchUpRunId, stage, retryCount: 1);
        }

        dbContext.ChangeTracker.Clear();
        Assert.Equal(
            "Existing Game Re-upserted",
            await dbContext.Games.Where(value => value.Id == gameId).Select(value => value.Name).SingleAsync()
        );
        Assert.True(await dbContext.GameCompanies.AnyAsync(value => value.Id == involvedCompanyId));
        Assert.True(await dbContext.ExternalGames.AnyAsync(value => value.Id == externalGameId));
        Assert.Empty(await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync());

        var catchUpRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == catchUpRunId);
        Assert.Equal(1, catchUpRun.RowsProcessed);
        Assert.Equal(0, catchUpRun.RowsSkipped);
        var catchUpStages = await dbContext
            .IgdbSyncStages.AsNoTracking()
            .Where(value => value.RunId == catchUpRunId)
            .ToDictionaryAsync(value => value.Kind);
        Assert.Equal(1, catchUpStages[IgdbSyncStageKind.Games].RowsProcessed);
        Assert.Equal(0, catchUpStages[IgdbSyncStageKind.InvolvedCompanies].RowsProcessed);
        Assert.Equal(0, catchUpStages[IgdbSyncStageKind.ExternalGames].RowsProcessed);
        Assert.All(catchUpStages.Values, stage => Assert.Equal(IgdbSyncStageStatus.Succeeded, stage.Status));

        var bootstrapWindow = new IgdbSyncWindow(null, new DateTimeOffset(bootstrapThrough));
        var catchUpWindow = new IgdbSyncWindow(
            new DateTimeOffset(bootstrapThrough),
            new DateTimeOffset(catchUpThrough)
        );
        AssertBootstrapThenCatchUpWindows(service.GameRequests, bootstrapWindow, catchUpWindow);
        AssertBootstrapThenCatchUpWindows(service.InvolvedCompanyRequests, bootstrapWindow, catchUpWindow);
        AssertBootstrapThenCatchUpWindows(service.ExternalGameRequests, bootstrapWindow, catchUpWindow);
    }

    [Fact]
    public async Task BootstrapDefersNewParentAndDependentsUntilOrderedIncrementalCatchUp()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_700;
        const long companyId = 90;
        const long involvedCompanyId = 190;
        const long externalGameId = 290;
        var bootstrapThrough = SynchronizedAt;
        var rowUpdatedAt = new DateTimeOffset(bootstrapThrough.AddSeconds(1));
        var catchUpThrough = bootstrapThrough.AddMinutes(10);
        var stages = new[]
        {
            IgdbSyncStageKind.Games,
            IgdbSyncStageKind.InvolvedCompanies,
            IgdbSyncStageKind.ExternalGames,
        };

        dbContext.AddRange(
            Stamp(new Company { Id = companyId, Name = "Catch-up Company" }),
            Stamp(new ExternalGameSource { Id = 1, Name = "Steam" })
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            Games = [CreateWindowedGame(gameId, rowUpdatedAt)],
            InvolvedCompanies =
            [
                new IgdbInvolvedCompany(
                    involvedCompanyId,
                    gameId,
                    companyId,
                    true,
                    false,
                    false,
                    false,
                    null,
                    rowUpdatedAt
                ),
            ],
            ExternalGames =
            [
                new IgdbExternalGame(
                    externalGameId,
                    gameId,
                    1,
                    null,
                    null,
                    "Catch-up Game",
                    "https://store.steampowered.com/app/5000000700",
                    2026,
                    null,
                    rowUpdatedAt
                ),
            ],
        };
        var runner = CreateRunner(dbContext, service);
        var bootstrapRunId = await CreateRunAsync(dbContext, IgdbSyncRunMode.Bootstrap, null, bootstrapThrough, stages);

        foreach (var stage in stages)
        {
            await runner.RunAsync(bootstrapRunId, stage, retryCount: 1);
        }

        dbContext.ChangeTracker.Clear();
        Assert.False(await dbContext.Games.AnyAsync(value => value.Id == gameId));
        Assert.False(await dbContext.GameCompanies.AnyAsync(value => value.Id == involvedCompanyId));
        Assert.False(await dbContext.ExternalGames.AnyAsync(value => value.Id == externalGameId));
        Assert.Empty(await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync());
        await CompleteRunAsync(dbContext, bootstrapRunId);

        var catchUpRunId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            bootstrapThrough,
            catchUpThrough,
            stages
        );
        foreach (var stage in stages)
        {
            await runner.RunAsync(catchUpRunId, stage, retryCount: 1);
        }

        dbContext.ChangeTracker.Clear();
        Assert.True(await dbContext.Games.AnyAsync(value => value.Id == gameId));
        Assert.True(await dbContext.GameCompanies.AnyAsync(value => value.Id == involvedCompanyId));
        Assert.True(await dbContext.ExternalGames.AnyAsync(value => value.Id == externalGameId));
        Assert.Empty(await dbContext.IgdbSyncSkippedRows.AsNoTracking().ToArrayAsync());
        var catchUpRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == catchUpRunId);
        Assert.Equal(3, catchUpRun.RowsProcessed);
        Assert.Equal(0, catchUpRun.RowsSkipped);
        Assert.All(
            await dbContext.IgdbSyncStages.AsNoTracking().Where(value => value.RunId == catchUpRunId).ToArrayAsync(),
            stage =>
            {
                Assert.Equal(IgdbSyncStageStatus.Succeeded, stage.Status);
                Assert.Equal(0, stage.RowsSkipped);
            }
        );

        var bootstrapWindow = new IgdbSyncWindow(null, new DateTimeOffset(bootstrapThrough));
        var catchUpWindow = new IgdbSyncWindow(
            new DateTimeOffset(bootstrapThrough),
            new DateTimeOffset(catchUpThrough)
        );
        AssertBootstrapThenCatchUpWindows(service.GameRequests, bootstrapWindow, catchUpWindow);
        AssertBootstrapThenCatchUpWindows(service.InvolvedCompanyRequests, bootstrapWindow, catchUpWindow);
        AssertBootstrapThenCatchUpWindows(service.ExternalGameRequests, bootstrapWindow, catchUpWindow);
    }

    private static IgdbGame CreateWindowedGame(long id, DateTimeOffset updatedAt)
    {
        return new IgdbGame(
            Id: id,
            Name: "Catch-up Game",
            Slug: "catch-up-game",
            Summary: "A game created just after the bootstrap watermark.",
            Storyline: null,
            TotalRating: null,
            TotalRatingCount: null,
            FirstReleaseDate: null,
            Url: "https://www.igdb.com/games/catch-up-game",
            GameTypeId: null,
            GameStatusId: null,
            Cover: null,
            DlcIds: [],
            ExpansionIds: [],
            StandaloneExpansionIds: [],
            GenreIds: [],
            ThemeIds: [],
            GameModeIds: [],
            PlayerPerspectiveIds: [],
            PlatformIds: [],
            CollectionIds: [],
            FranchiseId: null,
            FranchiseIds: [],
            Checksum: null,
            UpdatedAt: updatedAt
        );
    }

    private static void AssertBootstrapThenCatchUpWindows(
        IReadOnlyList<FakeIgdbService.PageRequest> requests,
        IgdbSyncWindow bootstrapWindow,
        IgdbSyncWindow catchUpWindow
    )
    {
        Assert.Collection(
            requests,
            request =>
            {
                Assert.Equal(-1, request.AfterId);
                Assert.Equal(bootstrapWindow, request.Window);
            },
            request =>
            {
                Assert.Equal(-1, request.AfterId);
                Assert.Equal(catchUpWindow, request.Window);
            }
        );
    }
}
