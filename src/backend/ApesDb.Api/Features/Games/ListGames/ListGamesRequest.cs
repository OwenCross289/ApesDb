namespace ApesDb.Api.Features.Games.ListGames;

public sealed class ListGamesRequest
{
    public long[]? GameTypeIds { get; set; }

    public long[]? GameStatusIds { get; set; }

    public long[]? GenreIds { get; set; }

    public long[]? ThemeIds { get; set; }

    public long[]? GameModeIds { get; set; }

    public long[]? PlayerPerspectiveIds { get; set; }

    public long[]? PlatformIds { get; set; }

    public string? Developer { get; set; }

    public string? Publisher { get; set; }

    public string? Collection { get; set; }

    public string? Franchise { get; set; }

    public string? Search { get; set; }

    public bool? IsCoop { get; set; }

    public bool? IsSteam { get; set; }

    public string[]? GameKinds { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;
}
