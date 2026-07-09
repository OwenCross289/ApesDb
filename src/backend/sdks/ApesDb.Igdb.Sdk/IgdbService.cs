using ApesDb.Igdb.Sdk.Models;
using Microsoft.Extensions.Logging;

namespace ApesDb.Igdb.Sdk;

internal sealed class IgdbService : IIgdbService
{
    private const int PopularityPageSize = 500;
    private const int HydrationBatchSize = 200;
    private const string GameFields =
        "id,name,slug,summary,storyline,total_rating,total_rating_count,first_release_date,url,"
        + "game_type,game_status,cover.id,cover.image_id,cover.width,cover.height,cover.url,cover.checksum,"
        + "dlcs,expansions,standalone_expansions,genres,themes,game_modes,player_perspectives,platforms,"
        + "external_games,involved_companies,collections,franchise,franchises,checksum,updated_at";
    private const string NamedResourceFields = "id,name,slug,url,checksum,updated_at";

    private readonly IIgdbClient _client;
    private readonly ILogger<IgdbService> _logger;

    public IgdbService(IIgdbClient client, ILogger<IgdbService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IReadOnlyList<TopIgdbGame>> ListTopGamesAsync(
        int limit = 10,
        CancellationToken cancellationToken = default
    )
    {
        if (limit <= 0)
        {
            return [];
        }

        var popularityQuery =
            "fields game_id,value,popularity_type; " + $"sort value desc; limit {limit}; where popularity_type = 1;";

        var popularity = await _client.QueryPopularityPrimitivesAsync(popularityQuery, cancellationToken);

        if (popularity.Count == 0)
        {
            return [];
        }

        var gameIds = popularity.Select(game => game.GameId).Distinct().ToArray();
        var gamesQuery =
            "fields id,name,slug,summary,total_rating,first_release_date,cover.image_id; "
            + $"where id = ({string.Join(",", gameIds)}); limit {gameIds.Length};";
        var games = await _client.QueryGamesAsync(gamesQuery, cancellationToken);
        var gamesById = games.Where(game => !string.IsNullOrWhiteSpace(game.Name)).ToDictionary(game => game.Id);

        return popularity
            .Select(
                (entry, index) =>
                    gamesById.TryGetValue(entry.GameId, out var game)
                        ? new TopIgdbGame(
                            index + 1,
                            game.Id,
                            game.Name!,
                            game.Slug,
                            game.Summary,
                            game.TotalRating,
                            ToDateTimeOffset(game.FirstReleaseDate),
                            game.Cover?.ImageId,
                            (double)entry.Value
                        )
                        : null
            )
            .OfType<TopIgdbGame>()
            .ToArray();
    }

    public async Task<IgdbPopularCatalog> FetchPopularCatalogAsync(
        int targetCount = 1000,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(targetCount);

        var popularityTypeResource = await ResolvePopularityTypeAsync(cancellationToken);
        var mainGameTypeResource = await ResolveMainGameTypeAsync(cancellationToken);
        var rankedGames = await FetchRankedMainGamesAsync(
            popularityTypeResource.Id,
            mainGameTypeResource.Id,
            targetCount,
            cancellationToken
        );

        var baseGameIds = rankedGames.Select(entry => entry.Primitive.GameId).ToArray();
        var baseGames = await QueryByIdsAsync<IgdbGame>("games", GameFields, baseGameIds, cancellationToken);
        ValidateBaseGames(baseGameIds, baseGames);

        var proposedRelations = CreateRelations(baseGames);
        var relatedGameIds = proposedRelations.Select(relation => relation.RelatedGameId).Except(baseGameIds).ToArray();
        var relatedGames = await QueryByIdsAsync<IgdbGame>("games", GameFields, relatedGameIds, cancellationToken);
        var validRelatedGames = relatedGames.Where(HasIdentity).ToArray();
        var allGames = baseGames
            .Concat(validRelatedGames)
            .GroupBy(game => game.Id)
            .Select(group => group.First())
            .ToArray();
        var gameIdSet = allGames.Select(game => game.Id).ToHashSet();
        var relations = proposedRelations.Where(relation => gameIdSet.Contains(relation.RelatedGameId)).ToArray();

        if (relations.Length != proposedRelations.Count)
        {
            _logger.LogWarning(
                "IGDB omitted or returned invalid data for {SkippedRelationCount} related games; those relations were skipped.",
                proposedRelations.Count - relations.Length
            );
        }

        var externalResources = await QueryByIdsAsync<IgdbExternalGameResource>(
            "external_games",
            "id,game,external_game_source,platform,uid,name,url,year,checksum,updated_at",
            CollectIds(allGames, game => game.ExternalGameIds),
            cancellationToken
        );

        var platformIds = CollectIds(allGames, game => game.PlatformIds)
            .Concat(
                externalResources
                    .Where(resource => resource.PlatformId.HasValue)
                    .Select(resource => resource.PlatformId!.Value)
            )
            .Distinct()
            .ToArray();
        var platformResources = await QueryByIdsAsync<IgdbPlatformResource>(
            "platforms",
            "id,name,abbreviation,alternative_name,slug,platform_type,generation,summary,url,"
                + "platform_logo.id,platform_logo.image_id,platform_logo.width,platform_logo.height,"
                + "platform_logo.url,platform_logo.checksum,websites,checksum,updated_at",
            platformIds,
            cancellationToken
        );
        var validPlatformResources = platformResources.Where(resource => HasName(resource.Name)).ToArray();

        var platformWebsiteResources = await QueryByIdsAsync<IgdbPlatformWebsiteResource>(
            "platform_websites",
            "id,type,url,trusted,checksum",
            validPlatformResources.SelectMany(resource => resource.WebsiteIds ?? []),
            cancellationToken
        );
        var websiteTypeResources = await QueryByIdsAsync<IgdbWebsiteTypeResource>(
            "website_types",
            "id,type,checksum,updated_at",
            platformWebsiteResources.Select(resource => resource.WebsiteTypeId),
            cancellationToken
        );
        var platformTypeResources = await QueryByIdsAsync<IgdbNamedResource>(
            "platform_types",
            "id,name,checksum,updated_at",
            validPlatformResources
                .Where(resource => resource.PlatformTypeId.HasValue)
                .Select(resource => resource.PlatformTypeId!.Value),
            cancellationToken
        );

        var genreResources = await QueryByIdsAsync<IgdbNamedResource>(
            "genres",
            NamedResourceFields,
            CollectIds(allGames, game => game.GenreIds),
            cancellationToken
        );
        var themeResources = await QueryByIdsAsync<IgdbNamedResource>(
            "themes",
            NamedResourceFields,
            CollectIds(allGames, game => game.ThemeIds),
            cancellationToken
        );
        var gameModeResources = await QueryByIdsAsync<IgdbNamedResource>(
            "game_modes",
            NamedResourceFields,
            CollectIds(allGames, game => game.GameModeIds),
            cancellationToken
        );
        var perspectiveResources = await QueryByIdsAsync<IgdbNamedResource>(
            "player_perspectives",
            NamedResourceFields,
            CollectIds(allGames, game => game.PlayerPerspectiveIds),
            cancellationToken
        );

        var gameTypeResources = await QueryByIdsAsync<IgdbGameTypeResource>(
            "game_types",
            "id,type,checksum,updated_at",
            allGames.Where(game => game.GameTypeId.HasValue).Select(game => game.GameTypeId!.Value),
            cancellationToken
        );
        var gameStatusResources = await QueryByIdsAsync<IgdbGameStatusResource>(
            "game_statuses",
            "id,status,checksum,updated_at",
            allGames.Where(game => game.GameStatusId.HasValue).Select(game => game.GameStatusId!.Value),
            cancellationToken
        );

        var externalSourceResources = await QueryByIdsAsync<IgdbExternalGameSourceResource>(
            "external_game_sources",
            "id,name,checksum,updated_at",
            externalResources.Select(resource => resource.SourceId),
            cancellationToken
        );

        var involvedCompanyResources = await QueryByIdsAsync<IgdbInvolvedCompanyResource>(
            "involved_companies",
            "id,game,company,developer,publisher,porting,supporting,checksum,updated_at",
            CollectIds(allGames, game => game.InvolvedCompanyIds),
            cancellationToken
        );
        var companyResources = await QueryByIdsAsync<IgdbCompanyResource>(
            "companies",
            "id,name,slug,description,country,url,logo.id,logo.image_id,logo.width,logo.height,logo.url,logo.checksum,checksum,updated_at",
            involvedCompanyResources.Select(resource => resource.CompanyId),
            cancellationToken
        );
        var collectionResources = await QueryByIdsAsync<IgdbNamedResource>(
            "collections",
            NamedResourceFields,
            CollectIds(allGames, game => game.CollectionIds),
            cancellationToken
        );
        var franchiseResources = await QueryByIdsAsync<IgdbNamedResource>(
            "franchises",
            NamedResourceFields,
            CollectFranchiseIds(allGames),
            cancellationToken
        );

        var popularityType = Map(popularityTypeResource);
        var mainGameType = Map(mainGameTypeResource);
        var gameTypes = gameTypeResources
            .Append(mainGameTypeResource)
            .Where(resource => HasName(resource.Name))
            .GroupBy(resource => resource.Id)
            .Select(group => Map(group.First()))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var gameStatuses = gameStatusResources
            .Where(resource => HasName(resource.Name))
            .Select(Map)
            .OrderBy(resource => resource.Id)
            .ToArray();
        var games = allGames.Select(Map).OrderBy(game => game.Id).ToArray();

        var genres = genreResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbGenre(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var themes = themeResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbTheme(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var gameModes = gameModeResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbGameMode(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var playerPerspectives = perspectiveResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbPlayerPerspective(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();

        var platforms = validPlatformResources
            .Select(resource => new IgdbPlatform(
                resource.Id,
                resource.Name!,
                resource.Abbreviation,
                resource.AlternativeName,
                resource.Slug,
                resource.PlatformTypeId,
                resource.Generation,
                resource.Summary,
                resource.Url,
                MapImage(resource.Logo),
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var platformTypes = platformTypeResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbPlatformType(
                resource.Id,
                resource.Name!,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var websiteTypes = websiteTypeResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbWebsiteType(
                resource.Id,
                resource.Name!,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();

        var externalSources = externalSourceResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbExternalGameSource(
                resource.Id,
                resource.Name!,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var companies = companyResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbCompany(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Description,
                resource.CountryCode,
                resource.Url,
                MapImage(resource.Logo),
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var collections = collectionResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbCollection(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();
        var franchises = franchiseResources
            .Where(resource => HasName(resource.Name))
            .Select(resource => new IgdbFranchise(
                resource.Id,
                resource.Name!,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .OrderBy(resource => resource.Id)
            .ToArray();

        var platformIdSet = platforms.Select(resource => resource.Id).ToHashSet();
        var websiteTypeIdSet = websiteTypes.Select(resource => resource.Id).ToHashSet();
        var externalSourceIdSet = externalSources.Select(resource => resource.Id).ToHashSet();
        var companyIdSet = companies.Select(resource => resource.Id).ToHashSet();
        var genreIdSet = genres.Select(resource => resource.Id).ToHashSet();
        var themeIdSet = themes.Select(resource => resource.Id).ToHashSet();
        var gameModeIdSet = gameModes.Select(resource => resource.Id).ToHashSet();
        var perspectiveIdSet = playerPerspectives.Select(resource => resource.Id).ToHashSet();
        var collectionIdSet = collections.Select(resource => resource.Id).ToHashSet();
        var franchiseIdSet = franchises.Select(resource => resource.Id).ToHashSet();

        return new IgdbPopularCatalog(
            popularityType,
            mainGameType,
            rankedGames
                .Select(
                    (entry, index) =>
                        new IgdbPopularGame(
                            entry.Primitive.Id,
                            index + 1,
                            entry.SourceRank,
                            entry.Primitive.GameId,
                            entry.Primitive.Value,
                            ToDateTimeOffset(entry.Primitive.CalculatedAt),
                            entry.Primitive.Checksum,
                            ToDateTimeOffset(entry.Primitive.UpdatedAt)
                        )
                )
                .ToArray(),
            games,
            gameTypes,
            gameStatuses,
            relations,
            genres,
            CreateJoins(
                allGames,
                game => game.GenreIds,
                genreIdSet,
                (gameId, valueId) => new IgdbGameGenre(gameId, valueId)
            ),
            themes,
            CreateJoins(
                allGames,
                game => game.ThemeIds,
                themeIdSet,
                (gameId, valueId) => new IgdbGameTheme(gameId, valueId)
            ),
            gameModes,
            CreateJoins(
                allGames,
                game => game.GameModeIds,
                gameModeIdSet,
                (gameId, valueId) => new IgdbGameGameMode(gameId, valueId)
            ),
            playerPerspectives,
            CreateJoins(
                allGames,
                game => game.PlayerPerspectiveIds,
                perspectiveIdSet,
                (gameId, valueId) => new IgdbGamePlayerPerspective(gameId, valueId)
            ),
            platforms,
            platformTypes,
            CreateJoins(
                allGames,
                game => game.PlatformIds,
                platformIdSet,
                (gameId, valueId) => new IgdbGamePlatform(gameId, valueId)
            ),
            CreatePlatformLinks(validPlatformResources, platformWebsiteResources, websiteTypeIdSet),
            websiteTypes,
            externalSources,
            externalResources
                .Where(resource =>
                    gameIdSet.Contains(resource.GameId)
                    && externalSourceIdSet.Contains(resource.SourceId)
                    && !string.IsNullOrWhiteSpace(resource.ExternalId)
                )
                .Select(resource => new IgdbExternalGameIdentifier(
                    resource.Id,
                    resource.GameId,
                    resource.SourceId,
                    resource.PlatformId is { } platformId && platformIdSet.Contains(platformId) ? platformId : null,
                    resource.ExternalId!,
                    resource.Name,
                    resource.Url,
                    resource.Year,
                    resource.Checksum,
                    ToDateTimeOffset(resource.UpdatedAt)
                ))
                .OrderBy(resource => resource.Id)
                .ToArray(),
            companies,
            involvedCompanyResources
                .Where(resource => gameIdSet.Contains(resource.GameId) && companyIdSet.Contains(resource.CompanyId))
                .Select(resource => new IgdbGameCompany(
                    resource.Id,
                    resource.GameId,
                    resource.CompanyId,
                    resource.Developer,
                    resource.Publisher,
                    resource.Porting,
                    resource.Supporting,
                    resource.Checksum,
                    ToDateTimeOffset(resource.UpdatedAt)
                ))
                .OrderBy(resource => resource.Id)
                .ToArray(),
            collections,
            CreateJoins(
                allGames,
                game => game.CollectionIds,
                collectionIdSet,
                (gameId, valueId) => new IgdbGameCollection(gameId, valueId)
            ),
            franchises,
            CreateFranchiseJoins(allGames, franchiseIdSet)
        );
    }

    private async Task<IgdbPopularityTypeResource> ResolvePopularityTypeAsync(CancellationToken cancellationToken)
    {
        const string query =
            "fields id,name,external_popularity_source,checksum,updated_at; where name = \"Visits\"; limit 10;";
        var resources = await _client.QueryAsync<IgdbPopularityTypeResource>(
            "popularity_types",
            query,
            cancellationToken
        );
        var matches = resources
            .Where(resource => string.Equals(resource.Name, "Visits", StringComparison.Ordinal))
            .ToArray();

        return matches.Length == 1
            ? matches[0]
            : throw new InvalidOperationException(
                $"Expected exactly one IGDB popularity type named 'Visits', but found {matches.Length}."
            );
    }

    private async Task<IgdbGameTypeResource> ResolveMainGameTypeAsync(CancellationToken cancellationToken)
    {
        const string query = "fields id,type,checksum,updated_at; where type = \"Main Game\"; limit 10;";
        var resources = await _client.QueryAsync<IgdbGameTypeResource>("game_types", query, cancellationToken);
        var matches = resources
            .Where(resource => string.Equals(resource.Name, "Main Game", StringComparison.Ordinal))
            .ToArray();

        return matches.Length == 1
            ? matches[0]
            : throw new InvalidOperationException(
                $"Expected exactly one IGDB game type named 'Main Game', but found {matches.Length}."
            );
    }

    private async Task<IReadOnlyList<RankedPopularityPrimitive>> FetchRankedMainGamesAsync(
        long popularityTypeId,
        long mainGameTypeId,
        int targetCount,
        CancellationToken cancellationToken
    )
    {
        var accepted = new List<RankedPopularityPrimitive>(targetCount);
        var seenGameIds = new HashSet<long>();
        var offset = 0;

        while (accepted.Count < targetCount)
        {
            var query =
                "fields id,game_id,value,popularity_type,calculated_at,checksum,updated_at; "
                + $"where popularity_type = {popularityTypeId}; sort value desc; limit {PopularityPageSize}; offset {offset};";
            var page = await _client.QueryPopularityPrimitivesAsync(query, cancellationToken);
            if (page.Count == 0)
            {
                break;
            }

            var candidates = page.Select(
                    (primitive, index) => new RankedPopularityPrimitive(primitive, offset + index + 1)
                )
                .Where(entry => entry.Primitive.GameId > 0 && seenGameIds.Add(entry.Primitive.GameId))
                .ToArray();
            var mainGameIds = await FilterMainGameIdsAsync(candidates, mainGameTypeId, cancellationToken);

            foreach (var candidate in candidates)
            {
                if (mainGameIds.Contains(candidate.Primitive.GameId))
                {
                    accepted.Add(candidate);
                    if (accepted.Count == targetCount)
                    {
                        break;
                    }
                }
            }

            if (page.Count < PopularityPageSize)
            {
                break;
            }

            offset += PopularityPageSize;
        }

        if (accepted.Count != targetCount)
        {
            throw new InvalidOperationException(
                $"IGDB returned only {accepted.Count} unique Main Game records for the requested top {targetCount} catalog."
            );
        }

        return accepted;
    }

    private async Task<HashSet<long>> FilterMainGameIdsAsync(
        IReadOnlyList<RankedPopularityPrimitive> candidates,
        long mainGameTypeId,
        CancellationToken cancellationToken
    )
    {
        var result = new HashSet<long>();

        foreach (var batch in candidates.Select(entry => entry.Primitive.GameId).Chunk(HydrationBatchSize))
        {
            var query =
                "fields id,game_type; "
                + $"where id = ({string.Join(",", batch)}) & game_type = {mainGameTypeId}; limit {batch.Length};";
            var games = await _client.QueryGamesAsync(query, cancellationToken);
            result.UnionWith(games.Where(game => game.GameTypeId == mainGameTypeId).Select(game => game.Id));
        }

        return result;
    }

    private async Task<IReadOnlyList<TResource>> QueryByIdsAsync<TResource>(
        string endpoint,
        string fields,
        IEnumerable<long> ids,
        CancellationToken cancellationToken
    )
    {
        var distinctIds = ids.Where(id => id > 0).Distinct().Order().ToArray();
        if (distinctIds.Length == 0)
        {
            return [];
        }

        var resources = new List<TResource>(distinctIds.Length);
        foreach (var batch in distinctIds.Chunk(HydrationBatchSize))
        {
            var query = $"fields {fields}; where id = ({string.Join(",", batch)}); limit {batch.Length};";
            resources.AddRange(await _client.QueryAsync<TResource>(endpoint, query, cancellationToken));
        }

        return resources;
    }

    private static void ValidateBaseGames(IReadOnlyList<long> baseGameIds, IReadOnlyList<IgdbGame> games)
    {
        var gamesById = games.GroupBy(game => game.Id).ToDictionary(group => group.Key, group => group.First());
        var invalidIds = baseGameIds
            .Where(id => !gamesById.TryGetValue(id, out var game) || !HasIdentity(game))
            .ToArray();

        if (invalidIds.Length > 0)
        {
            throw new InvalidOperationException(
                $"IGDB failed to hydrate {invalidIds.Length} ranked games with required IDs and names."
            );
        }
    }

    private static List<IgdbGameRelation> CreateRelations(IEnumerable<IgdbGame> games)
    {
        var relations = new List<IgdbGameRelation>();

        foreach (var game in games)
        {
            AddRelations(relations, game.Id, game.DlcIds, IgdbGameRelationType.Dlc);
            AddRelations(relations, game.Id, game.ExpansionIds, IgdbGameRelationType.Expansion);
            AddRelations(relations, game.Id, game.StandaloneExpansionIds, IgdbGameRelationType.StandaloneExpansion);
        }

        return relations
            .Distinct()
            .OrderBy(relation => relation.GameId)
            .ThenBy(relation => relation.RelatedGameId)
            .ToList();
    }

    private static void AddRelations(
        ICollection<IgdbGameRelation> relations,
        long gameId,
        IEnumerable<long>? relatedGameIds,
        IgdbGameRelationType relationType
    )
    {
        foreach (var relatedGameId in relatedGameIds ?? [])
        {
            if (relatedGameId > 0)
            {
                relations.Add(new IgdbGameRelation(gameId, relatedGameId, relationType));
            }
        }
    }

    private static long[] CollectIds(IEnumerable<IgdbGame> games, Func<IgdbGame, IReadOnlyList<long>?> selector)
    {
        return games.SelectMany(game => selector(game) ?? []).Where(id => id > 0).Distinct().ToArray();
    }

    private static IReadOnlyList<TJoin> CreateJoins<TJoin>(
        IEnumerable<IgdbGame> games,
        Func<IgdbGame, IReadOnlyList<long>?> selector,
        IReadOnlySet<long> validValueIds,
        Func<long, long, TJoin> factory
    )
    {
        return games
            .SelectMany(game =>
                (selector(game) ?? [])
                    .Where(validValueIds.Contains)
                    .Distinct()
                    .Select(valueId => factory(game.Id, valueId))
            )
            .ToArray();
    }

    private static long[] CollectFranchiseIds(IEnumerable<IgdbGame> games)
    {
        return games
            .SelectMany(game =>
                (game.FranchiseIds ?? []).Concat(game.FranchiseId is { } franchiseId ? [franchiseId] : [])
            )
            .Where(id => id > 0)
            .Distinct()
            .ToArray();
    }

    private static IReadOnlyList<IgdbGameFranchise> CreateFranchiseJoins(
        IEnumerable<IgdbGame> games,
        IReadOnlySet<long> validFranchiseIds
    )
    {
        return games
            .SelectMany(game =>
                (game.FranchiseIds ?? [])
                    .Concat(game.FranchiseId is { } franchiseId ? [franchiseId] : [])
                    .Where(validFranchiseIds.Contains)
                    .Distinct()
                    .Select(franchiseId => new IgdbGameFranchise(game.Id, franchiseId))
            )
            .ToArray();
    }

    private static IReadOnlyList<IgdbPlatformLink> CreatePlatformLinks(
        IEnumerable<IgdbPlatformResource> platforms,
        IEnumerable<IgdbPlatformWebsiteResource> websites,
        IReadOnlySet<long> validWebsiteTypeIds
    )
    {
        var websitesById = websites
            .Where(website => validWebsiteTypeIds.Contains(website.WebsiteTypeId))
            .GroupBy(website => website.Id)
            .ToDictionary(group => group.Key, group => group.First());

        return platforms
            .SelectMany(platform =>
                (platform.WebsiteIds ?? [])
                    .Distinct()
                    .Where(websitesById.ContainsKey)
                    .Select(websiteId =>
                    {
                        var website = websitesById[websiteId];
                        return new IgdbPlatformLink(
                            website.Id,
                            platform.Id,
                            website.WebsiteTypeId,
                            website.Url,
                            website.Trusted,
                            website.Checksum
                        );
                    })
            )
            .OrderBy(link => link.PlatformId)
            .ThenBy(link => link.Id)
            .ToArray();
    }

    private static IgdbPopularityType Map(IgdbPopularityTypeResource resource)
    {
        return new IgdbPopularityType(
            resource.Id,
            resource.Name!,
            resource.ExternalPopularitySourceId,
            resource.Checksum,
            ToDateTimeOffset(resource.UpdatedAt)
        );
    }

    private static IgdbGameType Map(IgdbGameTypeResource resource)
    {
        return new IgdbGameType(resource.Id, resource.Name!, resource.Checksum, ToDateTimeOffset(resource.UpdatedAt));
    }

    private static IgdbGameStatus Map(IgdbGameStatusResource resource)
    {
        return new IgdbGameStatus(resource.Id, resource.Name!, resource.Checksum, ToDateTimeOffset(resource.UpdatedAt));
    }

    private static IgdbCatalogGame Map(IgdbGame game)
    {
        return new IgdbCatalogGame(
            game.Id,
            game.Name!,
            game.Slug,
            game.Summary,
            game.Storyline,
            ToDateTimeOffset(game.FirstReleaseDate),
            game.TotalRating,
            game.TotalRatingCount,
            game.Url,
            game.GameTypeId,
            game.GameStatusId,
            MapImage(game.Cover),
            game.Checksum,
            ToDateTimeOffset(game.UpdatedAt)
        );
    }

    private static IgdbImage? MapImage(IgdbCover? image)
    {
        if (image is null)
        {
            return null;
        }

        var imageId = string.IsNullOrWhiteSpace(image.ImageId) ? null : image.ImageId;
        return new IgdbImage(
            image.Id,
            imageId,
            image.Width,
            image.Height,
            NormalizeUrl(image.Url),
            BuildImageUrl(imageId, "cover_small_2x"),
            BuildImageUrl(imageId, "cover_big_2x"),
            image.Checksum
        );
    }

    private static string? BuildImageUrl(string? imageId, string size)
    {
        return imageId is null ? null : $"https://images.igdb.com/igdb/image/upload/t_{size}/{imageId}.jpg";
    }

    private static string? NormalizeUrl(string? url)
    {
        return url?.StartsWith("//", StringComparison.Ordinal) == true ? $"https:{url}" : url;
    }

    private static bool HasIdentity(IgdbGame game)
    {
        return game.Id > 0 && HasName(game.Name);
    }

    private static bool HasName(string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    private static DateTimeOffset? ToDateTimeOffset(long? unixSeconds)
    {
        return unixSeconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(unixSeconds.Value) : null;
    }

    private sealed record RankedPopularityPrimitive(IgdbPopularityPrimitive Primitive, int SourceRank);
}
