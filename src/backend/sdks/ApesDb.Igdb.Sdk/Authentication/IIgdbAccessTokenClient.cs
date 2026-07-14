namespace ApesDb.Igdb.Sdk.Authentication;

internal interface IIgdbAccessTokenClient
{
    Task<IgdbAccessTokenResponse> RequestTokenAsync(CancellationToken cancellationToken);
}
