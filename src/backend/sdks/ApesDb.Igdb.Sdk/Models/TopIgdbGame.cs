namespace ApesDb.Igdb.Sdk.Models;

public sealed record TopIgdbGame(
    int Rank,
    long Id,
    string Name,
    string? Slug,
    string? Summary,
    double? TotalRating,
    DateTimeOffset? FirstReleaseDate,
    string? CoverImageId,
    double Popularity
);
