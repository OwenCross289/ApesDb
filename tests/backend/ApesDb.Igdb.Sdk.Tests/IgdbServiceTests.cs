using System.Text.RegularExpressions;
using ApesDb.Igdb.Sdk.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ApesDb.Igdb.Sdk.Tests;

public sealed class IgdbServiceTests
{
    [Fact]
    public async Task FetchPopularCatalogAsync_PagesFiltersAndHydratesTheCatalog()
    {
        var client = new FakeIgdbClient();
        var service = new IgdbService(client, NullLogger<IgdbService>.Instance);

        var catalog = await service.FetchPopularCatalogAsync(2);

        Assert.Equal([1, 2], catalog.PopularGames.Select(value => value.Rank));
        Assert.Equal([500, 501], catalog.PopularGames.Select(value => value.SourceRank));
        Assert.Equal([500L, 501L], catalog.PopularGames.Select(value => value.GameId));
        Assert.Contains(client.PopularityQueries, query => query.Contains("offset 0", StringComparison.Ordinal));
        Assert.Contains(client.PopularityQueries, query => query.Contains("offset 500", StringComparison.Ordinal));

        Assert.Contains(catalog.Games, value => value.Id == 900 && value.Name == "Related DLC");
        Assert.Contains(
            catalog.Relations,
            value => value.GameId == 500 && value.RelatedGameId == 900 && value.RelationType == IgdbGameRelationType.Dlc
        );
        var rankedGame = Assert.Single(catalog.Games, value => value.Id == 500);
        Assert.Equal(
            "https://images.igdb.com/igdb/image/upload/t_cover_small_2x/cover500.jpg",
            rankedGame.Cover?.SmallUrl
        );
        Assert.Equal(
            "https://images.igdb.com/igdb/image/upload/t_cover_big_2x/cover500.jpg",
            rankedGame.Cover?.LargeUrl
        );
        Assert.Contains(catalog.GameModes, value => value.Name == "Split screen");
        Assert.Contains(catalog.GameGameModes, value => value.GameId == 500 && value.GameModeId == 30);
        Assert.Contains(catalog.ExternalGameIdentifiers, value => value.ExternalId == "12345");
        Assert.Contains(catalog.GameCompanies, value => value.GameId == 500 && value.Developer);
        Assert.Equal("A developer", Assert.Single(catalog.Companies).Description);
        Assert.Equal(578, Assert.Single(catalog.Companies).CountryCode);
        Assert.Equal(2, catalog.Franchises.Count);
        Assert.Equal(2, catalog.GameFranchises.Count(value => value.GameId == 500));
    }

    private sealed class FakeIgdbClient : IIgdbClient
    {
        private const long UpdatedAt = 1_783_593_600;

        private static readonly IgdbGame MainGame = CreateGame(
            500,
            "Ranked Game",
            1,
            cover: new IgdbCover(5000, "cover500", 264, 374, "//images.igdb.com/cover500.jpg", null),
            dlcs: [900],
            genres: [10],
            themes: [20],
            gameModes: [30],
            perspectives: [40],
            platforms: [50],
            externalGames: [80],
            involvedCompanies: [90],
            collections: [100],
            franchise: 110,
            franchises: [111]
        );
        private static readonly IgdbGame SecondMainGame = CreateGame(501, "Second Ranked Game", 1);
        private static readonly IgdbGame RelatedDlc = CreateGame(900, "Related DLC", 2);

        public List<string> PopularityQueries { get; } = [];

