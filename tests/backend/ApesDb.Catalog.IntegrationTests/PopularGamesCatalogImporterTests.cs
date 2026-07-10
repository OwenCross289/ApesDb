using System.Text.Json;
using ApesDb.Api.Features.Games;
using ApesDb.Api.Features.Games.Genres;
using ApesDb.Api.Features.Games.ListGames;
using ApesDb.Api.Features.Games.Statuses;
using ApesDb.Api.Features.Games.TopGames;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using ApesDb.Igdb.Sdk.Models;
using ApesDb.Worker.Games;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Testcontainers.PostgreSql;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Catalog.IntegrationTests;

public sealed class PopularGamesCatalogImporterTests : IClassFixture<CatalogDatabaseFixture>
{
    private readonly CatalogDatabaseFixture _database;

    public PopularGamesCatalogImporterTests(CatalogDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task ImportAsync_PersistsAndReconcilesTheCompleteCatalogGraph()
    {
        await _database.ResetAsync();
        var synchronizedAt = new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc);
        await using var dbContext = _database.CreateDbContext();
        var importer = CreateImporter(dbContext, synchronizedAt);

        await importer.ImportAsync(CreateCatalog(100, "Original Game", includeGraph: true));

        var game = await dbContext.Games.AsNoTracking().SingleAsync(value => value.IgdbId == 100);
        Assert.Equal("Original Game", game.Name);
        Assert.Equal("https://images.igdb.com/igdb/image/upload/t_cover_small_2x/cover100.jpg", game.CoverSmallUrl);
        Assert.Equal("https://images.igdb.com/igdb/image/upload/t_cover_big_2x/cover100.jpg", game.CoverLargeUrl);
        Assert.Equal(2, await dbContext.Games.CountAsync());
        Assert.Equal(0.123456789012345678m, await dbContext.PopularGames.Select(value => value.Score).SingleAsync());
        Assert.Equal(1, await dbContext.GameRelations.CountAsync());
        Assert.Equal("Split screen", await dbContext.GameModes.Select(value => value.Name).SingleAsync());
        Assert.Equal(1, await dbContext.GameGameModes.CountAsync());
        Assert.Equal(1, await dbContext.GameGenres.CountAsync());
        Assert.Equal(1, await dbContext.GameThemes.CountAsync());
        Assert.Equal(1, await dbContext.GamePlayerPerspectives.CountAsync());
        Assert.Equal(1, await dbContext.GamePlatforms.CountAsync());
        Assert.Equal(1, await dbContext.PlatformLinks.CountAsync());
        Assert.Equal(2, await dbContext.GameExternalIdentifiers.CountAsync());
        Assert.Contains(
            "12345",
            await dbContext.GameExternalIdentifiers.Select(value => value.ExternalId).ToArrayAsync()
        );
        Assert.Contains(
            "PPSA01234",
            await dbContext.GameExternalIdentifiers.Select(value => value.ExternalId).ToArrayAsync()
        );
        Assert.True(await dbContext.GameCompanies.AnyAsync(value => value.Developer));
        Assert.Equal(1, await dbContext.GameCollections.CountAsync());
        Assert.Equal(1, await dbContext.GameFranchises.CountAsync());

        await importer.ImportAsync(CreateCatalog(100, "Updated Game", includeGraph: false));

        var updatedGame = await dbContext.Games.AsNoTracking().SingleAsync(value => value.IgdbId == 100);
        Assert.Equal(game.Id, updatedGame.Id);
        Assert.Equal("Updated Game", updatedGame.Name);
        Assert.Equal(2, await dbContext.Games.CountAsync());
        Assert.Empty(await dbContext.GameRelations.ToArrayAsync());
        Assert.Empty(await dbContext.GameGameModes.ToArrayAsync());
        Assert.Empty(await dbContext.GameGenres.ToArrayAsync());
        Assert.Empty(await dbContext.GameThemes.ToArrayAsync());
        Assert.Empty(await dbContext.GamePlayerPerspectives.ToArrayAsync());
        Assert.Empty(await dbContext.GameExternalIdentifiers.ToArrayAsync());
        Assert.Empty(await dbContext.GameCompanies.ToArrayAsync());
        Assert.Empty(await dbContext.GameCollections.ToArrayAsync());
        Assert.Empty(await dbContext.GameFranchises.ToArrayAsync());
        Assert.Equal(synchronizedAt, updatedGame.LastSyncedAt);
    }

