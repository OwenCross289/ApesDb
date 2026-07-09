using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using ApesDb.Igdb.Sdk.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Worker.Games;

public sealed class PopularGamesCatalogImporter : IPopularGamesCatalogImporter
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<PopularGamesCatalogImporter> _logger;

    public PopularGamesCatalogImporter(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        ILogger<PopularGamesCatalogImporter> logger
    )
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task ImportAsync(IgdbPopularCatalog catalog, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ValidateCatalog(catalog);

        var synchronizedAt = _dateTimeProvider.UtcNow;

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var gameTypeIds = await UpsertIgdbEntitiesAsync<IgdbGameType, GameType>(
            catalog.GameTypes.Append(catalog.MainGameType),
            value => value.Id,
            (value, id) =>
                new GameType
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbGameType)),
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var gameStatusIds = await UpsertIgdbEntitiesAsync<IgdbGameStatus, GameStatus>(
            catalog.GameStatuses,
            value => value.Id,
            (value, id) =>
                new GameStatus
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbGameStatus)),
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var genreIds = await UpsertIgdbEntitiesAsync<IgdbGenre, Genre>(
            catalog.Genres,
            value => value.Id,
            (value, id) =>
                new Genre
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbGenre)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var themeIds = await UpsertIgdbEntitiesAsync<IgdbTheme, Theme>(
            catalog.Themes,
            value => value.Id,
            (value, id) =>
                new Theme
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbTheme)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var gameModeIds = await UpsertIgdbEntitiesAsync<IgdbGameMode, GameMode>(
            catalog.GameModes,
            value => value.Id,
            (value, id) =>
                new GameMode
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbGameMode)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var playerPerspectiveIds = await UpsertIgdbEntitiesAsync<IgdbPlayerPerspective, PlayerPerspective>(
            catalog.PlayerPerspectives,
            value => value.Id,
            (value, id) =>
                new PlayerPerspective
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbPlayerPerspective)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );

        var platformTypeNames = catalog
            .PlatformTypes.DistinctBy(value => value.Id)
            .ToDictionary(value => value.Id, value => value.Name);
        var platformIds = await UpsertIgdbEntitiesAsync<IgdbPlatform, Platform>(
            catalog.Platforms,
            value => value.Id,
            (value, id) =>
                new Platform
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbPlatform)),
                    Abbreviation = value.Abbreviation,
                    AlternativeName = value.AlternativeName,
                    Slug = value.Slug,
                    Summary = value.Summary,
                    IgdbUrl = value.Url,
                    IgdbPlatformTypeId = value.PlatformTypeId,
                    PlatformTypeName = value.PlatformTypeId is { } platformTypeId
                        ? platformTypeNames.GetValueOrDefault(platformTypeId)
                        : null,
                    Generation = value.Generation,
                    LogoImageId = value.Logo?.ImageId,
                    LogoWidth = value.Logo?.Width,
                    LogoHeight = value.Logo?.Height,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var externalSourceIds = await UpsertIgdbEntitiesAsync<IgdbExternalGameSource, ExternalGameSource>(
            catalog.ExternalGameSources,
            value => value.Id,
            (value, id) =>
                new ExternalGameSource
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbExternalGameSource)),
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var companyIds = await UpsertIgdbEntitiesAsync<IgdbCompany, Company>(
            catalog.Companies,
            value => value.Id,
            (value, id) =>
                new Company
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbCompany)),
                    Slug = value.Slug,
                    Description = value.Description,
                    CountryCode = value.CountryCode,
                    IgdbUrl = value.Url,
                    LogoImageId = value.Logo?.ImageId,
                    LogoWidth = value.Logo?.Width,
                    LogoHeight = value.Logo?.Height,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var collectionIds = await UpsertIgdbEntitiesAsync<IgdbCollection, Collection>(
            catalog.Collections,
            value => value.Id,
            (value, id) =>
                new Collection
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbCollection)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        var franchiseIds = await UpsertIgdbEntitiesAsync<IgdbFranchise, Franchise>(
            catalog.Franchises,
            value => value.Id,
            (value, id) =>
                new Franchise
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbFranchise)),
                    Slug = value.Slug,
                    IgdbUrl = value.Url,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );

        var gameIds = await UpsertIgdbEntitiesAsync<IgdbCatalogGame, Game>(
            catalog.Games,
            value => value.Id,
            (value, id) =>
                new Game
                {
                    Id = id,
                    IgdbId = value.Id,
                    Name = Required(value.Name, nameof(IgdbCatalogGame)),
                    Slug = value.Slug,
                    Summary = value.Summary,
                    Storyline = value.Storyline,
                    FirstReleaseDate = ToUtcDateTime(value.FirstReleaseDate),
                    TotalRating = value.TotalRating.HasValue ? Convert.ToDecimal(value.TotalRating.Value) : null,
                    TotalRatingCount = value.TotalRatingCount,
                    IgdbUrl = value.Url,
                    GameTypeId = ResolveOptional(gameTypeIds, value.GameTypeId),
                    GameStatusId = ResolveOptional(gameStatusIds, value.GameStatusId),
                    CoverImageId = value.Cover?.ImageId,
                    CoverWidth = value.Cover?.Width,
                    CoverHeight = value.Cover?.Height,
                    CoverSmallUrl = value.Cover?.SmallUrl,
                    CoverLargeUrl = value.Cover?.LargeUrl,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );

        await ReconcileRelationshipsAsync(
            catalog,
            gameIds,
            genreIds,
            themeIds,
            gameModeIds,
            playerPerspectiveIds,
            platformIds,
            externalSourceIds,
            companyIds,
            collectionIds,
            franchiseIds,
            synchronizedAt,
            cancellationToken
        );
        await ReplacePopularityAsync(catalog, gameIds, synchronizedAt, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        _logger.LogInformation(
            "Persisted {GameCount} IGDB games and replaced {PopularGameCount} popularity entries.",
            gameIds.Count,
            catalog.PopularGames.Count
        );
    }

    private async Task ReconcileRelationshipsAsync(
        IgdbPopularCatalog catalog,
        IReadOnlyDictionary<long, Guid> gameIds,
        IReadOnlyDictionary<long, Guid> genreIds,
        IReadOnlyDictionary<long, Guid> themeIds,
        IReadOnlyDictionary<long, Guid> gameModeIds,
        IReadOnlyDictionary<long, Guid> playerPerspectiveIds,
        IReadOnlyDictionary<long, Guid> platformIds,
        IReadOnlyDictionary<long, Guid> externalSourceIds,
        IReadOnlyDictionary<long, Guid> companyIds,
        IReadOnlyDictionary<long, Guid> collectionIds,
        IReadOnlyDictionary<long, Guid> franchiseIds,
        DateTime synchronizedAt,
        CancellationToken cancellationToken
    )
    {
        var touchedGameIds = gameIds.Values.ToArray();
        var touchedPlatformIds = platformIds.Values.ToArray();
        var platformLinks = catalog
            .PlatformLinks.Where(value => !string.IsNullOrWhiteSpace(value.Url))
            .DistinctBy(value => value.Id)
            .ToArray();
        var externalIdentifiers = catalog
            .ExternalGameIdentifiers.Where(value => !string.IsNullOrWhiteSpace(value.ExternalId))
            .DistinctBy(value => value.Id)
            .ToArray();
        var gameCompanies = catalog
            .GameCompanies.GroupBy(value => (value.GameId, value.CompanyId))
            .Select(group =>
            {
                var value = group.First();
                return value with
                {
                    Developer = group.Any(item => item.Developer),
                    Publisher = group.Any(item => item.Publisher),
                    Porting = group.Any(item => item.Porting),
                    Supporting = group.Any(item => item.Supporting),
                };
            })
            .Where(value => value.Developer || value.Publisher || value.Porting || value.Supporting)
            .ToArray();

        await _dbContext
            .GameRelations.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GameGenres.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GameThemes.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GameGameModes.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GamePlayerPerspectives.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GamePlatforms.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GameCollections.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);
        await _dbContext
            .GameFranchises.Where(value => touchedGameIds.Contains(value.GameId))
            .ExecuteDeleteAsync(cancellationToken);

        var platformLinkIgdbIds = platformLinks.Select(value => value.Id).ToArray();
        var stalePlatformLinks = _dbContext.PlatformLinks.Where(value => touchedPlatformIds.Contains(value.PlatformId));
        if (platformLinkIgdbIds.Length > 0)
        {
            stalePlatformLinks = stalePlatformLinks.Where(value => !platformLinkIgdbIds.Contains(value.IgdbId));
        }

        await stalePlatformLinks.ExecuteDeleteAsync(cancellationToken);

        var externalIdentifierIgdbIds = externalIdentifiers.Select(value => value.Id).ToArray();
        var staleExternalIdentifiers = _dbContext.GameExternalIdentifiers.Where(value =>
            touchedGameIds.Contains(value.GameId)
        );
        if (externalIdentifierIgdbIds.Length > 0)
        {
            staleExternalIdentifiers = staleExternalIdentifiers.Where(value =>
                !externalIdentifierIgdbIds.Contains(value.IgdbId)
            );
        }

        await staleExternalIdentifiers.ExecuteDeleteAsync(cancellationToken);

        var gameCompanyIgdbIds = gameCompanies.Select(value => value.Id).ToArray();
        var staleGameCompanies = _dbContext.GameCompanies.Where(value => touchedGameIds.Contains(value.GameId));
        if (gameCompanyIgdbIds.Length > 0)
        {
            staleGameCompanies = staleGameCompanies.Where(value => !gameCompanyIgdbIds.Contains(value.IgdbId));
        }

        await staleGameCompanies.ExecuteDeleteAsync(cancellationToken);

        await BulkInsertAsync(
            catalog
                .Relations.DistinctBy(value => (value.GameId, value.RelatedGameId, value.RelationType))
                .Select(value => new GameRelation
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameRelation.GameId)),
                    RelatedGameId = ResolveRequired(
                        gameIds,
                        value.RelatedGameId,
                        nameof(IgdbGameRelation.RelatedGameId)
                    ),
                    RelationType = value.RelationType switch
                    {
                        IgdbGameRelationType.Dlc => GameRelationType.Dlc,
                        IgdbGameRelationType.Expansion => GameRelationType.Expansion,
                        IgdbGameRelationType.StandaloneExpansion => GameRelationType.StandaloneExpansion,
                        _ => throw new ArgumentOutOfRangeException(
                            nameof(value.RelationType),
                            value.RelationType,
                            "Unsupported IGDB game relation type."
                        ),
                    },
                    CreatedAt = synchronizedAt,
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GameGenres.DistinctBy(value => (value.GameId, value.GenreId))
                .Select(value => new GameGenre
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameGenre.GameId)),
                    GenreId = ResolveRequired(genreIds, value.GenreId, nameof(IgdbGameGenre.GenreId)),
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GameThemes.DistinctBy(value => (value.GameId, value.ThemeId))
                .Select(value => new GameTheme
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameTheme.GameId)),
                    ThemeId = ResolveRequired(themeIds, value.ThemeId, nameof(IgdbGameTheme.ThemeId)),
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GameGameModes.DistinctBy(value => (value.GameId, value.GameModeId))
                .Select(value => new GameGameMode
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameGameMode.GameId)),
                    GameModeId = ResolveRequired(gameModeIds, value.GameModeId, nameof(IgdbGameGameMode.GameModeId)),
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GamePlayerPerspectives.DistinctBy(value => (value.GameId, value.PlayerPerspectiveId))
                .Select(value => new GamePlayerPerspective
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGamePlayerPerspective.GameId)),
                    PlayerPerspectiveId = ResolveRequired(
                        playerPerspectiveIds,
                        value.PlayerPerspectiveId,
                        nameof(IgdbGamePlayerPerspective.PlayerPerspectiveId)
                    ),
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GamePlatforms.DistinctBy(value => (value.GameId, value.PlatformId))
                .Select(value => new GamePlatform
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGamePlatform.GameId)),
                    PlatformId = ResolveRequired(platformIds, value.PlatformId, nameof(IgdbGamePlatform.PlatformId)),
                })
                .ToList(),
            cancellationToken
        );

        var websiteTypeNames = catalog
            .WebsiteTypes.DistinctBy(value => value.Id)
            .ToDictionary(value => value.Id, value => value.Name);
        await UpsertIgdbEntitiesAsync<IgdbPlatformLink, PlatformLink>(
            platformLinks,
            value => value.Id,
            (value, id) =>
                new PlatformLink
                {
                    Id = id,
                    IgdbId = value.Id,
                    PlatformId = ResolveRequired(platformIds, value.PlatformId, nameof(IgdbPlatformLink.PlatformId)),
                    IgdbWebsiteTypeId = value.WebsiteTypeId,
                    WebsiteTypeName = websiteTypeNames.GetValueOrDefault(value.WebsiteTypeId),
                    Url = value.Url!,
                    Trusted = value.Trusted,
                    Checksum = value.Checksum,
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        await UpsertIgdbEntitiesAsync<IgdbExternalGameIdentifier, GameExternalIdentifier>(
            externalIdentifiers,
            value => value.Id,
            (value, id) =>
                new GameExternalIdentifier
                {
                    Id = id,
                    IgdbId = value.Id,
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbExternalGameIdentifier.GameId)),
                    ExternalGameSourceId = ResolveRequired(
                        externalSourceIds,
                        value.SourceId,
                        nameof(IgdbExternalGameIdentifier.SourceId)
                    ),
                    PlatformId = ResolveOptional(platformIds, value.PlatformId),
                    ExternalId = value.ExternalId,
                    Name = value.Name,
                    Url = value.Url,
                    Year = value.Year,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        await UpsertIgdbEntitiesAsync<IgdbGameCompany, GameCompany>(
            gameCompanies,
            value => value.Id,
            (value, id) =>
                new GameCompany
                {
                    Id = id,
                    IgdbId = value.Id,
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameCompany.GameId)),
                    CompanyId = ResolveRequired(companyIds, value.CompanyId, nameof(IgdbGameCompany.CompanyId)),
                    Developer = value.Developer,
                    Publisher = value.Publisher,
                    Porting = value.Porting,
                    Supporting = value.Supporting,
                    Checksum = value.Checksum,
                    IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                    CreatedAt = synchronizedAt,
                    UpdatedAt = synchronizedAt,
                    LastSyncedAt = synchronizedAt,
                },
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GameCollections.DistinctBy(value => (value.GameId, value.CollectionId))
                .Select(value => new GameCollection
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameCollection.GameId)),
                    CollectionId = ResolveRequired(
                        collectionIds,
                        value.CollectionId,
                        nameof(IgdbGameCollection.CollectionId)
                    ),
                })
                .ToList(),
            cancellationToken
        );
        await BulkInsertAsync(
            catalog
                .GameFranchises.DistinctBy(value => (value.GameId, value.FranchiseId))
                .Select(value => new GameFranchise
                {
                    GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbGameFranchise.GameId)),
                    FranchiseId = ResolveRequired(
                        franchiseIds,
                        value.FranchiseId,
                        nameof(IgdbGameFranchise.FranchiseId)
                    ),
                })
                .ToList(),
            cancellationToken
        );
    }

    private async Task ReplacePopularityAsync(
        IgdbPopularCatalog catalog,
        IReadOnlyDictionary<long, Guid> gameIds,
        DateTime synchronizedAt,
        CancellationToken cancellationToken
    )
    {
        var popularity = catalog
            .PopularGames.OrderBy(value => value.Rank)
            .Select(value => new PopularGame
            {
                GameId = ResolveRequired(gameIds, value.GameId, nameof(IgdbPopularGame.GameId)),
                Rank = value.Rank,
                SourceRank = value.SourceRank,
                Score = value.Score,
                IgdbPopularityTypeId = catalog.PopularityType.Id,
                CalculatedAt = ToUtcDateTime(value.CalculatedAt) ?? synchronizedAt,
                IgdbUpdatedAt = ToUtcDateTime(value.UpdatedAt),
                Checksum = value.Checksum,
                SyncedAt = synchronizedAt,
            })
            .ToList();

        await _dbContext.PopularGames.ExecuteDeleteAsync(cancellationToken);
        await BulkInsertAsync(popularity, cancellationToken);
    }

    private async Task<Dictionary<long, Guid>> UpsertIgdbEntitiesAsync<TSource, TEntity>(
        IEnumerable<TSource> source,
        Func<TSource, long> igdbIdSelector,
        Func<TSource, Guid, TEntity> entityFactory,
        CancellationToken cancellationToken
    )
        where TEntity : class, IIgdbEntity
    {
        var normalized = source.DistinctBy(igdbIdSelector).ToArray();
        if (normalized.Length == 0)
        {
            return [];
        }

        var igdbIds = normalized.Select(igdbIdSelector).ToArray();
        var entityIds = await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(value => igdbIds.Contains(value.IgdbId))
            .ToDictionaryAsync(value => value.IgdbId, value => value.Id, cancellationToken);

        var entities = new List<TEntity>(normalized.Length);
        foreach (var value in normalized)
        {
            var igdbId = igdbIdSelector(value);
            if (!entityIds.TryGetValue(igdbId, out var entityId))
            {
                entityId = Guid.CreateVersion7();
                entityIds.Add(igdbId, entityId);
            }

            entities.Add(entityFactory(value, entityId));
        }

        await _dbContext.BulkInsertOrUpdateAsync(
            entities,
            new BulkConfig { PropertiesToExcludeOnUpdate = [nameof(IIgdbEntity.CreatedAt)] },
            cancellationToken: cancellationToken
        );

        return entityIds;
    }

    private async Task BulkInsertAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken)
        where TEntity : class
    {
        if (entities.Count == 0)
        {
            return;
        }

        await _dbContext.BulkInsertAsync(entities, cancellationToken: cancellationToken);
    }

    private static void ValidateCatalog(IgdbPopularCatalog catalog)
    {
        if (catalog.PopularGames.Count == 0)
        {
            throw new InvalidDataException("The IGDB catalog did not contain any popular games.");
        }

        var expectedRanks = Enumerable.Range(1, catalog.PopularGames.Count);
        var actualRanks = catalog.PopularGames.Select(value => value.Rank).Order().ToArray();
        if (!actualRanks.SequenceEqual(expectedRanks))
        {
            throw new InvalidDataException("Popular-game ranks must be unique and contiguous, starting at one.");
        }

        if (catalog.PopularGames.Any(value => value.SourceRank <= 0 || value.Score < 0))
        {
            throw new InvalidDataException("Popular-game source ranks and scores must be non-negative.");
        }

        var popularGameIds = catalog.PopularGames.Select(value => value.GameId).ToArray();
        if (popularGameIds.Distinct().Count() != popularGameIds.Length)
        {
            throw new InvalidDataException("The IGDB catalog contains duplicate popular games.");
        }

        var gamesById = catalog.Games.ToLookup(value => value.Id);
        foreach (var popularGameId in popularGameIds)
        {
            var matchingGames = gamesById[popularGameId].ToArray();
            if (matchingGames.Length != 1 || string.IsNullOrWhiteSpace(matchingGames[0].Name))
            {
                throw new InvalidDataException(
                    $"Popular IGDB game {popularGameId} does not have exactly one hydrated game with a name."
                );
            }
        }
    }

    private static Guid ResolveRequired(IReadOnlyDictionary<long, Guid> entityIds, long igdbId, string propertyName)
    {
        return entityIds.TryGetValue(igdbId, out var entityId)
            ? entityId
            : throw new InvalidDataException($"{propertyName} references missing IGDB entity {igdbId}.");
    }

    private static Guid? ResolveOptional(IReadOnlyDictionary<long, Guid> entityIds, long? igdbId)
    {
        return igdbId is { } value && entityIds.TryGetValue(value, out var entityId) ? entityId : null;
    }

    private static string Required(string value, string resourceName)
    {
        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidDataException($"{resourceName} has a missing required name.");
    }

    private static DateTime? ToUtcDateTime(DateTimeOffset? value)
    {
        return value?.UtcDateTime;
    }
}
