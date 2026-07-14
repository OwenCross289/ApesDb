namespace ApesDb.Igdb.Sdk;

internal interface IIgdbClient
{
    Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
        string endpoint,
        string query,
        CancellationToken cancellationToken
    );
}
