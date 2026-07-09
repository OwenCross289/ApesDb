using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbPopularityPrimitive(
    [property: JsonPropertyName("game_id")] long GameId,
    [property: JsonPropertyName("value")] double Value,
    [property: JsonPropertyName("popularity_type")] int PopularityType
);
