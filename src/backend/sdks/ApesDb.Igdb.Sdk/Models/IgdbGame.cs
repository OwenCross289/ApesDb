using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbGame(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("total_rating")] double? TotalRating,
    [property: JsonPropertyName("first_release_date")] long? FirstReleaseDate,
    [property: JsonPropertyName("cover")] IgdbCover? Cover
);
