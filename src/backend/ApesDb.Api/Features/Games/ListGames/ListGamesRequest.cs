namespace ApesDb.Api.Features.Games.ListGames;

public sealed class ListGamesRequest
{
    public long[]? GameTypeIds { get; init; }

    public long[]? GameStatusIds { get; init; }

    public long[]? GenreIds { get; init; }

    public long[]? ThemeIds { get; init; }

    public long[]? GameModeIds { get; init; }

    public long[]? PlayerPerspectiveIds { get; init; }

    public long[]? PlatformIds { get; init; }

    public string? Developer { get; init; }

    public string? Publisher { get; init; }

    public string? Collection { get; init; }

    public string? Franchise { get; init; }

    public string? Search { get; init; }

    public bool? IsCoop { get; init; }

    public bool? IsSteam { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}
