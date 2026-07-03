using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace ApesDb.Igdb.Sdk.Authentication;

public sealed class IgdbAuthenticationHandler(
    IOptions<IgdbOptions> options,
    IIgdbAccessTokenProvider tokenProvider
) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        request.Headers.Remove("Client-ID");
        request.Headers.Add("Client-ID", options.Value.ClientId);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
