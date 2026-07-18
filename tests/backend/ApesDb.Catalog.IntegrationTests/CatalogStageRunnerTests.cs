using System.Text.Json;
using ApesDb.Api.Features.Games;
using ApesDb.Api.Features.Games.Genres;
using ApesDb.Api.Features.Games.ListGames;
using ApesDb.Api.Features.Games.Statuses;
using ApesDb.Api.Features.Games.TopGames;
using ApesDb.Api.Features.Games.Types;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Games;
using ApesDb.Domain.Entities.IgdbSync;
using ApesDb.Igdb.Sdk.Models;
using ApesDb.Worker.Games;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Catalog.IntegrationTests;

public sealed partial class CatalogStageRunnerTests : IClassFixture<CatalogDatabaseFixture>
{
    private static readonly DateTime SynchronizedAt = new(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc);
    private static readonly DateTimeOffset IgdbUpdatedAt = new(2026, 7, 9, 10, 0, 0, TimeSpan.Zero);

    private static readonly IgdbSyncStageKind[] GraphStages =
    [
        IgdbSyncStageKind.GameTypes,
        IgdbSyncStageKind.GameStatuses,
        IgdbSyncStageKind.Genres,
        IgdbSyncStageKind.Themes,
        IgdbSyncStageKind.GameModes,
        IgdbSyncStageKind.PlayerPerspectives,
        IgdbSyncStageKind.PlatformTypes,
        IgdbSyncStageKind.WebsiteTypes,
        IgdbSyncStageKind.ExternalGameSources,
        IgdbSyncStageKind.PopularityTypes,
        IgdbSyncStageKind.Companies,
        IgdbSyncStageKind.Collections,
        IgdbSyncStageKind.Franchises,
        IgdbSyncStageKind.Platforms,
        IgdbSyncStageKind.PlatformLinks,
        IgdbSyncStageKind.Games,
        IgdbSyncStageKind.GameRelations,
        IgdbSyncStageKind.InvolvedCompanies,
        IgdbSyncStageKind.ExternalGames,
    ];

    private readonly CatalogDatabaseFixture _database;