    [Fact]
    public async Task ImportAsync_KeepsUnrankedGamesAndRollsBackAnInvalidGraph()
    {
        await _database.ResetAsync();
        var synchronizedAt = new DateTime(2026, 7, 9, 13, 0, 0, DateTimeKind.Utc);
        await using var dbContext = _database.CreateDbContext();
        var importer = CreateImporter(dbContext, synchronizedAt);

        await importer.ImportAsync(CreateCatalog(200, "First Ranked Game", includeGraph: false));
        await importer.ImportAsync(CreateCatalog(300, "Second Ranked Game", includeGraph: false));

        Assert.Equal(2, await dbContext.Games.CountAsync());
        var currentPopularIgdbId = await dbContext.PopularGames.Select(value => value.Game.IgdbId).SingleAsync();
        Assert.Equal(300, currentPopularIgdbId);

        var invalidCatalog = CreateCatalog(300, "Uncommitted Name", includeGraph: false) with
        {
            Relations = [new IgdbGameRelation(300, 999, IgdbGameRelationType.Dlc)],
        };

        await Assert.ThrowsAsync<InvalidDataException>(() => importer.ImportAsync(invalidCatalog));

        dbContext.ChangeTracker.Clear();
        Assert.Equal(
            "Second Ranked Game",
            await dbContext.Games.Where(value => value.IgdbId == 300).Select(value => value.Name).SingleAsync()
        );
        Assert.Equal(300, await dbContext.PopularGames.Select(value => value.Game.IgdbId).SingleAsync());
    }

    [Fact]
    public async Task TopGamesEndpoint_ReturnsTheDatabaseBackedRanking()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var importer = CreateImporter(dbContext, new DateTime(2026, 7, 9, 14, 0, 0, DateTimeKind.Utc));
        await importer.ImportAsync(CreateCatalog(400, "Database Ranked Game", includeGraph: false));

        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<TopGamesEndpoint>(httpContext, dbContext);

        await endpoint.HandleAsync(CancellationToken.None);

