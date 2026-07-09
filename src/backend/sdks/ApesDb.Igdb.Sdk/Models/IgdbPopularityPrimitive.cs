using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbPopularityPrimitive(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("game_id")] long GameId,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("popularity_type")] long PopularityType,
    [property: JsonPropertyName("calculated_at")] long? CalculatedAt,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);
