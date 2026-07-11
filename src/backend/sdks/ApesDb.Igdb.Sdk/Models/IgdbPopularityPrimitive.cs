using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbPopularityPrimitiveResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("game_id")] long GameId,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("popularity_type")] long PopularityTypeId,
    [property: JsonPropertyName("external_popularity_source")] long? ExternalPopularitySourceId,
    [property: JsonPropertyName("calculated_at")] long? CalculatedAt,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);
