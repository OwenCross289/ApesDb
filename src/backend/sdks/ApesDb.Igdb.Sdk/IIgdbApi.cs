using ApesDb.Igdb.Sdk.Models;
using Refit;

namespace ApesDb.Igdb.Sdk;

public interface IIgdbApi
{
    [Post("/popularity_primitives")]
    [Headers("Accept: application/json", "Content-Type: text/plain")]
    Task<IReadOnlyList<IgdbPopularityPrimitive>> QueryPopularityPrimitivesAsync(
        [Body] string query,
        CancellationToken cancellationToken
    );

    [Post("/games")]
    [Headers("Accept: application/json", "Content-Type: text/plain")]
    Task<IReadOnlyList<IgdbGame>> QueryGamesAsync([Body] string query, CancellationToken cancellationToken);
}
