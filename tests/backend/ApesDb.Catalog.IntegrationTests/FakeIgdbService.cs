using ApesDb.Igdb.Sdk;
using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Catalog.IntegrationTests;

internal sealed class FakeIgdbService : IIgdbService
{
    private const int PageSize = 500;

    public IReadOnlyList<IgdbGameType> GameTypes { get; set; } = [];

    public IReadOnlyList<IgdbGameStatus> GameStatuses { get; set; } = [];

    public IReadOnlyList<IgdbGenre> Genres { get; set; } = [];

    public IReadOnlyList<IgdbTheme> Themes { get; set; } = [];

    public IReadOnlyList<IgdbGameMode> GameModes { get; set; } = [];

    public IReadOnlyList<IgdbPlayerPerspective> PlayerPerspectives { get; set; } = [];

    public IReadOnlyList<IgdbPlatformType> PlatformTypes { get; set; } = [];

    public IReadOnlyList<IgdbWebsiteType> WebsiteTypes { get; set; } = [];

    public IReadOnlyList<IgdbPopularityType> PopularityTypes { get; set; } = [];

    public IReadOnlyList<IgdbExternalGameSource> ExternalGameSources { get; set; } = [];

    public IReadOnlyList<IgdbCompany> Companies { get; set; } = [];

    public IReadOnlyList<IgdbCollection> Collections { get; set; } = [];

    public IReadOnlyList<IgdbFranchise> Franchises { get; set; } = [];

    public IReadOnlyList<IgdbPlatform> Platforms { get; set; } = [];

    public IReadOnlyList<IgdbPlatformWebsite> PlatformWebsites { get; set; } = [];

    public IReadOnlyList<IgdbGame> Games { get; set; } = [];

    public IReadOnlyList<IgdbInvolvedCompany> InvolvedCompanies { get; set; } = [];

    public IReadOnlyList<IgdbExternalGame> ExternalGames { get; set; } = [];

    public IReadOnlyList<IgdbPopularityPrimitive> PopularityPrimitives { get; set; } = [];

    public Dictionary<long, DateTimeOffset?> GameCreatedAt { get; } = [];

    public Dictionary<long, DateTimeOffset?> InvolvedCompanyCreatedAt { get; } = [];

    public Dictionary<long, DateTimeOffset?> ExternalGameCreatedAt { get; } = [];

    public Func<
        long,
        IgdbSyncWindow?,
        CancellationToken,
        Task<IReadOnlyList<IgdbGenre>>
    >? GenresPageHandler { get; set; }

    public List<PageRequest> GenreRequests { get; } = [];

    public List<PageRequest> PlatformRequests { get; } = [];

    public List<PageRequest> GameRequests { get; } = [];

    public List<PageRequest> InvolvedCompanyRequests { get; } = [];

    public List<PageRequest> ExternalGameRequests { get; } = [];

