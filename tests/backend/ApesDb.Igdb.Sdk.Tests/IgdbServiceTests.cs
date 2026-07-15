using System.Threading.RateLimiting;
using ApesDb.Igdb.Sdk.Models;
using Xunit;

namespace ApesDb.Igdb.Sdk.Tests;

public sealed class IgdbServiceTests
{
    private const long UpdatedAt = 1_783_593_600;

    [Fact]
    public async Task FetchPageAsync_AllowsInitialCursorToIncludeIdZero()
    {
        var client = new RecordingIgdbClient(new IgdbGameStatusResource(0, "Released", null, UpdatedAt));
        var service = new IgdbService(client);

        var status = Assert.Single(await service.FetchGameStatusesPageAsync(-1));

        Assert.Equal(0, status.Id);
        Assert.Contains("where id > -1", Assert.Single(client.Queries).Query, StringComparison.Ordinal);
    }

    [Fact]
    public async Task FetchGamesPageAsync_UsesUnwindowedKeysetAndMapsACompleteShortPage()
    {
        var checksum = Guid.NewGuid();
        var client = new RecordingIgdbClient(
            new IgdbGameResource(
                Id: 1_201,
                Name: "A game",
                Slug: "a-game",
                Summary: "Summary",
                Storyline: "Story",
                TotalRating: 91.5,
                TotalRatingCount: 25,
                FirstReleaseDate: UpdatedAt,
                Url: "https://www.igdb.com/games/a-game",
                GameTypeId: 7,
                GameStatusId: 3,
                VersionParentId: 1_200,
                Cover: new IgdbCover(91, "cover-91", 264, 374, "//images.igdb.com/cover-91.jpg", checksum),
                DlcIds: [2_001],
                ExpansionIds: [2_002],
                StandaloneExpansionIds: [2_003],
                GenreIds: [10],
                ThemeIds: [20],
                GameModeIds: [30],
                PlayerPerspectiveIds: [40],
                PlatformIds: [50],
                CollectionIds: [80],
                FranchiseId: 90,
                FranchiseIds: [90, 91],
                Checksum: checksum,
                UpdatedAt: UpdatedAt
            ),
            new IgdbGameResource(
                Id: 1_202,
                Name: null,
                Slug: null,
                Summary: null,
                Storyline: null,
                TotalRating: null,
                TotalRatingCount: null,
                FirstReleaseDate: null,
                Url: null,
                GameTypeId: null,
                GameStatusId: null,
                VersionParentId: null,
                Cover: null,
                DlcIds: null,
                ExpansionIds: null,
                StandaloneExpansionIds: null,
                GenreIds: null,
                ThemeIds: null,
                GameModeIds: null,
                PlayerPerspectiveIds: null,
                PlatformIds: null,
                CollectionIds: null,
                FranchiseId: null,
                FranchiseIds: null,
                Checksum: null,
                UpdatedAt: null
            )
        );
        var service = new IgdbService(client);

        var page = await service.FetchGamesPageAsync(1_200);

        Assert.Equal(2, page.Count);
        var game = page[0];
        Assert.Equal(1_201, game.Id);
        Assert.Equal(1_200, game.VersionParentId);
        Assert.Equal([2_001], game.DlcIds);
        Assert.Equal([2_002], game.ExpansionIds);
        Assert.Equal([2_003], game.StandaloneExpansionIds);
        Assert.Equal([10], game.GenreIds);
        Assert.Equal([20], game.ThemeIds);
        Assert.Equal([30], game.GameModeIds);
        Assert.Equal([40], game.PlayerPerspectiveIds);
        Assert.Equal([50], game.PlatformIds);
        Assert.Equal([80], game.CollectionIds);
        Assert.Equal([90, 91], game.FranchiseIds);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(UpdatedAt), game.FirstReleaseDate);
        Assert.Equal("https://images.igdb.com/cover-91.jpg", game.Cover?.SourceUrl);
        Assert.Equal("https://images.igdb.com/igdb/image/upload/t_cover_small_2x/cover-91.jpg", game.Cover?.SmallUrl);
        Assert.Empty(page[1].GenreIds);

