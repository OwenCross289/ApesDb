using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ApesDb.Api.Tests.Infrastructure.Http;

public sealed record HttpResponseSnapshot(HttpResponseDetails Response, object? Content)
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public static async Task<HttpResponseSnapshot> CreateAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        object? snapshotContent = null;

        if (!string.IsNullOrEmpty(content))
        {
            snapshotContent = content;
        }

        return Create(response, snapshotContent);
    }

    public static async Task<HttpResponseSnapshot> CreateAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        object? snapshotContent = null;

        if (!string.IsNullOrWhiteSpace(content))
        {
            snapshotContent = JsonSerializer.Deserialize<T>(content, SerializerOptions);
        }

        return Create(response, snapshotContent);
    }

    private static HttpResponseSnapshot Create(HttpResponseMessage response, object? content)
    {
        var details = new HttpResponseDetails(
            (int)response.StatusCode,
            response.ReasonPhrase,
            response.Version.ToString(),
            CreateHeaders(response.Headers),
            CreateHeaders(response.Content.Headers)
        );

        return new HttpResponseSnapshot(details, content);
    }

    private static IReadOnlyDictionary<string, string[]> CreateHeaders(HttpHeaders headers)
    {
        var snapshot = new SortedDictionary<string, string[]>(StringComparer.Ordinal);

        foreach (var header in headers)
        {
            snapshot[header.Key] = header.Value.Select(value => NormalizeHeader(header.Key, value)).ToArray();
        }

        return snapshot;
    }

    private static string NormalizeHeader(string name, string value)
    {
        if (string.Equals(name, "Location", StringComparison.OrdinalIgnoreCase))
        {
            return NormalizeLocation(value);
        }

        if (string.Equals(name, "Set-Cookie", StringComparison.OrdinalIgnoreCase))
        {
            return NormalizeCookie(value);
        }

        return value;
    }

    private static string NormalizeLocation(string value)
    {
        return Regex.Replace(
            value,
            "([?&](?:nonce|state|code_challenge)=)[^&]+",
            "$1{scrubbed}",
            RegexOptions.IgnoreCase
        );
    }

    private static string NormalizeCookie(string value)
    {
        var normalized = Regex.Replace(
            value,
            "^(\\.AspNetCore\\.(?:Correlation|OpenIdConnect\\.Nonce)\\.)[^=]+=[^;]*",
            "$1{scrubbed}={scrubbed}",
            RegexOptions.IgnoreCase
        );

        return Regex.Replace(normalized, "expires=[^;]+", "expires={scrubbed}", RegexOptions.IgnoreCase);
    }
}

public sealed record HttpResponseDetails(
    int StatusCode,
    string? ReasonPhrase,
    string Version,
    IReadOnlyDictionary<string, string[]> Headers,
    IReadOnlyDictionary<string, string[]> ContentHeaders
);
