namespace ApesDb.Igdb.Sdk.Authentication;

public interface IIgdbAccessTokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
}

public sealed class IgdbAccessTokenProvider(IIgdbAccessTokenClient client) : IIgdbAccessTokenProvider
{
    private static readonly TimeSpan RefreshSkew = TimeSpan.FromMinutes(5);
    private readonly SemaphoreSlim semaphore = new(1, 1);
    private CachedToken? cachedToken;

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        if (this.cachedToken is { } current && current.ExpiresAt > DateTimeOffset.UtcNow)
        {
            return current.AccessToken;
        }

        await this.semaphore.WaitAsync(cancellationToken);

        try
        {
            if (this.cachedToken is { } refreshed && refreshed.ExpiresAt > DateTimeOffset.UtcNow)
            {
                return refreshed.AccessToken;
            }

            var response = await client.RequestTokenAsync(cancellationToken);
            var expiresAt = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn) - RefreshSkew;

            this.cachedToken = new CachedToken(response.AccessToken, expiresAt);

            return response.AccessToken;
        }
        finally
        {
            this.semaphore.Release();
        }
    }

    private sealed record CachedToken(string AccessToken, DateTimeOffset ExpiresAt);
}
