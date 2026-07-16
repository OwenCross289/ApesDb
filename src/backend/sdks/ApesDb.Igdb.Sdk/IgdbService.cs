using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

internal sealed class IgdbService : IIgdbService
{
    internal const int PageSize = 500;

    private const string NamedResourceFields = "id,name,slug,url,checksum,updated_at";
    private const string CoverFields = "cover.id,cover.image_id,cover.width,cover.height,cover.url,cover.checksum";
    private const string CompanyLogoFields = "logo.id,logo.image_id,logo.width,logo.height,logo.url,logo.checksum";
    private const string PlatformLogoFields =
        "platform_logo.id,platform_logo.image_id,platform_logo.width,platform_logo.height,"
        + "platform_logo.url,platform_logo.checksum";
    private const string GameFields =
        "id,name,slug,summary,storyline,total_rating,total_rating_count,first_release_date,url,"
        + "game_type,game_status,version_parent,"
        + CoverFields
        + ",dlcs,expansions,standalone_expansions,genres,themes,game_modes,player_perspectives,"
        + "platforms,collections,franchise,franchises,checksum,updated_at";

    private readonly IIgdbClient _client;

    public IgdbService(IIgdbClient client)
    {
        _client = client;
    }

    public Task<IReadOnlyList<IgdbGameType>> FetchGameTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbGameTypeResource, IgdbGameType>(
            "game_types",
            "id,type,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbGameType(
                resource.Id,
                resource.Name,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbGameStatus>> FetchGameStatusesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbGameStatusResource, IgdbGameStatus>(
            "game_statuses",
            "id,status,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbGameStatus(
                resource.Id,
                resource.Name,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbGenre>> FetchGenresPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbGenre>(
            "genres",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbGenre(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbTheme>> FetchThemesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbTheme>(
            "themes",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbTheme(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbGameMode>> FetchGameModesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbGameMode>(
            "game_modes",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbGameMode(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPlayerPerspective>> FetchPlayerPerspectivesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbPlayerPerspective>(
            "player_perspectives",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbPlayerPerspective(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPlatformType>> FetchPlatformTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbPlatformType>(
            "platform_types",
            "id,name,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbPlatformType(
                resource.Id,
                resource.Name,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbWebsiteType>> FetchWebsiteTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbWebsiteTypeResource, IgdbWebsiteType>(
            "website_types",
            "id,type,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbWebsiteType(
                resource.Id,
                resource.Name,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPopularityType>> FetchPopularityTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbPopularityTypeResource, IgdbPopularityType>(
            "popularity_types",
            "id,name,external_popularity_source,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbPopularityType(
                resource.Id,
                resource.Name,
                resource.ExternalPopularitySourceId,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbExternalGameSource>> FetchExternalGameSourcesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbExternalGameSourceResource, IgdbExternalGameSource>(
            "external_game_sources",
            "id,name,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbExternalGameSource(
                resource.Id,
                resource.Name,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbCompany>> FetchCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbCompanyResource, IgdbCompany>(
            "companies",
            "id,name,slug,description,country,url," + CompanyLogoFields + ",checksum,updated_at",
            afterId,
            window,
            resource => new IgdbCompany(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Description,
                resource.CountryCode,
                resource.Url,
                MapImage(resource.Logo),
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbCollection>> FetchCollectionsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbCollection>(
            "collections",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbCollection(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbFranchise>> FetchFranchisesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbNamedResource, IgdbFranchise>(
            "franchises",
            NamedResourceFields,
            afterId,
            window,
            resource => new IgdbFranchise(
                resource.Id,
                resource.Name,
                resource.Slug,
                resource.Url,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPlatform>> FetchPlatformsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbPlatformResource, IgdbPlatform>(
            "platforms",
            "id,name,abbreviation,alternative_name,slug,platform_type,generation,summary,url,"
                + PlatformLogoFields
                + ",websites,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbPlatform(
                resource.Id,
                resource.Name,
                resource.Abbreviation,
                resource.AlternativeName,
                resource.Slug,
                resource.PlatformTypeId,
                resource.Generation,
                resource.Summary,
                resource.Url,
                MapImage(resource.Logo),
                NormalizeIds(resource.WebsiteIds),
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPlatformWebsite>> FetchPlatformWebsitesPageAsync(
        long afterId,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbPlatformWebsiteResource, IgdbPlatformWebsite>(
            "platform_websites",
            "id,type,url,trusted,checksum",
            afterId,
            null,
            resource => new IgdbPlatformWebsite(
                resource.Id,
                resource.WebsiteTypeId,
                resource.Url,
                resource.Trusted,
                resource.Checksum
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbGame>> FetchGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbGameResource, IgdbGame>(
            "games",
            GameFields,
            afterId,
            window,
            MapGame,
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbInvolvedCompany>> FetchInvolvedCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbInvolvedCompanyResource, IgdbInvolvedCompany>(
            "involved_companies",
            "id,game,company,developer,publisher,porting,supporting,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbInvolvedCompany(
                resource.Id,
                resource.GameId,
                resource.CompanyId,
                resource.Developer,
                resource.Publisher,
                resource.Porting,
                resource.Supporting,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbExternalGame>> FetchExternalGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        return FetchPageAsync<IgdbExternalGameResource, IgdbExternalGame>(
            "external_games",
            "id,game,external_game_source,platform,uid,name,url,year,checksum,updated_at",
            afterId,
            window,
            resource => new IgdbExternalGame(
                resource.Id,
                resource.GameId,
                resource.SourceId,
                resource.PlatformId,
                resource.ExternalId,
                resource.Name,
                resource.Url,
                resource.Year,
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ),
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<IgdbPopularityPrimitive>> FetchPopularityPrimitivesPageAsync(
        long popularityTypeId,
        int offset = 0,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(popularityTypeId);
        ArgumentOutOfRangeException.ThrowIfNegative(offset);

        var query =
            "fields id,game_id,value,popularity_type,external_popularity_source,calculated_at,checksum,updated_at; "
            + $"where popularity_type = {popularityTypeId}; sort value desc; limit {PageSize}; offset {offset};";
        var resources = await _client.QueryAsync<IgdbPopularityPrimitiveResource>(
            "popularity_primitives",
            query,
            cancellationToken
        );

        return resources
            .Select(resource => new IgdbPopularityPrimitive(
                resource.Id,
                resource.GameId,
                resource.Value,
                resource.PopularityTypeId,
                resource.ExternalPopularitySourceId,
                ToDateTimeOffset(resource.CalculatedAt),
                resource.Checksum,
                ToDateTimeOffset(resource.UpdatedAt)
            ))
            .ToArray();
    }

    private async Task<IReadOnlyList<TResult>> FetchPageAsync<TResource, TResult>(
        string endpoint,
        string fields,
        long afterId,
        IgdbSyncWindow? window,
        Func<TResource, TResult> map,
        CancellationToken cancellationToken
    )
    {
        var query = BuildPageQuery(fields, afterId, window);
        var resources = await _client.QueryAsync<TResource>(endpoint, query, cancellationToken);
        return resources.Select(map).ToArray();
    }

    private static string BuildPageQuery(string fields, long afterId, IgdbSyncWindow? window)
    {
        if (afterId < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(afterId), afterId, "The IGDB page cursor cannot be below -1.");
        }

        var where = $"id > {afterId}";
        if (window is not null)
        {
            if (window.UpdatedAfter is { } updatedAfter && updatedAfter > window.UpdatedThrough)
            {
                throw new ArgumentException(
                    "The incremental synchronization boundary must not end before it starts.",
                    nameof(window)
                );
            }

            if (window.UpdatedAfter is { } lowerBoundary)
            {
                where += $" & updated_at > {lowerBoundary.ToUnixTimeSeconds()}";
                where += $" & updated_at <= {window.UpdatedThrough.ToUnixTimeSeconds()}";
            }
            else
            {
                where += $" & created_at <= {window.UpdatedThrough.ToUnixTimeSeconds()}";
            }
        }

        return $"fields {fields}; where {where}; sort id asc; limit {PageSize};";
    }

    private static IgdbGame MapGame(IgdbGameResource resource)
    {
        return new IgdbGame(
            resource.Id,
            resource.Name,
            resource.Slug,
            resource.Summary,
            resource.Storyline,
            resource.TotalRating,
            resource.TotalRatingCount,
            ToDateTimeOffset(resource.FirstReleaseDate),
            resource.Url,
            resource.GameTypeId,
            resource.GameStatusId,
            resource.VersionParentId,
            MapImage(resource.Cover),
            NormalizeIds(resource.DlcIds),
            NormalizeIds(resource.ExpansionIds),
            NormalizeIds(resource.StandaloneExpansionIds),
            NormalizeIds(resource.GenreIds),
            NormalizeIds(resource.ThemeIds),
            NormalizeIds(resource.GameModeIds),
            NormalizeIds(resource.PlayerPerspectiveIds),
            NormalizeIds(resource.PlatformIds),
            NormalizeIds(resource.CollectionIds),
            resource.FranchiseId,
            NormalizeIds(resource.FranchiseIds),
            resource.Checksum,
            ToDateTimeOffset(resource.UpdatedAt)
        );
    }

    private static IgdbImage? MapImage(IgdbCover? image)
    {
        if (image is null)
        {
            return null;
        }

        var imageId = image.ImageId;
        if (string.IsNullOrWhiteSpace(imageId))
        {
            imageId = null;
        }
        return new IgdbImage(
            image.Id,
            imageId,
            image.Width,
            image.Height,
            NormalizeUrl(image.Url),
            BuildImageUrl(imageId, "cover_small_2x"),
            BuildImageUrl(imageId, "cover_big_2x"),
            image.Checksum
        );
    }

    private static IReadOnlyList<long> NormalizeIds(IReadOnlyList<long>? ids)
    {
        return (ids ?? []).Where(id => id > 0).Distinct().ToArray();
    }

    private static string? BuildImageUrl(string? imageId, string size)
    {
        if (imageId is null)
        {
            return null;
        }

        return $"https://images.igdb.com/igdb/image/upload/t_{size}/{imageId}.jpg";
    }

    private static string? NormalizeUrl(string? url)
    {
        if (url?.StartsWith("//", StringComparison.Ordinal) == true)
        {
            return $"https:{url}";
        }

        return url;
    }

    private static DateTimeOffset? ToDateTimeOffset(long? unixSeconds)
    {
        if (!unixSeconds.HasValue)
        {
            return null;
        }

        return DateTimeOffset.FromUnixTimeSeconds(unixSeconds.Value);
    }
}
