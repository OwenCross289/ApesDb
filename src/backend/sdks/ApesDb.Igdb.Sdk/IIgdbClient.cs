using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

internal interface IIgdbClient
{
    Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
        string endpoint,
        string query,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<IgdbPopularityPrimitive>> QueryPopularityPrimitivesAsync(
        string query,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyList<IgdbGame>> QueryGamesAsync(string query, CancellationToken cancellationToken);
}