    public CatalogStageRunnerTests(CatalogDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task RunAsync_SynchronizesTheCatalogGraphUsingDirectIgdbKeys()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var service = CreateGraphService();
        var runner = CreateRunner(dbContext, service);
        var runId = await CreateRunAsync(dbContext, IgdbSyncRunMode.Bootstrap, null, SynchronizedAt, GraphStages);

        foreach (var stage in GraphStages)
        {
            await runner.RunAsync(runId, stage, retryCount: 1);
        }

        dbContext.ChangeTracker.Clear();
        var game = await dbContext.Games.AsNoTracking().SingleAsync(value => value.Id == 5_000_000_500);
        Assert.Equal(5_000_000_500, game.Id);
        Assert.Equal("Catalog Game", game.Name);
        Assert.Equal(0, game.GameTypeId);
        Assert.Equal(0, game.GameStatusId);
        Assert.Equal("cover500", game.CoverImageId);
        Assert.Equal("https://images.example/cover500-small.jpg", game.CoverSmallUrl);
        Assert.Equal("https://images.example/cover500-large.jpg", game.CoverLargeUrl);
        Assert.Null(game.VersionParentId);
        Assert.Equal(SynchronizedAt, game.LastSyncedAt);

        var edition = await dbContext.Games.AsNoTracking().SingleAsync(value => value.Id == 5_000_000_502);
        Assert.Equal(game.Id, edition.VersionParentId);

        Assert.Equal(new long[] { 0 }, await dbContext.GameStatuses.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(
            new long[] { 0, 1 },
            await dbContext.GameTypes.OrderBy(value => value.Id).Select(value => value.Id).ToArrayAsync()
        );
        Assert.Equal(new long[] { 10 }, await dbContext.Genres.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 20 }, await dbContext.Themes.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 30 }, await dbContext.GameModes.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 40 }, await dbContext.PlayerPerspectives.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 5 }, await dbContext.PlatformTypes.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 6 }, await dbContext.WebsiteTypes.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 7 }, await dbContext.PopularityTypes.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(
            121,
            await dbContext.PopularityTypes.Select(value => value.ExternalPopularitySourceId).SingleAsync()
        );
        Assert.Equal(
            new long[] { 1, 31, 36 },
            await dbContext.ExternalGameSources.OrderBy(value => value.Id).Select(value => value.Id).ToArrayAsync()
        );
        Assert.Equal(new long[] { 90 }, await dbContext.Companies.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 1_000 }, await dbContext.Collections.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 1_100 }, await dbContext.Franchises.Select(value => value.Id).ToArrayAsync());
        Assert.Equal(new long[] { 50 }, await dbContext.Platforms.Select(value => value.Id).ToArrayAsync());

        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 10) },
            await dbContext
                .GameGenres.Select(value => new ValueTuple<long, long>(value.GameId, value.GenreId))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 20) },
            await dbContext
                .GameThemes.Select(value => new ValueTuple<long, long>(value.GameId, value.ThemeId))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 30) },
            await dbContext
                .GameGameModes.Select(value => new ValueTuple<long, long>(value.GameId, value.GameModeId))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 40) },
            await dbContext
                .GamePlayerPerspectives.Select(value => new ValueTuple<long, long>(
                    value.GameId,
                    value.PlayerPerspectiveId
                ))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 50) },
            await dbContext
                .GamePlatforms.Select(value => new ValueTuple<long, long>(value.GameId, value.PlatformId))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 1_000) },
            await dbContext
                .GameCollections.Select(value => new ValueTuple<long, long>(value.GameId, value.CollectionId))
                .ToArrayAsync()
        );
        Assert.Equal(
            new (long, long)[] { (5_000_000_500, 1_100) },
            await dbContext
                .GameFranchises.Select(value => new ValueTuple<long, long>(value.GameId, value.FranchiseId))
                .ToArrayAsync()
        );

        var relation = await dbContext.GameRelations.AsNoTracking().SingleAsync();
        Assert.Equal(5_000_000_500, relation.GameId);
        Assert.Equal(5_000_000_501, relation.RelatedGameId);
        Assert.Equal(GameRelationType.Dlc, relation.RelationType);
        Assert.Empty(await dbContext.IgdbSyncTouchedRelationParents.ToArrayAsync());
        Assert.Empty(await dbContext.IgdbSyncPendingGameRelations.ToArrayAsync());

        var company = await dbContext.GameCompanies.AsNoTracking().SingleAsync();
        Assert.Equal(91, company.Id);
        Assert.Equal(5_000_000_500, company.GameId);
        Assert.Equal(90, company.CompanyId);
        Assert.True(company.Developer);
        Assert.True(company.Publisher);

        var platformLink = await dbContext.PlatformLinks.AsNoTracking().SingleAsync();
        Assert.Equal(60, platformLink.Id);
        Assert.Equal(50, platformLink.PlatformId);
        Assert.Equal(6, platformLink.WebsiteTypeId);

        var externalGames = await dbContext.ExternalGames.AsNoTracking().OrderBy(value => value.Id).ToArrayAsync();
        Assert.Collection(
            externalGames,
            value =>
            {
                Assert.Equal(80, value.Id);
                Assert.Equal(1, value.ExternalGameSourceId);
                Assert.Equal(50, value.PlatformId);
                Assert.Null(value.ExternalId);
                Assert.Equal("https://store.steampowered.com/app/500", value.Url);
            },
            value =>
            {
                Assert.Equal(81, value.Id);
                Assert.Equal(36, value.ExternalGameSourceId);
                Assert.Null(value.PlatformId);
                Assert.Null(value.ExternalId);
                Assert.Equal("https://store.playstation.com/product/PPSA500", value.Url);
            },
            value =>
            {
                Assert.Equal(82, value.Id);
                Assert.Equal(31, value.ExternalGameSourceId);
                Assert.Equal("XBOX500", value.ExternalId);
                Assert.Equal("https://www.xbox.com/games/store/catalog-game/XBOX500", value.Url);
            }
        );

        var stages = await dbContext.IgdbSyncStages.AsNoTracking().Where(value => value.RunId == runId).ToArrayAsync();
        Assert.Equal(GraphStages.Length, stages.Length);
        Assert.All(stages, stage => Assert.Equal(IgdbSyncStageStatus.Succeeded, stage.Status));
    }

    [Fact]
    public async Task InvolvedCompaniesStage_PreservesDistinctIgdbRowsAndUpsertsRolesIndependently()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_500;
        const long companyId = 90;
        dbContext.AddRange(
            CreateStoredGame(gameId, "Shared Company Game"),
            Stamp(new Company { Id = companyId, Name = "Shared Company" })
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            InvolvedCompanies =
            [
                new IgdbInvolvedCompany(91, gameId, companyId, true, false, false, false, null, IgdbUpdatedAt),
                new IgdbInvolvedCompany(92, gameId, companyId, false, true, false, false, null, IgdbUpdatedAt),
            ],
        };
        var runner = CreateRunner(dbContext, service);
        var firstRunId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.InvolvedCompanies]
        );

        await runner.RunAsync(firstRunId, IgdbSyncStageKind.InvolvedCompanies, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var firstRows = await dbContext.GameCompanies.AsNoTracking().OrderBy(value => value.Id).ToArrayAsync();
        Assert.Collection(
            firstRows,
            value =>
            {
                Assert.Equal(91, value.Id);
                Assert.Equal(gameId, value.GameId);
                Assert.Equal(companyId, value.CompanyId);
                Assert.True(value.Developer);
                Assert.False(value.Publisher);
            },
            value =>
            {
                Assert.Equal(92, value.Id);
                Assert.Equal(gameId, value.GameId);
                Assert.Equal(companyId, value.CompanyId);
                Assert.False(value.Developer);
                Assert.True(value.Publisher);
            }
        );
        await CompleteRunAsync(dbContext, firstRunId);

        service.InvolvedCompanies =
        [
            new IgdbInvolvedCompany(91, gameId, companyId, true, true, false, false, null, IgdbUpdatedAt),
            new IgdbInvolvedCompany(92, gameId, companyId, true, true, false, false, null, IgdbUpdatedAt),
        ];
        var secondRunId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            SynchronizedAt.AddDays(-1),
            SynchronizedAt,
            [IgdbSyncStageKind.InvolvedCompanies]
        );

        await runner.RunAsync(secondRunId, IgdbSyncStageKind.InvolvedCompanies, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var updatedRows = await dbContext.GameCompanies.AsNoTracking().OrderBy(value => value.Id).ToArrayAsync();
        Assert.Equal([91L, 92L], updatedRows.Select(value => value.Id).ToArray());
        Assert.All(updatedRows, value => Assert.True(value.Developer));
        Assert.All(updatedRows, value => Assert.True(value.Publisher));
        await CompleteRunAsync(dbContext, secondRunId);

        var repeatedRunId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            SynchronizedAt.AddDays(-1),
            SynchronizedAt,
            [IgdbSyncStageKind.InvolvedCompanies]
        );

        await runner.RunAsync(repeatedRunId, IgdbSyncStageKind.InvolvedCompanies, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        Assert.Equal(2, await dbContext.GameCompanies.CountAsync());
        var response = await InvokeListGamesAsync(dbContext, new ListGamesRequest());
        var item = Assert.Single(response.Items);
        Assert.Equal(["Shared Company"], item.Developers);
        Assert.Equal(["Shared Company"], item.Publishers);
    }

    [Fact]
    public async Task RunAsync_RebuildsPlatformLinksUsingAnUnwindowedFullScan()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        dbContext.AddRange(
            Stamp(new PlatformType { Id = 5, Name = "Operating system" }),
            Stamp(new WebsiteType { Id = 6, Name = "Official" }),
            Stamp(
                new Platform
                {
                    Id = 50,
                    Name = "PC",
                    PlatformTypeId = 5,
                }
            ),
            Stamp(
                new PlatformLink
                {
                    Id = 60,
                    PlatformId = 50,
                    WebsiteTypeId = 6,
                    Url = "https://stale.example/pc",
                    Trusted = false,
                }
            )
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            Platforms =
            [
                new IgdbPlatform(50, "PC", "PC", null, "win", 5, null, null, null, null, [61], null, IgdbUpdatedAt),
            ],
            PlatformWebsites = [new IgdbPlatformWebsite(61, 6, "https://current.example/pc", true, null)],
        };
        var from = new DateTime(2026, 7, 8, 12, 0, 0, DateTimeKind.Utc);
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            from,
            SynchronizedAt,
            [IgdbSyncStageKind.PlatformLinks]
        );

        await CreateRunner(dbContext, service).RunAsync(runId, IgdbSyncStageKind.PlatformLinks, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var link = await dbContext.PlatformLinks.AsNoTracking().SingleAsync();
        Assert.Equal(61, link.Id);
        Assert.Equal("https://current.example/pc", link.Url);
        Assert.True(link.Trusted);
        Assert.All(service.PlatformRequests, request => Assert.Null(request.Window));
    }

    [Fact]
    public async Task RunAsync_CommitsPageProgressAndResumesTheSameIncrementalWindow()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var from = new DateTime(2026, 7, 8, 8, 30, 0, DateTimeKind.Utc);
        var through = new DateTime(2026, 7, 9, 8, 30, 0, DateTimeKind.Utc);
        var service = new FakeIgdbService();
        var requestNumber = 0;
        service.GenresPageHandler = (afterId, _, _) =>
        {
            requestNumber++;
            if (requestNumber == 1)
            {
                IReadOnlyList<IgdbGenre> page = Enumerable
                    .Range(0, 500)
                    .Select(id => new IgdbGenre(id, $"Genre {id}", $"genre-{id}", null, null, IgdbUpdatedAt))
                    .ToArray();
                return Task.FromResult(page);
            }

            if (requestNumber == 2)
            {
                throw new HttpRequestException("IGDB was temporarily unavailable.");
            }

            Assert.Equal(499, afterId);
            IReadOnlyList<IgdbGenre> finalPage =
            [
                new IgdbGenre(500, "Genre 500", "genre-500", null, null, IgdbUpdatedAt),
            ];
            return Task.FromResult(finalPage);
        };
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Incremental,
            from,
            through,
            [IgdbSyncStageKind.Genres]
        );
        var runner = CreateRunner(dbContext, service);

        await Assert.ThrowsAsync<HttpRequestException>(() =>
            runner.RunAsync(runId, IgdbSyncStageKind.Genres, retryCount: 1)
        );

        dbContext.ChangeTracker.Clear();
        var failedStage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        Assert.Equal(IgdbSyncStageStatus.Failed, failedStage.Status);
        Assert.Equal(499, failedStage.PageCursor);
        Assert.Equal(1, failedStage.PagesProcessed);
        Assert.Equal(500, failedStage.RowsProcessed);
        Assert.Equal(500, await dbContext.Genres.CountAsync());

        await runner.RunAsync(runId, IgdbSyncStageKind.Genres, retryCount: 2);

        dbContext.ChangeTracker.Clear();
        var succeededStage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.RunId == runId);
        Assert.Equal(IgdbSyncStageStatus.Succeeded, succeededStage.Status);
        Assert.Equal(500, succeededStage.PageCursor);
        Assert.Equal(2, succeededStage.PagesProcessed);
        Assert.Equal(501, succeededStage.RowsProcessed);
        Assert.Equal(501, await dbContext.Genres.CountAsync());
        Assert.Equal([-1L, 499L, 499L], service.GenreRequests.Select(value => value.AfterId).ToArray());

        var expectedWindow = new IgdbSyncWindow(new DateTimeOffset(from), new DateTimeOffset(through));
        Assert.All(service.GenreRequests, request => Assert.Equal(expectedWindow, request.Window));
    }

    [Fact]
    public async Task PopularityStage_ReplacesTheSnapshotAndTopGamesKeepsLongWireIds()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long firstGameId = 5_000_000_100;
        const long secondGameId = 5_000_000_200;
        const long staleGameId = 5_000_000_300;
        dbContext.AddRange(
            Stamp(new PopularityType { Id = 7, Name = "Visits" }),
            CreateStoredGame(firstGameId, "First ranked game"),
            CreateStoredGame(secondGameId, "Second ranked game"),
            CreateStoredGame(staleGameId, "Stale ranked game")
        );
        dbContext.PopularGames.Add(
            new PopularGame
            {
                Id = 999,
                GameId = staleGameId,
                Rank = 1,
                SourceRank = 1,
                Score = 0.1m,
                PopularityTypeId = 7,
                CalculatedAt = SynchronizedAt,
                SyncedAt = SynchronizedAt,
            }
        );
        await dbContext.SaveChangesAsync();

        var service = new FakeIgdbService
        {
            PopularityPrimitives =
            [
                new IgdbPopularityPrimitive(1, 99_999, 0.95m, 7, null, IgdbUpdatedAt, null, IgdbUpdatedAt),
                new IgdbPopularityPrimitive(2, firstGameId, 0.90m, 7, null, IgdbUpdatedAt, null, IgdbUpdatedAt),
                new IgdbPopularityPrimitive(3, firstGameId, 0.80m, 7, null, IgdbUpdatedAt, null, IgdbUpdatedAt),
                new IgdbPopularityPrimitive(4, secondGameId, 0.70m, 7, null, IgdbUpdatedAt, null, IgdbUpdatedAt),
            ],
        };
        var runId = await CreateRunAsync(
            dbContext,
            IgdbSyncRunMode.Bootstrap,
            null,
            SynchronizedAt,
            [IgdbSyncStageKind.Popularity]
        );

        await CreateRunner(dbContext, service).RunAsync(runId, IgdbSyncStageKind.Popularity, retryCount: 1);

        dbContext.ChangeTracker.Clear();
        var snapshot = await dbContext.PopularGames.AsNoTracking().OrderBy(value => value.Rank).ToArrayAsync();
        Assert.Equal([2L, 4L], snapshot.Select(value => value.Id).ToArray());
        Assert.Equal([firstGameId, secondGameId], snapshot.Select(value => value.GameId).ToArray());
        Assert.Equal([1, 2], snapshot.Select(value => value.Rank).ToArray());
        Assert.Equal([2, 4], snapshot.Select(value => value.SourceRank).ToArray());
        Assert.Equal([0.90m, 0.70m], snapshot.Select(value => value.Score).ToArray());

        var response = await InvokeTopGamesAsync(dbContext);
        Assert.Equal([firstGameId, secondGameId], response.Select(value => value.Id).ToArray());
        Assert.Equal([1, 2], response.Select(value => value.Rank).ToArray());
        Assert.Equal("First ranked game", response[0].Name);
    }

    [Fact]
    public async Task ListGamesEndpoint_PreservesFiltersAndUsesStableSteamSourceId()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long gameId = 5_000_000_500;
        const long nameOnlySteamGameId = 5_000_000_501;
        var gameType = Stamp(new GameType { Id = 0, Name = "Main Game" });
        var gameStatus = Stamp(new GameStatus { Id = 9, Name = "Early Access" });
        var genre = Stamp(
            new Genre
            {
                Id = 10,
                Name = "Adventure",
                Slug = "adventure",
            }
        );
        var theme = Stamp(
            new Theme
            {
                Id = 20,
                Name = "Action",
                Slug = "action",
            }
        );
        var mode = Stamp(
            new GameMode
            {
                Id = 30,
                Name = "Co-operative",
                Slug = "co-operative",
            }
        );
        var perspective = Stamp(
            new PlayerPerspective
            {
                Id = 40,
                Name = "Third person",
                Slug = "third-person",
            }
        );
        var platform = Stamp(new Platform { Id = 50, Name = "PC" });
        var company = Stamp(new Company { Id = 90, Name = "Developer Publisher" });
        var collection = Stamp(new Collection { Id = 1_000, Name = "Game Series" });
        var franchise = Stamp(new Franchise { Id = 1_100, Name = "Game Franchise" });
        var stableSteamSource = Stamp(new ExternalGameSource { Id = 1, Name = "Renamed Valve Store" });
        var nameOnlySteamSource = Stamp(new ExternalGameSource { Id = 70, Name = "Steam" });
        var game = CreateStoredGame(gameId, "Filterable Game");
        game.GameTypeId = gameType.Id;
        game.GameStatusId = gameStatus.Id;
        game.CoverSmallUrl = "https://images.example/filterable-small.jpg";
        game.CoverLargeUrl = "https://images.example/filterable-large.jpg";
        var nameOnlySteamGame = CreateStoredGame(nameOnlySteamGameId, "Name-only Steam Game");
        nameOnlySteamGame.GameTypeId = gameType.Id;
        nameOnlySteamGame.GameStatusId = gameStatus.Id;

        dbContext.AddRange(
            gameType,
            gameStatus,
            genre,
            theme,
            mode,
            perspective,
            platform,
            company,
            collection,
            franchise,
            stableSteamSource,
            nameOnlySteamSource,
            game,
            nameOnlySteamGame
        );
        dbContext.AddRange(
            new GameGenre { GameId = gameId, GenreId = genre.Id },
            new GameTheme { GameId = gameId, ThemeId = theme.Id },
            new GameGameMode { GameId = gameId, GameModeId = mode.Id },
            new GamePlayerPerspective { GameId = gameId, PlayerPerspectiveId = perspective.Id },
            new GamePlatform { GameId = gameId, PlatformId = platform.Id },
            new GameCollection { GameId = gameId, CollectionId = collection.Id },
            new GameFranchise { GameId = gameId, FranchiseId = franchise.Id },
            Stamp(
                new GameCompany
                {
                    Id = 91,
                    GameId = gameId,
                    CompanyId = company.Id,
                    Developer = true,
                    Publisher = true,
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 80,
                    GameId = gameId,
                    ExternalGameSourceId = stableSteamSource.Id,
                    ExternalId = null,
                    Url = "https://store.steampowered.com/app/500",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 81,
                    GameId = nameOnlySteamGameId,
                    ExternalGameSourceId = nameOnlySteamSource.Id,
                    ExternalId = "501",
                    Url = "https://not-steam-by-id.example/501",
                }
            )
        );
        await dbContext.SaveChangesAsync();

        var commandInterceptor = new CountingDbCommandInterceptor();
        await using var endpointDbContext = _database.CreateDbContext(commandInterceptor);
        var response = await InvokeListGamesAsync(
            endpointDbContext,
            new ListGamesRequest
            {
                GameTypeIds = [0],
                GameStatusIds = [9],
                GenreIds = [999, 10],
                ThemeIds = [20],
                GameModeIds = [30],
                PlayerPerspectiveIds = [40],
                PlatformIds = [50],
                Developer = "VELOPER",
                Publisher = "publisher",
                Collection = "series",
                Franchise = "franchise",
                Search = "FILTERABLE",
                IsCoop = true,
                IsSteam = true,
            }
        );

        var item = Assert.Single(response.Items);
        Assert.Equal(gameId, item.Id);
        Assert.Equal("https://images.example/filterable-small.jpg", item.CoverSmallUrl);
        Assert.Equal("https://images.example/filterable-large.jpg", item.CoverLargeUrl);
        Assert.Equal("Filterable Game", item.Name);
        Assert.Equal(["Developer Publisher"], item.Developers);
        Assert.Equal(["Developer Publisher"], item.Publishers);
        Assert.True(item.IsCoop);
        Assert.True(item.IsSteam);
        Assert.Equal(new GameTypeResponse(gameType.Id, gameType.Name), item.GameType);
        Assert.Equal(2, response.Total);
        Assert.Equal(1, response.FilteredTotal);
        Assert.Equal(1, response.Page);
        Assert.Equal(50, response.PageSize);
        Assert.Equal(3, commandInterceptor.CommandCount);

        var pageCommandInterceptor = new CountingDbCommandInterceptor();
        await using var pageDbContext = _database.CreateDbContext(pageCommandInterceptor);
        var pageResponse = await InvokeListGamesAsync(pageDbContext, new ListGamesRequest { Page = 2, PageSize = 1 });
        var pageItem = Assert.Single(pageResponse.Items);
        Assert.Equal(nameOnlySteamGameId, pageItem.Id);
        Assert.Empty(pageItem.Developers);
        Assert.Empty(pageItem.Publishers);
        Assert.Equal(new GameTypeResponse(gameType.Id, gameType.Name), pageItem.GameType);
        Assert.Equal(2, pageResponse.Total);
        Assert.Equal(2, pageResponse.FilteredTotal);
        Assert.Equal(3, pageCommandInterceptor.CommandCount);

        var steamByStableId = await InvokeListGamesAsync(dbContext, new ListGamesRequest { IsSteam = true });
        Assert.Equal([gameId], steamByStableId.Items.Select(value => value.Id).ToArray());
        var notSteamByStableId = await InvokeListGamesAsync(dbContext, new ListGamesRequest { IsSteam = false });
        Assert.Equal([nameOnlySteamGameId], notSteamByStableId.Items.Select(value => value.Id).ToArray());
    }

    [Fact]
    public async Task ListGamesEndpoint_TreatsTextAsLiteralSubstringAndEmptyIdListsAsAbsent()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var gameStatus = Stamp(new GameStatus { Id = 9, Name = "Early Access" });
        var literalGame = CreateStoredGame(5_000_000_600, @"Literal %_\* Match");
        var statusGame = CreateStoredGame(5_000_000_601, "Other Game");
        statusGame.GameStatusId = gameStatus.Id;
        dbContext.AddRange(gameStatus, literalGame, statusGame);
        await dbContext.SaveChangesAsync();

        var literalResponse = await InvokeListGamesAsync(
            dbContext,
            new ListGamesRequest
            {
                GameTypeIds = [],
                GameStatusIds = [],
                GenreIds = [],
                ThemeIds = [],
                GameModeIds = [],
                PlayerPerspectiveIds = [],
                PlatformIds = [],
                Search = @"%_\*",
            }
        );

        var literalItem = Assert.Single(literalResponse.Items);
        Assert.Equal(literalGame.Id, literalItem.Id);
        Assert.Null(literalItem.GameType);
        Assert.Equal(2, literalResponse.Total);
        Assert.Equal(1, literalResponse.FilteredTotal);

        var releasedResponse = await InvokeListGamesAsync(dbContext, new ListGamesRequest { GameStatusIds = [0] });
        Assert.Equal(literalGame.Id, Assert.Single(releasedResponse.Items).Id);
        Assert.Equal(1, releasedResponse.FilteredTotal);
    }

    [Fact]
    public async Task ListGamesEndpoint_AlwaysExcludesEditionsButKeepsOtherGameTypes()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var mainGameType = Stamp(new GameType { Id = 0, Name = "Main Game" });
        var dlcGameType = Stamp(new GameType { Id = 1, Name = "DLC" });
        var expansionGameType = Stamp(new GameType { Id = 2, Name = "Expansion" });
        var standaloneExpansionGameType = Stamp(new GameType { Id = 4, Name = "Standalone Expansion" });
        var baseGame = CreateStoredGame(5_000_000_700, "Base Game");
        baseGame.GameTypeId = mainGameType.Id;
        var edition = CreateStoredGame(5_000_000_701, "Base Game - Gold Edition");
        edition.GameTypeId = mainGameType.Id;
        edition.VersionParentId = baseGame.Id;
        var dlc = CreateStoredGame(5_000_000_702, "DLC");
        dlc.GameTypeId = dlcGameType.Id;
        var expansion = CreateStoredGame(5_000_000_703, "Expansion");
        expansion.GameTypeId = expansionGameType.Id;
        var standaloneExpansion = CreateStoredGame(5_000_000_704, "Standalone Expansion");
        standaloneExpansion.GameTypeId = standaloneExpansionGameType.Id;
        dbContext.AddRange(
            mainGameType,
            dlcGameType,
            expansionGameType,
            standaloneExpansionGameType,
            baseGame,
            edition,
            dlc,
            expansion,
            standaloneExpansion
        );
        await dbContext.SaveChangesAsync();

        var all = await InvokeListGamesAsync(dbContext, new ListGamesRequest());
        Assert.Equal(
            [baseGame.Id, dlc.Id, expansion.Id, standaloneExpansion.Id],
            all.Items.Select(value => value.Id).ToArray()
        );
        Assert.Equal(4, all.Total);
        Assert.Equal(4, all.FilteredTotal);

        var additionalContent = await InvokeListGamesAsync(dbContext, new ListGamesRequest { GameTypeIds = [1, 2, 4] });
        Assert.Equal(
            [dlc.Id, expansion.Id, standaloneExpansion.Id],
            additionalContent.Items.Select(value => value.Id).ToArray()
        );
        Assert.Equal(4, additionalContent.Total);
        Assert.Equal(3, additionalContent.FilteredTotal);

        var editionSearch = await InvokeListGamesAsync(dbContext, new ListGamesRequest { Search = "Gold Edition" });
        Assert.Empty(editionSearch.Items);
        Assert.Equal(4, editionSearch.Total);
        Assert.Equal(0, editionSearch.FilteredTotal);
    }

    [Fact]
    public async Task LookupEndpoints_CacheSortedResponsesWithDirectLongIds()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        const long adventureId = 5_000_000_010;
        const long actionId = 5_000_000_011;
        dbContext.Genres.Add(Stamp(new Genre { Id = adventureId, Name = "Adventure" }));
        await dbContext.SaveChangesAsync();

        var services = new ServiceCollection();
        services
            .AddFusionCache(GameCache.CacheName)
            .WithCacheKeyPrefix(GameCache.CacheKeyPrefix)
            .WithDefaultEntryOptions(options => options.SetDuration(GameCache.Expiration));
        await using var serviceProvider = services.BuildServiceProvider();
        var cacheProvider = serviceProvider.GetRequiredService<IFusionCacheProvider>();

        var first = await InvokeGenresAsync(dbContext, cacheProvider);
        Assert.Equal([new GenreResponse(adventureId, "Adventure")], first);

        dbContext.Genres.Add(Stamp(new Genre { Id = actionId, Name = "Action" }));
        await dbContext.SaveChangesAsync();
        Assert.Equal(first, await InvokeGenresAsync(dbContext, cacheProvider));

        await cacheProvider.GetCache(GameCache.CacheName).RemoveAsync(GameGenresEndpoint.CacheKey);
        var refreshed = await InvokeGenresAsync(dbContext, cacheProvider);
        Assert.Equal([new GenreResponse(actionId, "Action"), new GenreResponse(adventureId, "Adventure")], refreshed);

        var statuses = await InvokeStatusesAsync(dbContext, cacheProvider);
        Assert.Equal([new GameStatusResponse(0, "Released")], statuses);
    }

    private static CatalogStageRunner CreateRunner(ApplicationDbContext dbContext, FakeIgdbService service)
    {
        var clock = new FixedDateTimeProvider(SynchronizedAt);
        var popularitySynchronizer = new PopularitySynchronizer(
            dbContext,
            service,
            clock,
            NullLogger<PopularitySynchronizer>.Instance
        );
        return new CatalogStageRunner(
            dbContext,
            service,
            popularitySynchronizer,
            clock,
            NullLogger<CatalogStageRunner>.Instance
        );
    }

    private static async Task<Guid> CreateRunAsync(
        ApplicationDbContext dbContext,
        IgdbSyncRunMode mode,
        DateTime? from,
        DateTime through,
        IReadOnlyList<IgdbSyncStageKind> stageKinds
    )
    {
        var run = new IgdbSyncRun
        {
            Mode = mode,
            Status = IgdbSyncRunStatus.Pending,
            From = from,
            Through = through,
            CreatedAt = SynchronizedAt,
            UpdatedAt = SynchronizedAt,
        };
        dbContext.IgdbSyncRuns.Add(run);
        for (var index = 0; index < stageKinds.Count; index++)
        {
            dbContext.IgdbSyncStages.Add(
                new IgdbSyncStage
                {
                    Run = run,
                    Kind = stageKinds[index],
                    Order = index,
                    Status = IgdbSyncStageStatus.Pending,
                    PageCursor = -1,
                    CreatedAt = SynchronizedAt,
                    UpdatedAt = SynchronizedAt,
                }
            );
        }

        await dbContext.SaveChangesAsync();
        return run.Id;
    }

    private static async Task CompleteRunAsync(ApplicationDbContext dbContext, Guid runId)
    {
        var run = await dbContext.IgdbSyncRuns.SingleAsync(value => value.Id == runId);
        run.Status = IgdbSyncRunStatus.Succeeded;
        run.CompletedAt = SynchronizedAt;
        run.UpdatedAt = SynchronizedAt;
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();
    }

    private static FakeIgdbService CreateGraphService()
    {
        const long gameId = 5_000_000_500;
        return new FakeIgdbService
        {
            GameTypes =
            [
                new IgdbGameType(0, "Main Game", null, IgdbUpdatedAt),
                new IgdbGameType(1, "DLC", null, IgdbUpdatedAt),
            ],
            GameStatuses = [new IgdbGameStatus(0, "Released", null, IgdbUpdatedAt)],
            Genres = [new IgdbGenre(10, "Adventure", "adventure", null, null, IgdbUpdatedAt)],
            Themes = [new IgdbTheme(20, "Action", "action", null, null, IgdbUpdatedAt)],
            GameModes = [new IgdbGameMode(30, "Co-operative", "co-operative", null, null, IgdbUpdatedAt)],
            PlayerPerspectives =
            [
                new IgdbPlayerPerspective(40, "Third person", "third-person", null, null, IgdbUpdatedAt),
            ],
            PlatformTypes = [new IgdbPlatformType(5, "Operating system", null, IgdbUpdatedAt)],
            WebsiteTypes = [new IgdbWebsiteType(6, "Official", null, IgdbUpdatedAt)],
            PopularityTypes = [new IgdbPopularityType(7, "Visits", 121, null, IgdbUpdatedAt)],
            ExternalGameSources =
            [
                new IgdbExternalGameSource(1, "Steam", null, IgdbUpdatedAt),
                new IgdbExternalGameSource(31, "Xbox Marketplace", null, IgdbUpdatedAt),
                new IgdbExternalGameSource(36, "Playstation Store US", null, IgdbUpdatedAt),
            ],
            Companies =
            [
                new IgdbCompany(90, "Developer", "developer", "A developer", 578, null, null, null, IgdbUpdatedAt),
            ],
            Collections = [new IgdbCollection(1_000, "Game Series", "game-series", null, null, IgdbUpdatedAt)],
            Franchises = [new IgdbFranchise(1_100, "Game Franchise", "game-franchise", null, null, IgdbUpdatedAt)],
            Platforms =
            [
                new IgdbPlatform(
                    50,
                    "PC",
                    "PC",
                    null,
                    "win",
                    5,
                    null,
                    "Personal computer",
                    null,
                    null,
                    [60],
                    null,
                    IgdbUpdatedAt
                ),
            ],
            PlatformWebsites = [new IgdbPlatformWebsite(60, 6, "https://example.com/pc", true, null)],
            Games =
            [
                CreateIgdbGame(
                    gameId,
                    "Catalog Game",
                    gameTypeId: 0,
                    dlcIds: [gameId + 1],
                    genreIds: [10],
                    themeIds: [20],
                    gameModeIds: [30],
                    perspectiveIds: [40],
                    platformIds: [50],
                    collectionIds: [1_000],
                    franchiseId: 1_100,
                    franchiseIds: [1_100],
                    cover: new IgdbImage(
                        500,
                        "cover500",
                        264,
                        374,
                        null,
                        "https://images.example/cover500-small.jpg",
                        "https://images.example/cover500-large.jpg",
                        null
                    )
                ),
                CreateIgdbGame(gameId + 1, "Catalog Game DLC", gameTypeId: 1),
                CreateIgdbGame(
                    gameId + 2,
                    "Catalog Game - Collector's Edition",
                    gameTypeId: 0,
                    versionParentId: gameId
                ),
            ],
            InvolvedCompanies =
            [
                new IgdbInvolvedCompany(91, gameId, 90, true, true, false, false, null, IgdbUpdatedAt),
            ],
            ExternalGames =
            [
                new IgdbExternalGame(
                    80,
                    gameId,
                    1,
                    50,
                    null,
                    "Catalog Game",
                    "https://store.steampowered.com/app/500",
                    2026,
                    null,
                    IgdbUpdatedAt
                ),
                new IgdbExternalGame(
                    81,
                    gameId,
                    36,
                    null,
                    null,
                    "Catalog Game",
                    "https://store.playstation.com/product/PPSA500",
                    2026,
                    null,
                    IgdbUpdatedAt
                ),
                new IgdbExternalGame(
                    82,
                    gameId,
                    31,
                    null,
                    "XBOX500",
                    "Catalog Game",
                    "https://www.xbox.com/games/store/catalog-game/XBOX500",
                    2026,
                    null,
                    IgdbUpdatedAt
                ),
            ],
        };
    }

    private static IgdbGame CreateIgdbGame(
        long id,
        string name,
        long gameTypeId,
        IReadOnlyList<long>? dlcIds = null,
        IReadOnlyList<long>? genreIds = null,
        IReadOnlyList<long>? themeIds = null,
        IReadOnlyList<long>? gameModeIds = null,
        IReadOnlyList<long>? perspectiveIds = null,
        IReadOnlyList<long>? platformIds = null,
        IReadOnlyList<long>? collectionIds = null,
        long? franchiseId = null,
        IReadOnlyList<long>? franchiseIds = null,
        IgdbImage? cover = null,
        long? versionParentId = null
    )
    {
        return new IgdbGame(
            id,
            name,
            $"game-{id}",
            "Summary",
            "Storyline",
            87.5,
            42,
            IgdbUpdatedAt,
            $"https://www.igdb.com/games/game-{id}",
            gameTypeId,
            0,
            versionParentId,
            cover,
            dlcIds ?? [],
            [],
            [],
            genreIds ?? [],
            themeIds ?? [],
            gameModeIds ?? [],
            perspectiveIds ?? [],
            platformIds ?? [],
            collectionIds ?? [],
            franchiseId,
            franchiseIds ?? [],
            null,
            IgdbUpdatedAt
        );
    }

    private static Game CreateStoredGame(long id, string name)
    {
        return Stamp(
            new Game
            {
                Id = id,
                Name = name,
                Slug = $"game-{id}",
                Summary = "Summary",
                FirstReleaseDate = IgdbUpdatedAt.UtcDateTime,
                TotalRating = 87.5m,
                CoverImageId = $"cover-{id}",
                CoverSmallUrl = $"https://images.example/{id}-small.jpg",
                CoverLargeUrl = $"https://images.example/{id}-large.jpg",
            }
        );
    }

    private static T Stamp<T>(T entity)
        where T : class, IIgdbEntity
    {
        entity.CreatedAt = SynchronizedAt;
        entity.UpdatedAt = SynchronizedAt;
        entity.LastSyncedAt = SynchronizedAt;
        return entity;
    }

    private static async Task<Pagable<ListGameResponse>> InvokeListGamesAsync(
        ApplicationDbContext dbContext,
        ListGamesRequest request
    )
    {
        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<ListGamesEndpoint>(httpContext, dbContext);
        await endpoint.HandleAsync(request, CancellationToken.None);
        responseBody.Position = 0;
        return (
            await JsonSerializer.DeserializeAsync<Pagable<ListGameResponse>>(
                responseBody,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        )!;
    }

    private static async Task<TopGameResponse[]> InvokeTopGamesAsync(ApplicationDbContext dbContext)
    {
        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<TopGamesEndpoint>(httpContext, dbContext);
        await endpoint.HandleAsync(CancellationToken.None);
        responseBody.Position = 0;
        return (
            await JsonSerializer.DeserializeAsync<TopGameResponse[]>(
                responseBody,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        )!;
    }

    private static async Task<GenreResponse[]> InvokeGenresAsync(
        ApplicationDbContext dbContext,
        IFusionCacheProvider cacheProvider
    )
    {
        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<GameGenresEndpoint>(httpContext, dbContext, cacheProvider);
        await endpoint.HandleAsync(CancellationToken.None);
        responseBody.Position = 0;
        return (
            await JsonSerializer.DeserializeAsync<GenreResponse[]>(
                responseBody,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        )!;
    }

    private static async Task<GameStatusResponse[]> InvokeStatusesAsync(
        ApplicationDbContext dbContext,
        IFusionCacheProvider cacheProvider
    )
    {
        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<GameStatusesEndpoint>(httpContext, dbContext, cacheProvider);
        await endpoint.HandleAsync(CancellationToken.None);
        responseBody.Position = 0;
        return (
            await JsonSerializer.DeserializeAsync<GameStatusResponse[]>(
                responseBody,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        )!;
    }

    private sealed class FixedDateTimeProvider : IDateTimeProvider
    {
        public FixedDateTimeProvider(DateTime utcNow)
        {
            UtcNow = utcNow;
        }

        public DateTime Now => UtcNow.ToLocalTime();

        public DateTime UtcNow { get; }

        public DateTimeOffset OffsetNow => new(Now);

        public DateTimeOffset OffsetUtcNow => new(UtcNow);
    }
}
