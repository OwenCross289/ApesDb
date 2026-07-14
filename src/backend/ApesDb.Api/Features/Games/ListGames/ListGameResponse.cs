using ApesDb.Api.Features.Games.Types;

namespace ApesDb.Api.Features.Games.ListGames;

public sealed record ListGameResponse(
    long Id,
    string? CoverSmallUrl,
    string Name,
    IReadOnlyList<string> Developers,
    IReadOnlyList<string> Publishers,
    bool IsCoop,
    bool IsSteam,
    GameTypeResponse? GameType
);
