using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace ApesDb.Igdb.Sdk.Authentication;

public sealed class IgdbAuthenticationHandler : DelegatingHandler
{
    private readonly IOptions<IgdbOptions> _options;
    private readonly IIgdbAccessTokenProvider _tokenProvider;

    public IgdbAuthenticationHandler(
        IOptions<IgdbOptions> options,
        IIgdbAccessTokenProvider tokenProvider
    )
    {
        _options = options;
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var token = await _tokenProvider.GetAccessTokenAsync(cancellationToken);

        request.Headers.Remove("Client-ID");
        request.Headers.Add("Client-ID", _options.Value.ClientId);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
