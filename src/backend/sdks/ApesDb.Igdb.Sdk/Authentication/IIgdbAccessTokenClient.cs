namespace ApesDb.Igdb.Sdk.Authentication;

public interface IIgdbAccessTokenClient
{
    Task<IgdbAccessTokenResponse> RequestTokenAsync(CancellationToken cancellationToken);
}