        var query = Assert.Single(client.Queries);
        Assert.Equal("games", query.Endpoint);
        Assert.Contains("cover.id,cover.image_id,cover.width,cover.height", query.Query, StringComparison.Ordinal);
        Assert.Contains("version_parent", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("version_title", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("parent_game", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("external_games", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("involved_companies", query.Query, StringComparison.Ordinal);
        Assert.Contains("where id > 1200; sort id asc; limit 500;", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("& updated_at", query.Query, StringComparison.Ordinal);
        Assert.DoesNotContain("game_type =", query.Query, StringComparison.Ordinal);
    }

    [Fact]
    public async Task FetchGamesPageAsync_UsesCreatedAtUpperBoundForBootstrapWindow()
    {
        var client = new RecordingIgdbClient();
        var service = new IgdbService(client);
        var window = new IgdbSyncWindow(null, DateTimeOffset.FromUnixTimeSeconds(2_000));

        await service.FetchGamesPageAsync(42, window);

        var query = Assert.Single(client.Queries);
        Assert.Contains(
            "where id > 42 & created_at <= 2000; sort id asc; limit 500;",
            query.Query,
            StringComparison.Ordinal
        );
        Assert.DoesNotContain("& updated_at", query.Query, StringComparison.Ordinal);
    }

    [Fact]
    public async Task FetchGenresPageAsync_UsesStrictInclusiveIncrementalWindowAndKeysetCursor()
    {
        var client = new RecordingIgdbClient(new IgdbNamedResource(43, "Adventure", "adventure", null, null, 2_000));
        var service = new IgdbService(client);
        var window = new IgdbSyncWindow(
            DateTimeOffset.FromUnixTimeSeconds(1_000),
            DateTimeOffset.FromUnixTimeSeconds(2_000)
        );

        var page = await service.FetchGenresPageAsync(42, window);

        var genre = Assert.Single(page);
        Assert.Equal("Adventure", genre.Name);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(2_000), genre.UpdatedAt);
        var query = Assert.Single(client.Queries);
        Assert.Equal(
            "fields id,name,slug,url,checksum,updated_at; where id > 42 & updated_at > 1000 & updated_at <= 2000; sort id asc; limit 500;",
            query.Query
        );
    }

    [Fact]
    public async Task FetchPageAsync_RejectsAReversedIncrementalWindowBeforeCallingIgdb()
    {
        var client = new RecordingIgdbClient();
        var service = new IgdbService(client);
        var window = new IgdbSyncWindow(
            DateTimeOffset.FromUnixTimeSeconds(2_000),
            DateTimeOffset.FromUnixTimeSeconds(1_000)
        );

        await Assert.ThrowsAsync<ArgumentException>(() => service.FetchGamesPageAsync(0, window));

        Assert.Empty(client.Queries);
    }

    [Fact]
    public async Task FetchExternalGamesPageAsync_PreservesUrlOnlyStoreRows()
    {
        var client = new RecordingIgdbClient(
            new IgdbExternalGameResource(
                81,
                500,
                1,
                6,
                null,
                "A game",
                "https://store.steampowered.com/app/500",
                2026,
                null,
                UpdatedAt
            )
        );
        var service = new IgdbService(client);

        var externalGame = Assert.Single(await service.FetchExternalGamesPageAsync(80));

        Assert.Null(externalGame.Uid);
        Assert.Equal("https://store.steampowered.com/app/500", externalGame.Url);
        Assert.Equal(1, externalGame.SourceId);
        Assert.Equal(6, externalGame.PlatformId);
        Assert.Equal("external_games", Assert.Single(client.Queries).Endpoint);
    }

    [Fact]
    public async Task FetchPlatformsAndWebsites_MapNestedDataWithoutInventingAWebsitePlatformField()
    {
        var client = new EndpointIgdbClient(
            new Dictionary<string, IReadOnlyList<object>>
            {
                ["platforms"] =
                [
                    new IgdbPlatformResource(
                        6,
                        "PC (Microsoft Windows)",
                        "PC",
                        "Windows",
                        "win",
                        1,
                        null,
                        "Personal computer",
                        "https://www.igdb.com/platforms/win",
                        new IgdbCover(10, "pc-logo", 100, 100, "//images.igdb.com/pc-logo.jpg", null),
                        [100, 101],
                        null,
                        UpdatedAt
                    ),
                ],
                ["platform_websites"] = [new IgdbPlatformWebsiteResource(100, 1, "https://example.com", true, null)],
            }
        );
        var service = new IgdbService(client);

        var platform = Assert.Single(await service.FetchPlatformsPageAsync(0));
        var website = Assert.Single(await service.FetchPlatformWebsitesPageAsync(0));

        Assert.Equal([100, 101], platform.WebsiteIds);
        Assert.Equal("https://images.igdb.com/pc-logo.jpg", platform.Logo?.SourceUrl);
        Assert.Equal(1, website.WebsiteTypeId);
        Assert.Contains(
            "platform_logo.id,platform_logo.image_id,platform_logo.width,platform_logo.height",
            client.Queries[0].Query,
            StringComparison.Ordinal
        );
        Assert.DoesNotContain("updated_at", client.Queries[1].Query, StringComparison.Ordinal);
        Assert.Equal(
            "fields id,type,url,trusted,checksum; where id > 0; sort id asc; limit 500;",
            client.Queries[1].Query
        );
    }

    [Fact]
    public async Task LookupAndDependentResourceMethods_MapEveryConsumedEndpoint()
    {
        var client = new EndpointIgdbClient(CreateLookupResponses());
        var service = new IgdbService(client);

        Assert.Equal("Main Game", Assert.Single(await service.FetchGameTypesPageAsync(-1)).Name);
        Assert.Equal("Released", Assert.Single(await service.FetchGameStatusesPageAsync(0)).Name);
        Assert.Equal("Adventure", Assert.Single(await service.FetchGenresPageAsync(0)).Name);
        Assert.Equal("Science fiction", Assert.Single(await service.FetchThemesPageAsync(0)).Name);
        Assert.Equal("Single player", Assert.Single(await service.FetchGameModesPageAsync(0)).Name);
        Assert.Equal("Third person", Assert.Single(await service.FetchPlayerPerspectivesPageAsync(0)).Name);
        Assert.Equal("Console", Assert.Single(await service.FetchPlatformTypesPageAsync(0)).Name);
        Assert.Equal("Official", Assert.Single(await service.FetchWebsiteTypesPageAsync(0)).Name);
        Assert.Equal(121, Assert.Single(await service.FetchPopularityTypesPageAsync(0)).ExternalPopularitySourceId);
        Assert.Equal("Steam", Assert.Single(await service.FetchExternalGameSourcesPageAsync(0)).Name);
        Assert.Equal("Developer", Assert.Single(await service.FetchCompaniesPageAsync(0)).Name);
        Assert.Equal("Series", Assert.Single(await service.FetchCollectionsPageAsync(0)).Name);
        Assert.Equal("Franchise", Assert.Single(await service.FetchFranchisesPageAsync(0)).Name);
        Assert.True(Assert.Single(await service.FetchInvolvedCompaniesPageAsync(0)).Developer);

        Assert.Equal(
            [
                "game_types",
                "game_statuses",
                "genres",
                "themes",
                "game_modes",
                "player_perspectives",
                "platform_types",
                "website_types",
                "popularity_types",
                "external_game_sources",
                "companies",
                "collections",
                "franchises",
                "involved_companies",
            ],
            client.Queries.Select(query => query.Endpoint)
        );
    }

    [Fact]
    public async Task FetchPopularityPrimitivesPageAsync_UsesLightweightRankedPaging()
    {
        var client = new RecordingIgdbClient(
            new IgdbPopularityPrimitiveResource(70, 500, 12.5m, 1, 121, UpdatedAt, null, UpdatedAt)
        );
        var service = new IgdbService(client);

        var primitive = Assert.Single(await service.FetchPopularityPrimitivesPageAsync(1, 500));

        Assert.Equal(500, primitive.GameId);
        Assert.Equal(1, primitive.PopularityTypeId);
        Assert.Equal(121, primitive.ExternalPopularitySourceId);
        var query = Assert.Single(client.Queries);
        Assert.Equal("popularity_primitives", query.Endpoint);
        Assert.Equal(
            "fields id,game_id,value,popularity_type,external_popularity_source,calculated_at,checksum,updated_at; where popularity_type = 1; sort value desc; limit 500; offset 500;",
            query.Query
        );
        Assert.DoesNotContain("games", query.Query, StringComparison.Ordinal);
    }

    [Fact]
    public async Task RateLimiter_QueuesTheFifthRequestInsteadOfRejectingIt()
    {
        using var limiter = IgdbSdkServiceCollectionExtensions.CreateRateLimiter();
        var leases = new List<RateLimitLease>();
        for (var index = 0; index < 4; index++)
        {
            leases.Add(await limiter.AcquireAsync(1));
        }

        Assert.All(leases, lease => Assert.True(lease.IsAcquired));

        var fifthLeaseTask = limiter.AcquireAsync(1).AsTask();
        Assert.False(fifthLeaseTask.IsCompleted);

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        var fifthLease = await fifthLeaseTask.WaitAsync(timeout.Token);
        Assert.True(fifthLease.IsAcquired);

        fifthLease.Dispose();
        foreach (var lease in leases)
        {
            lease.Dispose();
        }
    }

    private static Dictionary<string, IReadOnlyList<object>> CreateLookupResponses()
    {
        return new Dictionary<string, IReadOnlyList<object>>
        {
            ["game_types"] = [new IgdbGameTypeResource(0, "Main Game", null, UpdatedAt)],
            ["game_statuses"] = [new IgdbGameStatusResource(1, "Released", null, UpdatedAt)],
            ["genres"] = [new IgdbNamedResource(1, "Adventure", "adventure", null, null, UpdatedAt)],
            ["themes"] = [new IgdbNamedResource(1, "Science fiction", "science-fiction", null, null, UpdatedAt)],
            ["game_modes"] = [new IgdbNamedResource(1, "Single player", "single-player", null, null, UpdatedAt)],
            ["player_perspectives"] = [new IgdbNamedResource(1, "Third person", "third-person", null, null, UpdatedAt)],
            ["platform_types"] = [new IgdbNamedResource(1, "Console", null, null, null, UpdatedAt)],
            ["website_types"] = [new IgdbWebsiteTypeResource(1, "Official", null, UpdatedAt)],
            ["popularity_types"] = [new IgdbPopularityTypeResource(1, "Visits", 121, null, UpdatedAt)],
            ["external_game_sources"] = [new IgdbExternalGameSourceResource(1, "Steam", null, UpdatedAt)],
            ["companies"] =
            [
                new IgdbCompanyResource(1, "Developer", "developer", "A developer", 578, null, null, null, UpdatedAt),
            ],
            ["collections"] = [new IgdbNamedResource(1, "Series", "series", null, null, UpdatedAt)],
            ["franchises"] = [new IgdbNamedResource(1, "Franchise", "franchise", null, null, UpdatedAt)],
            ["involved_companies"] =
            [
                new IgdbInvolvedCompanyResource(1, 10, 1, true, false, false, false, null, UpdatedAt),
            ],
        };
    }

    private sealed class RecordingIgdbClient : IIgdbClient
    {
        private readonly IReadOnlyList<object> _response;

        public RecordingIgdbClient(params object[] response)
        {
            _response = response;
        }

        public List<RecordedQuery> Queries { get; } = [];

        public Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
            string endpoint,
            string query,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            Queries.Add(new RecordedQuery(endpoint, query));
            return Task.FromResult<IReadOnlyList<TResource>>(_response.Cast<TResource>().ToArray());
        }
    }

    private sealed class EndpointIgdbClient : IIgdbClient
    {
        private readonly IReadOnlyDictionary<string, IReadOnlyList<object>> _responses;

        public EndpointIgdbClient(IReadOnlyDictionary<string, IReadOnlyList<object>> responses)
        {
            _responses = responses;
        }

        public List<RecordedQuery> Queries { get; } = [];

        public Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
            string endpoint,
            string query,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            Queries.Add(new RecordedQuery(endpoint, query));
            return Task.FromResult<IReadOnlyList<TResource>>(_responses[endpoint].Cast<TResource>().ToArray());
        }
    }

    private sealed record RecordedQuery(string Endpoint, string Query);
}
