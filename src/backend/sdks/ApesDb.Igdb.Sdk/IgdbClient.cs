using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace ApesDb.Igdb.Sdk;

internal sealed class IgdbClient : IIgdbClient
{
    private readonly HttpClient _httpClient;

    public IgdbClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IReadOnlyList<TResource>> QueryAsync<TResource>(
        string endpoint,
        string query,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(endpoint);
        ArgumentException.ThrowIfNullOrWhiteSpace(query);

        return PostAsync<IReadOnlyList<TResource>>(endpoint, query, cancellationToken);
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