    public Task<IReadOnlyList<IgdbGameType>> FetchGameTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(GameTypes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbGameStatus>> FetchGameStatusesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(GameStatuses, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbGenre>> FetchGenresPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        GenreRequests.Add(new PageRequest(afterId, window));
        if (GenresPageHandler is null)
        {
            return PageAsync(Genres, afterId, value => value.Id, cancellationToken);
        }

        return GenresPageHandler(afterId, window, cancellationToken);
    }

    public Task<IReadOnlyList<IgdbTheme>> FetchThemesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(Themes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbGameMode>> FetchGameModesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(GameModes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbPlayerPerspective>> FetchPlayerPerspectivesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(PlayerPerspectives, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbPlatformType>> FetchPlatformTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(PlatformTypes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbWebsiteType>> FetchWebsiteTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(WebsiteTypes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbPopularityType>> FetchPopularityTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(PopularityTypes, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbExternalGameSource>> FetchExternalGameSourcesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(ExternalGameSources, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbCompany>> FetchCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(Companies, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbCollection>> FetchCollectionsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(Collections, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbFranchise>> FetchFranchisesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    ) => PageAsync(Franchises, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbPlatform>> FetchPlatformsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        PlatformRequests.Add(new PageRequest(afterId, window));
        return PageAsync(Platforms, afterId, value => value.Id, cancellationToken);
    }

    public Task<IReadOnlyList<IgdbPlatformWebsite>> FetchPlatformWebsitesPageAsync(
        long afterId,
        CancellationToken cancellationToken = default
    ) => PageAsync(PlatformWebsites, afterId, value => value.Id, cancellationToken);

    public Task<IReadOnlyList<IgdbGame>> FetchGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        GameRequests.Add(new PageRequest(afterId, window));
        return WindowedPageAsync(
            Games,
            afterId,
            value => value.Id,
            value => value.UpdatedAt,
            value => GetCreatedAt(GameCreatedAt, value.Id, value.UpdatedAt),
            window,
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbInvolvedCompany>> FetchInvolvedCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        InvolvedCompanyRequests.Add(new PageRequest(afterId, window));
        return WindowedPageAsync(
            InvolvedCompanies,
            afterId,
            value => value.Id,
            value => value.UpdatedAt,
            value => GetCreatedAt(InvolvedCompanyCreatedAt, value.Id, value.UpdatedAt),
            window,
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbExternalGame>> FetchExternalGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    )
    {
        ExternalGameRequests.Add(new PageRequest(afterId, window));
        return WindowedPageAsync(
            ExternalGames,
            afterId,
            value => value.Id,
            value => value.UpdatedAt,
            value => GetCreatedAt(ExternalGameCreatedAt, value.Id, value.UpdatedAt),
            window,
            cancellationToken
        );
    }

    public Task<IReadOnlyList<IgdbPopularityPrimitive>> FetchPopularityPrimitivesPageAsync(
        long popularityTypeId,
        int offset = 0,
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();
        IReadOnlyList<IgdbPopularityPrimitive> page = PopularityPrimitives
            .Where(value => value.PopularityTypeId == popularityTypeId)
            .Skip(offset)
            .Take(PageSize)
            .ToArray();
        return Task.FromResult(page);
    }

    private static Task<IReadOnlyList<T>> PageAsync<T>(
        IReadOnlyList<T> values,
        long afterId,
        Func<T, long> idSelector,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();
        IReadOnlyList<T> page = values
            .Where(value => idSelector(value) > afterId)
            .OrderBy(idSelector)
            .Take(PageSize)
            .ToArray();
        return Task.FromResult(page);
    }

    private static Task<IReadOnlyList<T>> WindowedPageAsync<T>(
        IReadOnlyList<T> values,
        long afterId,
        Func<T, long> idSelector,
        Func<T, DateTimeOffset?> updatedAtSelector,
        Func<T, DateTimeOffset?> createdAtSelector,
        IgdbSyncWindow? window,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();
        IReadOnlyList<T> page = values
            .Where(value => idSelector(value) > afterId)
            .Where(value => IsWithinWindow(updatedAtSelector(value), createdAtSelector(value), window))
            .OrderBy(idSelector)
            .Take(PageSize)
            .ToArray();
        return Task.FromResult(page);
    }

    private static DateTimeOffset? GetCreatedAt(
        IReadOnlyDictionary<long, DateTimeOffset?> createdAtById,
        long id,
        DateTimeOffset? updatedAt
    )
    {
        if (createdAtById.TryGetValue(id, out var createdAt))
        {
            return createdAt;
        }

        return updatedAt;
    }

    private static bool IsWithinWindow(DateTimeOffset? updatedAt, DateTimeOffset? createdAt, IgdbSyncWindow? window)
    {
        if (window is null)
        {
            return true;
        }

        if (window.UpdatedAfter is { } lowerBoundary)
        {
            return updatedAt is { } updatedValue
                && updatedValue > lowerBoundary
                && updatedValue <= window.UpdatedThrough;
        }

        return createdAt is { } createdValue && createdValue <= window.UpdatedThrough;
    }

    public sealed record PageRequest(long AfterId, IgdbSyncWindow? Window);
}
