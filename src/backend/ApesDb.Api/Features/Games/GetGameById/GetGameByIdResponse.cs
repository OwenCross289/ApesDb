namespace ApesDb.Api.Features.Games.GetGameById;

public sealed record GetGameByIdResponse(
    long Id,
    string Name,
    string? Slug,
    string? Description,
    string? Storyline,
    DateTime? ReleaseDate,
    decimal? TotalRating,
    long? TotalRatingCount,
    string? IgdbUrl,
    GameReferenceResponse? GameType,
    GameReferenceResponse? GameStatus,
    long? VersionParentId,
    GameCoverResponse? Cover,
    GamePopularityResponse? Popularity,
    IReadOnlyList<GameReferenceResponse> Developers,
    IReadOnlyList<GameReferenceResponse> Publishers,
    IReadOnlyList<GameReferenceResponse> PortingCompanies,
    IReadOnlyList<GameReferenceResponse> SupportingCompanies,
    IReadOnlyList<GameStorePageResponse> StorePages,
    IReadOnlyList<GameEditionResponse> Editions,
    IReadOnlyList<GameAddonResponse> Addons,
    IReadOnlyList<GameReferenceResponse> Genres,
    IReadOnlyList<GameReferenceResponse> Themes,
    IReadOnlyList<GameReferenceResponse> GameModes,
    IReadOnlyList<GameReferenceResponse> GameEngines,
    IReadOnlyList<GameReferenceResponse> PlayerPerspectives,
    IReadOnlyList<GameReferenceResponse> Platforms,
    IReadOnlyList<GameReferenceResponse> Collections,
    IReadOnlyList<GameReferenceResponse> Franchises
);

public sealed record GameReferenceResponse(long Id, string Name);

public sealed record GameCoverResponse(string? ImageId, int? Width, int? Height, string? SmallUrl, string? LargeUrl);

public sealed record GamePopularityResponse(
    int Rank,
    int SourceRank,
    decimal Score,
    GameReferenceResponse Type,
    DateTime CalculatedAt
);

public sealed record GameStorePageResponse(
    long Id,
    GameReferenceResponse Source,
    GameReferenceResponse? Platform,
    string? ExternalId,
    string? Name,
    string? Url,
    int? Year
);

public sealed record GameEditionResponse(
    long Id,
    string Name,
    string? Description,
    DateTime? ReleaseDate,
    string? CoverSmallUrl,
    string? CoverLargeUrl
);

public sealed record GameAddonResponse(
    string Type,
    long Id,
    string Name,
    string? Description,
    DateTime? ReleaseDate,
    string? CoverSmallUrl,
    string? CoverLargeUrl
);
