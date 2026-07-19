using ApesDb.Api.Features.Games.Types;

namespace ApesDb.Api.Features.Games.GetGames;

public sealed record GetGameResponse(
    long Id,
    string? CoverSmallUrl,
    string? CoverLargeUrl,
    string Name,
    IReadOnlyList<string> Developers,
    IReadOnlyList<string> Publishers,
    bool IsCoop,
    bool IsSteam,
    GameTypeResponse? GameType
);
