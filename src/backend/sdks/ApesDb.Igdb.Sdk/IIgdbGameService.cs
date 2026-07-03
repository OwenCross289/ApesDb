using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

public interface IIgdbGameService
{
    Task<IReadOnlyList<TopIgdbGame>> ListTopGamesAsync(
        int limit = 10,
        CancellationToken cancellationToken = default
    );
}