        responseBody.Position = 0;
        var response = await JsonSerializer.DeserializeAsync<TopGameResponse[]>(
            responseBody,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
        );
        var game = Assert.Single(response!);
        Assert.Equal(400, game.Id);
        Assert.Equal("Database Ranked Game", game.Name);
        Assert.Equal("https://images.igdb.com/igdb/image/upload/t_cover_small_2x/cover400.jpg", game.CoverSmallUrl);
    }

    [Fact]
    public async Task ListGamesEndpoint_FiltersJoinedValuesAndReturnsTheListProjection()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var synchronizedAt = new DateTime(2026, 7, 9, 15, 0, 0, DateTimeKind.Utc);
        var importer = CreateImporter(dbContext, synchronizedAt);
        await importer.ImportAsync(CreateCatalog(500, "Filterable Game", includeGraph: true));

        var gameId = await dbContext.Games.Where(value => value.IgdbId == 500).Select(value => value.Id).SingleAsync();
        var company = await dbContext.GameCompanies.SingleAsync();
        company.Publisher = true;
        var coopMode = new GameMode
        {
            IgdbId = 3,
            Name = "Co-operative",
            Slug = "co-operative",
            CreatedAt = synchronizedAt,
            UpdatedAt = synchronizedAt,
            LastSyncedAt = synchronizedAt,
        };
        dbContext.GameModes.Add(coopMode);
        dbContext.GameGameModes.Add(new GameGameMode { GameId = gameId, GameMode = coopMode });
        await dbContext.SaveChangesAsync();

        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<ListGamesEndpoint>(httpContext, dbContext);
        var request = new ListGamesRequest
        {
            GameTypeIds = [1],
            GameStatusIds = [0],
            GenreIds = [999, 10],
            ThemeIds = [20],
            GameModeIds = [3],
            PlayerPerspectiveIds = [40],
            PlatformIds = [50],
            Developer = "VELOP",
            Publisher = "velop",
            Collection = "series",
            Franchise = "franchise",
            Search = "FILTERABLE",
            IsCoop = true,
            IsSteam = true,
            GameKinds = ["main"],
        };

        await endpoint.HandleAsync(request, CancellationToken.None);

        responseBody.Position = 0;
        var response = await JsonSerializer.DeserializeAsync<ListGamesResponse>(
            responseBody,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
        );
        var game = Assert.Single(response!.Items);
        Assert.Equal(500, game.Id);
        Assert.Equal("Filterable Game", game.Name);
        Assert.Equal(GameKind.Main, game.Kind);
        Assert.True(game.IsCoop);
        Assert.True(game.IsSteam);
        Assert.Equal(["Developer"], game.Developers);
        Assert.Equal(["Developer"], game.Publishers);
        Assert.Equal(1, response.TotalCount);
        Assert.Equal(1, response.Page);
        Assert.Equal(50, response.PageSize);
    }

    [Fact]
    public async Task ListGamesEndpoint_ClassifiesAChildFromTheIncomingRelation()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var importer = CreateImporter(dbContext, new DateTime(2026, 7, 9, 16, 0, 0, DateTimeKind.Utc));
        await importer.ImportAsync(CreateCatalog(600, "Parent Game", includeGraph: true));

        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<ListGamesEndpoint>(httpContext, dbContext);

        await endpoint.HandleAsync(new ListGamesRequest { GameKinds = ["dlc"], PageSize = 10 }, CancellationToken.None);

        responseBody.Position = 0;
        var response = await JsonSerializer.DeserializeAsync<ListGamesResponse>(
            responseBody,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
        );
        var game = Assert.Single(response!.Items);
        Assert.Equal(601, game.Id);
        Assert.Equal(GameKind.Dlc, game.Kind);
    }

    [Fact]
    public async Task GameLookupEndpoints_CacheSortedResponsesUsingTheNamedCache()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var synchronizedAt = new DateTime(2026, 7, 9, 17, 0, 0, DateTimeKind.Utc);
        var importer = CreateImporter(dbContext, synchronizedAt);
        await importer.ImportAsync(CreateCatalog(700, "Lookup Game", includeGraph: true));

        var services = new ServiceCollection();
        services
            .AddFusionCache(GameLookupCache.CacheName)
            .WithCacheKeyPrefix(GameLookupCache.CacheKeyPrefix)
            .WithDefaultEntryOptions(options => options.SetDuration(GameLookupCache.Expiration));
        await using var serviceProvider = services.BuildServiceProvider();
        var cacheProvider = serviceProvider.GetRequiredService<IFusionCacheProvider>();

        var first = await ListGenresAsync();
        Assert.Equal([new GenreResponse(10, "Adventure")], first);

        dbContext.Genres.Add(
            new Genre
            {
                IgdbId = 11,
                Name = "Action",
                Slug = "action",
                CreatedAt = synchronizedAt,
                UpdatedAt = synchronizedAt,
                LastSyncedAt = synchronizedAt,
            }
        );
        await dbContext.SaveChangesAsync();

        var cached = await ListGenresAsync();
        Assert.Equal(first, cached);

        var cache = cacheProvider.GetCache(GameLookupCache.CacheName);
        await cache.RemoveAsync(GameGenresEndpoint.CacheKey);
        var refreshed = await ListGenresAsync();
        Assert.Equal([new GenreResponse(11, "Action"), new GenreResponse(10, "Adventure")], refreshed);

        var statuses = await ListStatusesAsync();
        Assert.Contains(new GameStatusResponse(0, "Released"), statuses);

        async Task<GenreResponse[]> ListGenresAsync()
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

        async Task<GameStatusResponse[]> ListStatusesAsync()
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
    }

    private static PopularGamesCatalogImporter CreateImporter(ApplicationDbContext dbContext, DateTime synchronizedAt)
    {
        return new PopularGamesCatalogImporter(
            dbContext,
            new FixedDateTimeProvider(synchronizedAt),
            NullLogger<PopularGamesCatalogImporter>.Instance
        );
    }

    private static IgdbPopularCatalog CreateCatalog(long gameId, string gameName, bool includeGraph)
    {
        var updatedAt = new DateTimeOffset(2026, 7, 9, 10, 0, 0, TimeSpan.Zero);
        var mainGameType = new IgdbGameType(1, "Main Game", null, updatedAt);
        var games = new List<IgdbCatalogGame>
        {
            new(
                gameId,
                gameName,
                $"game-{gameId}",
                "Summary",
                "Storyline",
                updatedAt,
                87.5,
                42,
                $"https://www.igdb.com/games/game-{gameId}",
                mainGameType.Id,
                null,
                new IgdbImage(
                    gameId,
                    $"cover{gameId}",
                    264,
                    374,
                    null,
                    $"https://images.igdb.com/igdb/image/upload/t_cover_small_2x/cover{gameId}.jpg",
                    $"https://images.igdb.com/igdb/image/upload/t_cover_big_2x/cover{gameId}.jpg",
                    null
                ),
                null,
                updatedAt
            ),
        };

        if (includeGraph)
        {
            games.Add(
                new IgdbCatalogGame(
                    gameId + 1,
                    "Related DLC",
                    $"game-{gameId}-dlc",
                    null,
                    null,
                    updatedAt,
                    null,
                    null,
                    null,
                    2,
                    null,
                    null,
                    null,
                    updatedAt
                )
            );
        }

        return new IgdbPopularCatalog(
            new IgdbPopularityType(1, "Visits", 121, null, updatedAt),
            mainGameType,
            [new IgdbPopularGame(gameId, 1, 1, gameId, 0.123456789012345678m, updatedAt, null, updatedAt)],
            games,
            includeGraph ? [mainGameType, new IgdbGameType(2, "DLC/Add-on", null, updatedAt)] : [mainGameType],
            [],
            includeGraph ? [new IgdbGameRelation(gameId, gameId + 1, IgdbGameRelationType.Dlc)] : [],
            includeGraph ? [new IgdbGenre(10, "Adventure", "adventure", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGameGenre(gameId, 10)] : [],
            includeGraph ? [new IgdbTheme(20, "Action", "action", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGameTheme(gameId, 20)] : [],
            includeGraph ? [new IgdbGameMode(30, "Split screen", "split-screen", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGameGameMode(gameId, 30)] : [],
            includeGraph ? [new IgdbPlayerPerspective(40, "Third person", "third-person", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGamePlayerPerspective(gameId, 40)] : [],
            includeGraph
                ? [new IgdbPlatform(50, "PC", "PC", null, "win", 5, null, null, null, null, null, updatedAt)]
                : [],
            includeGraph ? [new IgdbPlatformType(5, "Operating system", null, updatedAt)] : [],
            includeGraph ? [new IgdbGamePlatform(gameId, 50)] : [],
            includeGraph ? [new IgdbPlatformLink(60, 50, 1, "https://example.com/pc", true, null)] : [],
            includeGraph ? [new IgdbWebsiteType(1, "Official", null, updatedAt)] : [],
            includeGraph
                ?
                [
                    new IgdbExternalGameSource(70, "Steam", null, updatedAt),
                    new IgdbExternalGameSource(71, "PlayStation Store US", null, updatedAt),
                ]
                : [],
            includeGraph
                ?
                [
                    new IgdbExternalGameIdentifier(
                        80,
                        gameId,
                        70,
                        50,
                        "12345",
                        gameName,
                        "https://store.steampowered.com/app/12345",
                        2026,
                        null,
                        updatedAt
                    ),
                    new IgdbExternalGameIdentifier(
                        81,
                        gameId,
                        71,
                        null,
                        "PPSA01234",
                        gameName,
                        "https://store.playstation.com/product/PPSA01234",
                        2026,
                        null,
                        updatedAt
                    ),
                ]
                : [],
            includeGraph
                ? [new IgdbCompany(90, "Developer", "developer", "A developer", 578, null, null, null, updatedAt)]
                : [],
            includeGraph ? [new IgdbGameCompany(91, gameId, 90, true, false, false, false, null, updatedAt)] : [],
            includeGraph ? [new IgdbCollection(1000, "Game Series", "game-series", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGameCollection(gameId, 1000)] : [],
            includeGraph ? [new IgdbFranchise(1100, "Game Franchise", "game-franchise", null, null, updatedAt)] : [],
            includeGraph ? [new IgdbGameFranchise(gameId, 1100)] : []
        );
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

public sealed class CatalogDatabaseFixture : IAsyncLifetime
{
    private const string DatabaseName = "apesdb";
    private const string DatabaseUsername = "apesdb";
    private const string DatabasePassword = "apesdb";
    private const string PostgresNetworkAlias = "postgres";

    private readonly INetwork _network;
    private readonly PostgreSqlContainer _postgres;
    private readonly IContainer _flyway;

    public CatalogDatabaseFixture()
    {
        _network = new NetworkBuilder().Build();
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:18")
            .WithDatabase(DatabaseName)
            .WithUsername(DatabaseUsername)
            .WithPassword(DatabasePassword)
            .WithNetwork(_network)
            .WithNetworkAliases(PostgresNetworkAlias)
            .Build();
        _flyway = new ContainerBuilder()
            .WithImage("flyway/flyway:11-alpine")
            .WithCommand("migrate")
            .WithEnvironment("FLYWAY_URL", $"jdbc:postgresql://{PostgresNetworkAlias}:5432/{DatabaseName}")
            .WithEnvironment("FLYWAY_USER", DatabaseUsername)
            .WithEnvironment("FLYWAY_PASSWORD", DatabasePassword)
            .WithEnvironment("FLYWAY_CONNECT_RETRIES", "60")
            .WithEnvironment("FLYWAY_BASELINE_ON_MIGRATE", "true")
            .WithEnvironment("FLYWAY_DEFAULT_SCHEMA", "migrations")
            .WithEnvironment("FLYWAY_SCHEMAS", "migrations,public,worker")
            .WithResourceMapping(new DirectoryInfo(ResolveMigrationsDirectory()), "/flyway/sql")
            .WithNetwork(_network)
            .DependsOn(_postgres)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Successfully applied"))
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await _flyway.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _flyway.DisposeAsync();
        await _postgres.DisposeAsync();
        await _network.DisposeAsync();
    }

    public ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .Options;
        return new ApplicationDbContext(options);
    }

    public async Task ResetAsync()
    {
        await using var dbContext = CreateDbContext();
        await dbContext.Database.ExecuteSqlRawAsync(
            """
            TRUNCATE TABLE
                "GameTypes",
                "GameStatuses",
                "Games",
                "Genres",
                "Themes",
                "GameModes",
                "PlayerPerspectives",
                "Platforms",
                "ExternalGameSources",
                "Companies",
                "Collections",
                "Franchises"
            CASCADE;
            """
        );
    }

    private static string ResolveMigrationsDirectory()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var migrationsDirectory = Path.Combine(directory.FullName, "db", "migrations");
            if (Directory.Exists(migrationsDirectory))
            {
                return migrationsDirectory;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not find db/migrations from the test runtime directory.");
    }
}
