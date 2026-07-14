using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Models;

internal sealed record IgdbGameResource(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("storyline")] string? Storyline,
    [property: JsonPropertyName("total_rating")] double? TotalRating,
    [property: JsonPropertyName("total_rating_count")] long? TotalRatingCount,
    [property: JsonPropertyName("first_release_date")] long? FirstReleaseDate,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("game_type")] long? GameTypeId,
    [property: JsonPropertyName("game_status")] long? GameStatusId,
    [property: JsonPropertyName("cover")] IgdbCover? Cover,
    [property: JsonPropertyName("dlcs")] IReadOnlyList<long>? DlcIds,
    [property: JsonPropertyName("expansions")] IReadOnlyList<long>? ExpansionIds,
    [property: JsonPropertyName("standalone_expansions")] IReadOnlyList<long>? StandaloneExpansionIds,
    [property: JsonPropertyName("genres")] IReadOnlyList<long>? GenreIds,
    [property: JsonPropertyName("themes")] IReadOnlyList<long>? ThemeIds,
    [property: JsonPropertyName("game_modes")] IReadOnlyList<long>? GameModeIds,
    [property: JsonPropertyName("player_perspectives")] IReadOnlyList<long>? PlayerPerspectiveIds,
    [property: JsonPropertyName("platforms")] IReadOnlyList<long>? PlatformIds,
    [property: JsonPropertyName("collections")] IReadOnlyList<long>? CollectionIds,
    [property: JsonPropertyName("franchise")] long? FranchiseId,
    [property: JsonPropertyName("franchises")] IReadOnlyList<long>? FranchiseIds,
    [property: JsonPropertyName("checksum")] Guid? Checksum,
    [property: JsonPropertyName("updated_at")] long? UpdatedAt
);
