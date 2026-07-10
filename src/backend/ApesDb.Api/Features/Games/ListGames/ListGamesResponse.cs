namespace ApesDb.Api.Features.Games.ListGames;

public sealed record ListGamesResponse(IReadOnlyList<ListGameResponse> Items, int Page, int PageSize, int TotalCount);

public sealed record ListGameResponse(
    long Id,
    string? CoverSmallUrl,
    string Name,
    IReadOnlyList<string> Developers,
    IReadOnlyList<string> Publishers,
    bool IsCoop,
    bool IsSteam,
    GameKind Kind
);
