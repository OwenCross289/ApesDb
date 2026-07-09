using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbCover([property: JsonPropertyName("image_id")] string? ImageId);
