namespace ApesDb.Api.Features.Games.TopGames;

public sealed record TopGameResponse(
    int Rank,
    long Id,
    string Name,
    string? Slug,
    string? Summary,
    double? TotalRating,
    DateTimeOffset? FirstReleaseDate,
    string? CoverImageId,
    string? CoverSmallUrl,
    string? CoverLargeUrl,
    double Popularity
);
