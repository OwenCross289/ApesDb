using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

internal sealed class IgdbClient : IIgdbClient
{
    private readonly HttpClient _httpClient;

    public IgdbClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IReadOnlyList<IgdbPopularityPrimitive>> QueryPopularityPrimitivesAsync(
        string query,
        CancellationToken cancellationToken
    )
    {
        return PostAsync<IReadOnlyList<IgdbPopularityPrimitive>>("popularity_primitives", query, cancellationToken);
    }

    public Task<IReadOnlyList<IgdbGame>> QueryGamesAsync(string query, CancellationToken cancellationToken)
    {
        return PostAsync<IReadOnlyList<IgdbGame>>("games", query, cancellationToken);
    }

    private async Task<TResponse> PostAsync<TResponse>(
        string endpoint,
        string query,
        CancellationToken cancellationToken
    )
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(query, Encoding.UTF8, MediaTypeNames.Text.Plain),
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);
        return result ?? throw new JsonException("IGDB returned a null JSON response body.");
    }
}
