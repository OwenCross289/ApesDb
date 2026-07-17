using System.Text.Json;
using ApesDb.Api.Features.Games;
using ApesDb.Api.Features.Games.GetGameById;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Catalog.IntegrationTests;

public sealed class GetGameByIdEndpointTests : IClassFixture<CatalogDatabaseFixture>
{
    private static readonly DateTime SynchronizedAt = new(2026, 7, 17, 12, 0, 0, DateTimeKind.Utc);

    private readonly CatalogDatabaseFixture _database;

    public GetGameByIdEndpointTests(CatalogDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task GetGameByIdEndpoint_ReturnsAllStoredCatalogData()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();

        var gameType = Stamp(new GameType { Id = 0, Name = "Main Game" });
        var gameStatus = Stamp(new GameStatus { Id = 5, Name = "Early Access" });
        var popularityType = Stamp(new PopularityType { Id = 1, Name = "Visits" });
        var genre = Stamp(new Genre { Id = 10, Name = "Adventure" });
        var theme = Stamp(new Theme { Id = 11, Name = "Action" });
        var gameMode = Stamp(new GameMode { Id = 12, Name = "Co-operative" });
        var perspective = Stamp(new PlayerPerspective { Id = 13, Name = "Third person" });
        var platform = Stamp(new Platform { Id = 14, Name = "PC" });
        var collection = Stamp(new Collection { Id = 15, Name = "Example Collection" });
        var franchise = Stamp(new Franchise { Id = 16, Name = "Example Franchise" });
        var storeSource = Stamp(new ExternalGameSource { Id = 1, Name = "Steam" });
        var urlOnlyStoreSource = Stamp(new ExternalGameSource { Id = 31, Name = "Xbox Marketplace" });
        var multiRoleCompany = Stamp(new Company { Id = 20, Name = "Multi-role Studio" });
        var supportingCompany = Stamp(new Company { Id = 21, Name = "Supporting Studio" });

        const long gameId = 5_000_001_000;
        var game = Stamp(
            new Game
            {
                Id = gameId,
                Name = "Complete Game",
                Slug = "complete-game",
                Summary = "Complete description",
                Storyline = "Complete storyline",
                FirstReleaseDate = SynchronizedAt,
                TotalRating = 87.5m,
                TotalRatingCount = 42,
                IgdbUrl = "https://www.igdb.com/games/complete-game",
                GameTypeId = gameType.Id,
                GameStatusId = gameStatus.Id,
                VersionParentId = 5_000_000_999,
                CoverImageId = "cover-complete",
                CoverWidth = 1080,
                CoverHeight = 1440,
                CoverSmallUrl = "https://images.example/complete-small.jpg",
                CoverLargeUrl = "https://images.example/complete-large.jpg",
            }
        );
        var dlc = CreateAddon(5_000_001_001, "Alpha DLC");
        var expansion = CreateAddon(5_000_001_002, "Beta Expansion");
        var standaloneExpansion = CreateAddon(5_000_001_003, "Gamma Standalone Expansion");

        dbContext.AddRange(
            gameType,
            gameStatus,
            popularityType,
            genre,
            theme,
            gameMode,
            perspective,
            platform,
            collection,
            franchise,
            storeSource,
            urlOnlyStoreSource,
            multiRoleCompany,
            supportingCompany,
            game,
            dlc,
            expansion,
            standaloneExpansion
        );
        dbContext.AddRange(
            new PopularGame
            {
                Id = 100,
                GameId = gameId,
                Rank = 12,
                SourceRank = 15,
                Score = 0.9125m,
                PopularityTypeId = popularityType.Id,
                CalculatedAt = SynchronizedAt,
                SyncedAt = SynchronizedAt,
            },
            new GameGenre { GameId = gameId, GenreId = genre.Id },
            new GameTheme { GameId = gameId, ThemeId = theme.Id },
            new GameGameMode { GameId = gameId, GameModeId = gameMode.Id },
            new GamePlayerPerspective { GameId = gameId, PlayerPerspectiveId = perspective.Id },
            new GamePlatform { GameId = gameId, PlatformId = platform.Id },
            new GameCollection { GameId = gameId, CollectionId = collection.Id },
            new GameFranchise { GameId = gameId, FranchiseId = franchise.Id },
            Stamp(
                new GameCompany
                {
                    Id = 200,
                    GameId = gameId,
                    CompanyId = multiRoleCompany.Id,
                    Developer = true,
                    Publisher = true,
                    Porting = true,
                }
            ),
            Stamp(
                new GameCompany
                {
                    Id = 201,
                    GameId = gameId,
                    CompanyId = multiRoleCompany.Id,
                    Developer = true,
                }
            ),
            Stamp(
                new GameCompany
                {
                    Id = 202,
                    GameId = gameId,
                    CompanyId = supportingCompany.Id,
                    Supporting = true,
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 300,
                    GameId = gameId,
                    ExternalGameSourceId = storeSource.Id,
                    PlatformId = platform.Id,
                    ExternalId = "123456",
                    Name = "Complete Game",
                    Url = "https://store.steampowered.com/app/123456",
                    Year = 2026,
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 301,
                    GameId = gameId,
                    ExternalGameSourceId = urlOnlyStoreSource.Id,
                    Url = "https://www.xbox.com/games/store/complete-game",
                }
            ),
            CreateRelation(gameId, dlc.Id, GameRelationType.Dlc),
            CreateRelation(gameId, expansion.Id, GameRelationType.Expansion),
            CreateRelation(gameId, standaloneExpansion.Id, GameRelationType.StandaloneExpansion)
        );
        await dbContext.SaveChangesAsync();

        var commandInterceptor = new CountingDbCommandInterceptor();
        await using var endpointDbContext = _database.CreateDbContext(commandInterceptor);
        await using var cacheServices = CreateCacheServices();
        var cacheProvider = cacheServices.GetRequiredService<IFusionCacheProvider>();
        var result = await InvokeAsync(endpointDbContext, cacheProvider, gameId);
        var cachedResult = await InvokeAsync(endpointDbContext, cacheProvider, gameId);

        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(StatusCodes.Status200OK, cachedResult.StatusCode);
        Assert.Equal(gameId, cachedResult.Response!.Id);
        Assert.Equal(1, commandInterceptor.CommandCount);
        var response = Assert.IsType<GetGameByIdResponse>(result.Response);
        Assert.Equal(gameId, response.Id);
        Assert.Equal("Complete Game", response.Name);
        Assert.Equal("complete-game", response.Slug);
        Assert.Equal("Complete description", response.Description);
        Assert.Equal("Complete storyline", response.Storyline);
        Assert.Equal(SynchronizedAt, response.ReleaseDate);
        Assert.Equal(87.5m, response.TotalRating);
        Assert.Equal(42, response.TotalRatingCount);
        Assert.Equal("https://www.igdb.com/games/complete-game", response.IgdbUrl);
        Assert.Equal(new GameReferenceResponse(gameType.Id, gameType.Name), response.GameType);
        Assert.Equal(new GameReferenceResponse(gameStatus.Id, gameStatus.Name), response.GameStatus);
        Assert.Equal(5_000_000_999, response.VersionParentId);
        Assert.Equal("cover-complete", response.Cover!.ImageId);
        Assert.Equal("https://images.example/complete-large.jpg", response.Cover.LargeUrl);
        Assert.Equal(12, response.Popularity!.Rank);
        Assert.Equal(0.9125m, response.Popularity.Score);
        Assert.Equal(new GameReferenceResponse(popularityType.Id, popularityType.Name), response.Popularity.Type);
        Assert.Equal([new GameReferenceResponse(multiRoleCompany.Id, multiRoleCompany.Name)], response.Developers);
        Assert.Equal([new GameReferenceResponse(multiRoleCompany.Id, multiRoleCompany.Name)], response.Publishers);
        Assert.Equal(
            [new GameReferenceResponse(multiRoleCompany.Id, multiRoleCompany.Name)],
            response.PortingCompanies
        );
        Assert.Equal(
            [new GameReferenceResponse(supportingCompany.Id, supportingCompany.Name)],
            response.SupportingCompanies
        );
        Assert.Equal([300L, 301L], response.StorePages.Select(value => value.Id).ToArray());
        Assert.Equal("123456", response.StorePages[0].ExternalId);
        Assert.Equal(new GameReferenceResponse(platform.Id, platform.Name), response.StorePages[0].Platform);
        Assert.Null(response.StorePages[1].Platform);
        Assert.Null(response.StorePages[1].ExternalId);
        Assert.Empty(response.Editions);
        Assert.Equal(["Dlc", "Expansion", "StandaloneExpansion"], response.Addons.Select(value => value.Type));
        Assert.Equal([dlc.Id, expansion.Id, standaloneExpansion.Id], response.Addons.Select(value => value.Id));
        Assert.Equal("Addon description", response.Addons[0].Description);
        Assert.Equal("https://images.example/addon-large.jpg", response.Addons[0].CoverLargeUrl);
        Assert.Equal([new GameReferenceResponse(genre.Id, genre.Name)], response.Genres);
        Assert.Equal([new GameReferenceResponse(theme.Id, theme.Name)], response.Themes);
        Assert.Equal([new GameReferenceResponse(gameMode.Id, gameMode.Name)], response.GameModes);
        Assert.Equal([new GameReferenceResponse(perspective.Id, perspective.Name)], response.PlayerPerspectives);
        Assert.Equal([new GameReferenceResponse(platform.Id, platform.Name)], response.Platforms);
        Assert.Equal([new GameReferenceResponse(collection.Id, collection.Name)], response.Collections);
        Assert.Equal([new GameReferenceResponse(franchise.Id, franchise.Name)], response.Franchises);

        var contractJson = JsonSerializer.Serialize(response, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        using var document = JsonDocument.Parse(contractJson);
        var root = document.RootElement;
        Assert.True(root.TryGetProperty("cover", out var coverElement), contractJson);
        Assert.Equal("https://images.example/complete-large.jpg", coverElement.GetProperty("largeUrl").GetString());
        Assert.True(root.TryGetProperty("addons", out var addonsElement), contractJson);
        Assert.Equal("Dlc", addonsElement[0].GetProperty("type").GetString());
        Assert.True(root.TryGetProperty("editions", out var editionsElement), contractJson);
        Assert.Equal(0, editionsElement.GetArrayLength());
        Assert.True(root.TryGetProperty("portingCompanies", out _));
    }

    [Fact]
    public async Task GetGameByIdEndpoint_ReturnsDirectEditionsAndTheirOrderedStorePagesForBaseGames()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();

        var steam = Stamp(new ExternalGameSource { Id = 1, Name = "Steam" });
        var xbox = Stamp(new ExternalGameSource { Id = 31, Name = "Xbox Marketplace" });
        var baseGame = Stamp(new Game { Id = 5_000_004_000, Name = "Base Game" });
        var newerEdition = Stamp(
            new Game
            {
                Id = 5_000_004_001,
                Name = "Complete Edition",
                Summary = "The complete release.",
                FirstReleaseDate = SynchronizedAt.AddYears(-1),
                VersionParentId = baseGame.Id,
                CoverSmallUrl = "https://images.example/complete-small.jpg",
                CoverLargeUrl = "https://images.example/complete-large.jpg",
            }
        );
        var olderEdition = Stamp(
            new Game
            {
                Id = 5_000_004_002,
                Name = "Launch Edition",
                FirstReleaseDate = SynchronizedAt.AddYears(-2),
                VersionParentId = baseGame.Id,
            }
        );
        var undatedEdition = Stamp(
            new Game
            {
                Id = 5_000_004_003,
                Name = "Collector's Edition",
                VersionParentId = baseGame.Id,
            }
        );

        dbContext.AddRange(steam, xbox, baseGame, newerEdition, olderEdition, undatedEdition);
        dbContext.AddRange(
            Stamp(
                new ExternalGame
                {
                    Id = 400,
                    GameId = baseGame.Id,
                    ExternalGameSourceId = xbox.Id,
                    Name = baseGame.Name,
                    Url = "https://www.xbox.com/games/store/base-game",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 401,
                    GameId = newerEdition.Id,
                    ExternalGameSourceId = steam.Id,
                    Name = newerEdition.Name,
                    Url = "http://store.steampowered.com/app/401",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 402,
                    GameId = newerEdition.Id,
                    ExternalGameSourceId = steam.Id,
                    Name = newerEdition.Name,
                    Url = "https://store.steampowered.com/app/402",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 403,
                    GameId = newerEdition.Id,
                    ExternalGameSourceId = xbox.Id,
                    Name = newerEdition.Name,
                    Url = "https://www.xbox.com/games/store/complete-edition",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 404,
                    GameId = olderEdition.Id,
                    ExternalGameSourceId = steam.Id,
                    Name = olderEdition.Name,
                    Url = "https://store.steampowered.com/app/404",
                }
            ),
            Stamp(
                new ExternalGame
                {
                    Id = 405,
                    GameId = undatedEdition.Id,
                    ExternalGameSourceId = steam.Id,
                    Name = undatedEdition.Name,
                    Url = "https://store.steampowered.com/app/405",
                }
            )
        );
        await dbContext.SaveChangesAsync();

        var commandInterceptor = new CountingDbCommandInterceptor();
        await using var endpointDbContext = _database.CreateDbContext(commandInterceptor);
        await using var cacheServices = CreateCacheServices();
        var cacheProvider = cacheServices.GetRequiredService<IFusionCacheProvider>();

        var baseResult = await InvokeAsync(endpointDbContext, cacheProvider, baseGame.Id);

        Assert.Equal(StatusCodes.Status200OK, baseResult.StatusCode);
        var baseResponse = Assert.IsType<GetGameByIdResponse>(baseResult.Response);
        Assert.Equal(
            [newerEdition.Id, olderEdition.Id, undatedEdition.Id],
            baseResponse.Editions.Select(edition => edition.Id).ToArray()
        );
        Assert.Equal("The complete release.", baseResponse.Editions[0].Description);
        Assert.Equal("https://images.example/complete-large.jpg", baseResponse.Editions[0].CoverLargeUrl);
        Assert.Null(baseResponse.Editions[1].Description);
        Assert.Equal([400L, 402L, 403L, 401L, 404L, 405L], baseResponse.StorePages.Select(page => page.Id));
        Assert.Equal([400L, 403L], baseResponse.StorePages.Where(IsXboxStore).Select(page => page.Id));
        Assert.Equal([402L, 401L, 404L, 405L], baseResponse.StorePages.Where(IsSteamStore).Select(page => page.Id));

        var editionResult = await InvokeAsync(endpointDbContext, cacheProvider, newerEdition.Id);

        Assert.Equal(StatusCodes.Status200OK, editionResult.StatusCode);
        var editionResponse = Assert.IsType<GetGameByIdResponse>(editionResult.Response);
        Assert.Empty(editionResponse.Editions);
        Assert.Equal([402L, 403L, 401L], editionResponse.StorePages.Select(page => page.Id));
        Assert.Equal(2, commandInterceptor.CommandCount);
    }

