using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace ApesDb.Igdb.Sdk.Authentication;

internal sealed class IgdbAccessTokenClient : IIgdbAccessTokenClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<IgdbOptions> _options;

    public IgdbAccessTokenClient(HttpClient httpClient, IOptions<IgdbOptions> options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<IgdbAccessTokenResponse> RequestTokenAsync(
        CancellationToken cancellationToken
    )
    {
        var value = _options.Value;
        var tokenUrl =
            $"{value.TokenUrl}?client_id={Uri.EscapeDataString(value.ClientId)}"
            + $"&client_secret={Uri.EscapeDataString(value.ClientSecret)}"
            + "&grant_type=client_credentials";

        using var response = await _httpClient.PostAsync(
            tokenUrl,
            content: null,
            cancellationToken
        );
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadFromJsonAsync<IgdbAccessTokenResponse>(
            cancellationToken
        );

        return token
            ?? throw new InvalidOperationException("IGDB token response did not include a body.");
    }
}
