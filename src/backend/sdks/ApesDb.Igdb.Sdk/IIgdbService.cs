using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

public interface IIgdbService
{
    Task<IReadOnlyList<IgdbGameType>> FetchGameTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbGameStatus>> FetchGameStatusesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbGenre>> FetchGenresPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbTheme>> FetchThemesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbGameMode>> FetchGameModesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPlayerPerspective>> FetchPlayerPerspectivesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPlatformType>> FetchPlatformTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbWebsiteType>> FetchWebsiteTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPopularityType>> FetchPopularityTypesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbExternalGameSource>> FetchExternalGameSourcesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbCompany>> FetchCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbCollection>> FetchCollectionsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbFranchise>> FetchFranchisesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPlatform>> FetchPlatformsPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPlatformWebsite>> FetchPlatformWebsitesPageAsync(
        long afterId,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbGame>> FetchGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbInvolvedCompany>> FetchInvolvedCompaniesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbExternalGame>> FetchExternalGamesPageAsync(
        long afterId,
        IgdbSyncWindow? window = null,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<IgdbPopularityPrimitive>> FetchPopularityPrimitivesPageAsync(
        long popularityTypeId,
        int offset = 0,
        CancellationToken cancellationToken = default
    );
}
