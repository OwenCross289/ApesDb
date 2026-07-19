using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApesDb.Api.Tests.Infrastructure.Http;

public sealed class ApiTestClient : IDisposable
{
    private readonly HttpClient _client;

    private ApiTestClient(ApiTestWebApplicationFactory factory, TestUser? identity)
    {
        _client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost"),
            }
        );

        if (identity is not null)
        {
            _client.DefaultRequestHeaders.Add(FakeAuthenticationHandler.HeaderName, identity.Key);
        }
    }

    public static ApiTestClient CreateAuthenticated(ApiTestWebApplicationFactory factory, TestUser identity)
    {
        return new ApiTestClient(factory, identity);
    }

    public static ApiTestClient CreateAnonymous(ApiTestWebApplicationFactory factory)
    {
        return new ApiTestClient(factory, null);
    }

    public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        return _client.GetAsync(requestUri, cancellationToken);
    }

    public Task<HttpResponseMessage> GetAsync(
        string requestUri,
        HttpCompletionOption completionOption,
        CancellationToken cancellationToken = default
    )
    {
        return _client.GetAsync(requestUri, completionOption, cancellationToken);
    }

    public Task<HttpResponseMessage> PostAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        return _client.PostAsync(requestUri, null, cancellationToken);
    }

    public Task<HttpResponseMessage> PostAsync(
        string requestUri,
        HttpContent content,
        CancellationToken cancellationToken = default
    )
    {
        return _client.PostAsync(requestUri, content, cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