    [Fact]
    public async Task GetGameByIdEndpoint_ReturnsSparseStoredEditionWithNullsAndEmptyCollections()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var edition = Stamp(
            new Game
            {
                Id = 5_000_002_000,
                Name = "Sparse Edition",
                VersionParentId = 5_000_001_999,
            }
        );
        dbContext.Games.Add(edition);
        await dbContext.SaveChangesAsync();

        var commandInterceptor = new CountingDbCommandInterceptor();
        await using var endpointDbContext = _database.CreateDbContext(commandInterceptor);
        await using var cacheServices = CreateCacheServices();
        var cacheProvider = cacheServices.GetRequiredService<IFusionCacheProvider>();
        var result = await InvokeAsync(endpointDbContext, cacheProvider, edition.Id);

        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(1, commandInterceptor.CommandCount);
        var response = Assert.IsType<GetGameByIdResponse>(result.Response);
        Assert.Equal(edition.VersionParentId, response.VersionParentId);
        Assert.Null(response.Description);
        Assert.Null(response.ReleaseDate);
        Assert.Null(response.GameType);
        Assert.Null(response.GameStatus);
        Assert.Null(response.Cover);
        Assert.Null(response.Popularity);
        Assert.Empty(response.Developers);
        Assert.Empty(response.Publishers);
        Assert.Empty(response.PortingCompanies);
        Assert.Empty(response.SupportingCompanies);
        Assert.Empty(response.StorePages);
        Assert.Empty(response.Editions);
        Assert.Empty(response.Addons);
        Assert.Empty(response.Genres);
        Assert.Empty(response.Themes);
        Assert.Empty(response.GameModes);
        Assert.Empty(response.PlayerPerspectives);
        Assert.Empty(response.Platforms);
        Assert.Empty(response.Collections);
        Assert.Empty(response.Franchises);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(5_000_003_000)]
    public async Task GetGameByIdEndpoint_ReturnsNotFoundForUnknownGame(long gameId)
    {
        await _database.ResetAsync();

        var commandInterceptor = new CountingDbCommandInterceptor();
        await using var endpointDbContext = _database.CreateDbContext(commandInterceptor);
        await using var cacheServices = CreateCacheServices();
        var cacheProvider = cacheServices.GetRequiredService<IFusionCacheProvider>();
        var result = await InvokeAsync(endpointDbContext, cacheProvider, gameId);

        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        Assert.Equal(1, commandInterceptor.CommandCount);
        Assert.Null(result.Response);
    }

    private static Game CreateAddon(long id, string name)
    {
        return Stamp(
            new Game
            {
                Id = id,
                Name = name,
                Summary = "Addon description",
                FirstReleaseDate = SynchronizedAt,
                CoverSmallUrl = "https://images.example/addon-small.jpg",
                CoverLargeUrl = "https://images.example/addon-large.jpg",
            }
        );
    }

    private static bool IsSteamStore(GameStorePageResponse storePage)
    {
        return storePage.Source.Name == "Steam";
    }

    private static bool IsXboxStore(GameStorePageResponse storePage)
    {
        return storePage.Source.Name == "Xbox Marketplace";
    }

    private static GameRelation CreateRelation(long gameId, long relatedGameId, GameRelationType relationType)
    {
        return new GameRelation
        {
            GameId = gameId,
            RelatedGameId = relatedGameId,
            RelationType = relationType,
            CreatedAt = SynchronizedAt,
        };
    }

    private static T Stamp<T>(T entity)
        where T : class, IIgdbEntity
    {
        entity.CreatedAt = SynchronizedAt;
        entity.UpdatedAt = SynchronizedAt;
        entity.LastSyncedAt = SynchronizedAt;
        return entity;
    }

    private static ServiceProvider CreateCacheServices()
    {
        var services = new ServiceCollection();
        services
            .AddFusionCache(GameCache.CacheName)
            .WithCacheKeyPrefix(GameCache.CacheKeyPrefix)
            .WithDefaultEntryOptions(options => options.SetDuration(GameCache.Expiration));
        return services.BuildServiceProvider();
    }

    private static async Task<EndpointResult> InvokeAsync(
        ApplicationDbContext dbContext,
        IFusionCacheProvider cacheProvider,
        long gameId
    )
    {
        var httpContext = new DefaultHttpContext();
        await using var responseBody = new MemoryStream();
        httpContext.Response.Body = responseBody;
        var endpoint = Factory.Create<GetGameByIdEndpoint>(httpContext, dbContext, cacheProvider);
        await endpoint.HandleAsync(new GetGameByIdRequest { Id = gameId }, CancellationToken.None);
        responseBody.Position = 0;

        using var reader = new StreamReader(responseBody);
        var json = await reader.ReadToEndAsync();
        GetGameByIdResponse? response = null;
        if (json.Length > 0)
        {
            response = JsonSerializer.Deserialize<GetGameByIdResponse>(
                json,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            );
        }

        return new EndpointResult(httpContext.Response.StatusCode, json, response);
    }

    private sealed record EndpointResult(int StatusCode, string Json, GetGameByIdResponse? Response);
}