        public Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
            string endpoint,
            string query,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            var requestedIds = ParseIds(query);
            IEnumerable<object> resources = endpoint switch
            {
                "popularity_types" => [new IgdbPopularityTypeResource(1, "Visits", 121, null, UpdatedAt)],
                "game_types" when requestedIds.Count == 0 =>
                [
                    new IgdbGameTypeResource(1, "Main Game", null, UpdatedAt),
                ],
                "game_types" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbGameTypeResource(1, "Main Game", null, UpdatedAt),
                    new IgdbGameTypeResource(2, "DLC/Add-on", null, UpdatedAt)
                ),
                "games" => Filter(requestedIds, value => value.Id, MainGame, SecondMainGame, RelatedDlc),
                "game_statuses" => [],
                "genres" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(10, "Adventure", "adventure", null, null, UpdatedAt)
                ),
                "themes" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(20, "Action", "action", null, null, UpdatedAt)
                ),
                "game_modes" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(30, "Split screen", "split-screen", null, null, UpdatedAt)
                ),
                "player_perspectives" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(40, "Third person", "third-person", null, null, UpdatedAt)
                ),
                "platforms" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbPlatformResource(
                        50,
                        "PC",
                        "PC",
                        null,
                        "win",
                        5,
                        null,
                        null,
                        null,
                        null,
                        [60],
                        null,
                        UpdatedAt
                    )
                ),
                "platform_websites" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbPlatformWebsiteResource(60, 1, "https://example.com/pc", true, null)
                ),
                "website_types" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbWebsiteTypeResource(1, "Official", null, UpdatedAt)
                ),
                "platform_types" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(5, "Operating system", null, null, null, UpdatedAt)
                ),
                "external_games" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbExternalGameResource(
                        80,
                        500,
                        70,
                        50,
                        "12345",
                        "Ranked Game",
                        "https://store.steampowered.com/app/12345",
                        2026,
                        null,
                        UpdatedAt
                    )
                ),
                "external_game_sources" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbExternalGameSourceResource(70, "Steam", null, UpdatedAt)
                ),
                "involved_companies" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbInvolvedCompanyResource(90, 500, 91, true, false, false, false, null, UpdatedAt)
                ),
                "companies" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbCompanyResource(
                        91,
                        "Developer",
                        "developer",
                        "A developer",
                        578,
                        null,
                        null,
                        null,
                        UpdatedAt
                    )
                ),
                "collections" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(100, "Game Series", "game-series", null, null, UpdatedAt)
                ),
                "franchises" => Filter(
                    requestedIds,
                    value => value.Id,
                    new IgdbNamedResource(110, "Main Franchise", "main-franchise", null, null, UpdatedAt),
                    new IgdbNamedResource(111, "Other Franchise", "other-franchise", null, null, UpdatedAt)
                ),
                _ => throw new InvalidOperationException($"Unexpected IGDB endpoint '{endpoint}'."),
            };

            return Task.FromResult<IReadOnlyList<TResource>>(resources.Cast<TResource>().ToArray());
        }

        public Task<IReadOnlyList<IgdbPopularityPrimitive>> QueryPopularityPrimitivesAsync(
            string query,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            PopularityQueries.Add(query);

            if (query.Contains("offset 500", StringComparison.Ordinal))
            {
                return Task.FromResult<IReadOnlyList<IgdbPopularityPrimitive>>([
                    new IgdbPopularityPrimitive(10_501, 501, 0.5m, 1, UpdatedAt, null, UpdatedAt),
                ]);
            }

            var firstPage = Enumerable
                .Range(1, 500)
                .Select(index => new IgdbPopularityPrimitive(
                    10_000 + index,
                    index,
                    1m - index / 1000m,
                    1,
                    UpdatedAt,
                    null,
                    UpdatedAt
                ))
                .ToArray();
            return Task.FromResult<IReadOnlyList<IgdbPopularityPrimitive>>(firstPage);
        }

        public Task<IReadOnlyList<IgdbGame>> QueryGamesAsync(string query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var requestedIds = ParseIds(query);
            IReadOnlyList<IgdbGame> games = new[] { MainGame, SecondMainGame }
                .Where(value => requestedIds.Contains(value.Id))
                .ToArray();
            return Task.FromResult(games);
        }

        private static IgdbGame CreateGame(
            long id,
            string name,
            long typeId,
            IgdbCover? cover = null,
            IReadOnlyList<long>? dlcs = null,
            IReadOnlyList<long>? genres = null,
            IReadOnlyList<long>? themes = null,
            IReadOnlyList<long>? gameModes = null,
            IReadOnlyList<long>? perspectives = null,
            IReadOnlyList<long>? platforms = null,
            IReadOnlyList<long>? externalGames = null,
            IReadOnlyList<long>? involvedCompanies = null,
            IReadOnlyList<long>? collections = null,
            long? franchise = null,
            IReadOnlyList<long>? franchises = null
        )
        {
            return new IgdbGame(
                id,
                name,
                $"game-{id}",
                "Summary",
                null,
                90,
                10,
                UpdatedAt,
                $"https://www.igdb.com/games/game-{id}",
                typeId,
                null,
                cover,
                dlcs,
                null,
                null,
                genres,
                themes,
                gameModes,
                perspectives,
                platforms,
                externalGames,
                involvedCompanies,
                collections,
                franchise,
                franchises,
                null,
                UpdatedAt
            );
        }

        private static HashSet<long> ParseIds(string query)
        {
            var match = Regex.Match(query, @"where id = \(([^)]*)\)", RegexOptions.CultureInvariant);
            return !match.Success
                ? []
                : match
                    .Groups[1]
                    .Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(long.Parse)
                    .ToHashSet();
        }

        private static IEnumerable<object> Filter<T>(
            IReadOnlySet<long> requestedIds,
            Func<T, long> idSelector,
            params T[] values
        )
        {
            return values.Where(value => requestedIds.Contains(idSelector(value))).Cast<object>();
        }
    }
}
