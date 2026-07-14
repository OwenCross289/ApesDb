using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbCover(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("image_id")] string? ImageId,
    [property: JsonPropertyName("width")] int? Width,
    [property: JsonPropertyName("height")] int? Height,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("checksum")] Guid? Checksum
);
