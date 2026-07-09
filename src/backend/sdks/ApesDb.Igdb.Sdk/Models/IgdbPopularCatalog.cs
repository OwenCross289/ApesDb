namespace ApesDb.Igdb.Sdk.Models;

public sealed record IgdbPopularCatalog(
    IgdbPopularityType PopularityType,
    IgdbGameType MainGameType,
    IReadOnlyList<IgdbPopularGame> PopularGames,
    IReadOnlyList<IgdbCatalogGame> Games,
    IReadOnlyList<IgdbGameType> GameTypes,
    IReadOnlyList<IgdbGameStatus> GameStatuses,
    IReadOnlyList<IgdbGameRelation> Relations,
    IReadOnlyList<IgdbGenre> Genres,
    IReadOnlyList<IgdbGameGenre> GameGenres,
    IReadOnlyList<IgdbTheme> Themes,
    IReadOnlyList<IgdbGameTheme> GameThemes,
    IReadOnlyList<IgdbGameMode> GameModes,
    IReadOnlyList<IgdbGameGameMode> GameGameModes,
    IReadOnlyList<IgdbPlayerPerspective> PlayerPerspectives,
    IReadOnlyList<IgdbGamePlayerPerspective> GamePlayerPerspectives,
    IReadOnlyList<IgdbPlatform> Platforms,
    IReadOnlyList<IgdbPlatformType> PlatformTypes,
    IReadOnlyList<IgdbGamePlatform> GamePlatforms,
    IReadOnlyList<IgdbPlatformLink> PlatformLinks,
    IReadOnlyList<IgdbWebsiteType> WebsiteTypes,
    IReadOnlyList<IgdbExternalGameSource> ExternalGameSources,
    IReadOnlyList<IgdbExternalGameIdentifier> ExternalGameIdentifiers,
    IReadOnlyList<IgdbCompany> Companies,
    IReadOnlyList<IgdbGameCompany> GameCompanies,
    IReadOnlyList<IgdbCollection> Collections,
    IReadOnlyList<IgdbGameCollection> GameCollections,
    IReadOnlyList<IgdbFranchise> Franchises,
    IReadOnlyList<IgdbGameFranchise> GameFranchises
);

public sealed record IgdbPopularityType(
    long Id,
    string Name,
    long? ExternalPopularitySourceId,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPopularGame(
    long PopularityPrimitiveId,
    int Rank,
    int SourceRank,
    long GameId,
    decimal Score,
    DateTimeOffset? CalculatedAt,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbCatalogGame(
    long Id,
    string Name,
    string? Slug,
    string? Summary,
    string? Storyline,
    DateTimeOffset? FirstReleaseDate,
    double? TotalRating,
    long? TotalRatingCount,
    string? Url,
    long? GameTypeId,
    long? GameStatusId,
    IgdbImage? Cover,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbImage(
    long Id,
    string? ImageId,
    int? Width,
    int? Height,
    string? SourceUrl,
    string? SmallUrl,
    string? LargeUrl,
    Guid? Checksum
);

public sealed record IgdbGameType(long Id, string Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbGameStatus(long Id, string Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public enum IgdbGameRelationType
{
    Dlc,
    Expansion,
    StandaloneExpansion,
}

public sealed record IgdbGameRelation(long GameId, long RelatedGameId, IgdbGameRelationType RelationType);

public sealed record IgdbGenre(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameGenre(long GameId, long GenreId);

public sealed record IgdbTheme(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameTheme(long GameId, long ThemeId);

public sealed record IgdbGameMode(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameGameMode(long GameId, long GameModeId);

public sealed record IgdbPlayerPerspective(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGamePlayerPerspective(long GameId, long PlayerPerspectiveId);

public sealed record IgdbPlatform(
    long Id,
    string Name,
    string? Abbreviation,
    string? AlternativeName,
    string? Slug,
    long? PlatformTypeId,
    int? Generation,
    string? Summary,
    string? Url,
    IgdbImage? Logo,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPlatformType(long Id, string Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbGamePlatform(long GameId, long PlatformId);

public sealed record IgdbPlatformLink(
    long Id,
    long PlatformId,
    long WebsiteTypeId,
    string? Url,
    bool Trusted,
    Guid? Checksum
);

public sealed record IgdbWebsiteType(long Id, string Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbExternalGameSource(long Id, string Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbExternalGameIdentifier(
    long Id,
    long GameId,
    long SourceId,
    long? PlatformId,
    string ExternalId,
    string? Name,
    string? Url,
    int? Year,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbCompany(
    long Id,
    string Name,
    string? Slug,
    string? Description,
    int? CountryCode,
    string? Url,
    IgdbImage? Logo,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameCompany(
    long Id,
    long GameId,
    long CompanyId,
    bool Developer,
    bool Publisher,
    bool Porting,
    bool Supporting,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbCollection(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameCollection(long GameId, long CollectionId);

public sealed record IgdbFranchise(
    long Id,
    string Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameFranchise(long GameId, long FranchiseId);
