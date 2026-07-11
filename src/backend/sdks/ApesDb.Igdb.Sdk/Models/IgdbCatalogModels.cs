namespace ApesDb.Igdb.Sdk.Models;

public sealed record IgdbSyncWindow(DateTimeOffset UpdatedAfter, DateTimeOffset UpdatedThrough);

public sealed record IgdbGameType(long Id, string? Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbGameStatus(long Id, string? Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbGenre(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbTheme(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbGameMode(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPlayerPerspective(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPlatformType(long Id, string? Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbWebsiteType(long Id, string? Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbPopularityType(
    long Id,
    string? Name,
    long? ExternalPopularitySourceId,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbExternalGameSource(long Id, string? Name, Guid? Checksum, DateTimeOffset? UpdatedAt);

public sealed record IgdbCompany(
    long Id,
    string? Name,
    string? Slug,
    string? Description,
    int? CountryCode,
    string? Url,
    IgdbImage? Logo,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbCollection(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbFranchise(
    long Id,
    string? Name,
    string? Slug,
    string? Url,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPlatform(
    long Id,
    string? Name,
    string? Abbreviation,
    string? AlternativeName,
    string? Slug,
    long? PlatformTypeId,
    int? Generation,
    string? Summary,
    string? Url,
    IgdbImage? Logo,
    IReadOnlyList<long> WebsiteIds,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPlatformWebsite(long Id, long WebsiteTypeId, string? Url, bool Trusted, Guid? Checksum);

public sealed record IgdbGame(
    long Id,
    string? Name,
    string? Slug,
    string? Summary,
    string? Storyline,
    double? TotalRating,
    long? TotalRatingCount,
    DateTimeOffset? FirstReleaseDate,
    string? Url,
    long? GameTypeId,
    long? GameStatusId,
    IgdbImage? Cover,
    IReadOnlyList<long> DlcIds,
    IReadOnlyList<long> ExpansionIds,
    IReadOnlyList<long> StandaloneExpansionIds,
    IReadOnlyList<long> GenreIds,
    IReadOnlyList<long> ThemeIds,
    IReadOnlyList<long> GameModeIds,
    IReadOnlyList<long> PlayerPerspectiveIds,
    IReadOnlyList<long> PlatformIds,
    IReadOnlyList<long> CollectionIds,
    long? FranchiseId,
    IReadOnlyList<long> FranchiseIds,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbInvolvedCompany(
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

public sealed record IgdbExternalGame(
    long Id,
    long GameId,
    long SourceId,
    long? PlatformId,
    string? Uid,
    string? Name,
    string? Url,
    int? Year,
    Guid? Checksum,
    DateTimeOffset? UpdatedAt
);

public sealed record IgdbPopularityPrimitive(
    long Id,
    long GameId,
    decimal Value,
    long PopularityTypeId,
    long? ExternalPopularitySourceId,
    DateTimeOffset? CalculatedAt,
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
