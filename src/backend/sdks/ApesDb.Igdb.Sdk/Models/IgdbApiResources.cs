using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbPopularityTypeResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("external_popularity_source")] long? ExternalPopularitySourceId,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbGameTypeResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("type")] string? Name,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbGameStatusResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("status")] string? Name,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbNamedResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbWebsiteTypeResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("type")] string? Name,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbPlatformResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("abbreviation")] string? Abbreviation,
    [property: JsonPropertyName("alternative_name")] string? AlternativeName,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("platform_type")] long? PlatformTypeId,
    [property: JsonPropertyName("generation")] int? Generation,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("platform_logo")] IgdbCover? Logo,
    [property: JsonPropertyName("websites")] IReadOnlyList<long>? WebsiteIds,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbPlatformWebsiteResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("type")] long WebsiteTypeId,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("trusted")] bool Trusted,
    [property: JsonPropertyName("checksum")] Guid? Checksum
);

internal sealed record IgdbExternalGameSourceResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbExternalGameResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("game")] long GameId,
    [property: JsonPropertyName("external_game_source")] long SourceId,
    [property: JsonPropertyName("platform")] long? PlatformId,
    [property: JsonPropertyName("uid")] string? ExternalId,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("year")] int? Year,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbInvolvedCompanyResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("game")] long GameId,
    [property: JsonPropertyName("company")] long CompanyId,
    [property: JsonPropertyName("developer")] bool Developer,
    [property: JsonPropertyName("publisher")] bool Publisher,
    [property: JsonPropertyName("porting")] bool Porting,
    [property: JsonPropertyName("supporting")] bool Supporting,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);

internal sealed record IgdbCompanyResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("country")] int? CountryCode,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("logo")] IgdbCover? Logo,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);
