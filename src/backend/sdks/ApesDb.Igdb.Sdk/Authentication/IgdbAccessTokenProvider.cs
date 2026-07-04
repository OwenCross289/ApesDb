namespace ApesDb.Igdb.Sdk.Authentication;

public interface IIgdbAccessTokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
}

public sealed class IgdbAccessTokenProvider : IIgdbAccessTokenProvider
{
    private static readonly TimeSpan RefreshSkew = TimeSpan.FromMinutes(5);
    private readonly IIgdbAccessTokenClient _client;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private CachedToken? _cachedToken;

    public IgdbAccessTokenProvider(IIgdbAccessTokenClient client)
    {
        _client = client;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        if (_cachedToken is { } current && current.ExpiresAt > DateTimeOffset.UtcNow)
        {
            return current.AccessToken;
        }

        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            if (_cachedToken is { } refreshed && refreshed.ExpiresAt > DateTimeOffset.UtcNow)
            {
                return refreshed.AccessToken;
            }

            var response = await _client.RequestTokenAsync(cancellationToken);
            var expiresAt = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn) - RefreshSkew;

            _cachedToken = new CachedToken(response.AccessToken, expiresAt);

            return response.AccessToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private sealed record CachedToken(string AccessToken, DateTimeOffset ExpiresAt);
}
