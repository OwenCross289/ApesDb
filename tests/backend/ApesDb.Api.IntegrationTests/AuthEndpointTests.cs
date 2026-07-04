using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApesDb.Api.Endpoints.Auth;
using ApesDb.Api.Endpoints.Ping;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ApesDb.Api.IntegrationTests;

public sealed class AuthEndpointTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private HttpClient CreateClient(bool authenticated = false)
    {
        var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost/"),
                AllowAutoRedirect = false,
            }
        );

        if (authenticated)
        {
            client.DefaultRequestHeaders.Add(TestAuthHandler.AuthHeaderName, "true");
        }

        return client;
    }

    [Fact]
    public async Task GetPingAllowsAnonymous()
    {
        using var client = CreateClient();
        var cancellationToken = TestContext.Current.CancellationToken;

        var response = await client.GetAsync("/api/ping", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<PingResponse>(cancellationToken);

        Assert.NotNull(payload);
        Assert.Equal("pong", payload.Status);
    }

    [Fact]
    public async Task GetMeReturnsUnauthorizedWhenAnonymous()
    {
        using var client = CreateClient();
        var cancellationToken = TestContext.Current.CancellationToken;

        var response = await client.GetAsync("/api/auth/me", cancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetMeReturnsUserWhenAuthenticated()
    {
        using var client = CreateClient(authenticated: true);
        var cancellationToken = TestContext.Current.CancellationToken;

        var response = await client.GetAsync("/api/auth/me", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<AuthUserResponse>(cancellationToken);

        Assert.NotNull(payload);
        Assert.Equal("test@example.com", payload.Email);
        Assert.Equal("Test User", payload.Name);
    }

    [Fact]
    public async Task PostLogoutReturnsAuth0LogoutUrl()
    {
        using var client = CreateClient(authenticated: true);
        var cancellationToken = TestContext.Current.CancellationToken;

        var logoutResponse = await client.PostAsync("/api/auth/logout", null, cancellationToken);
        Assert.Equal(HttpStatusCode.OK, logoutResponse.StatusCode);

        var payload = await logoutResponse.Content.ReadFromJsonAsync<LogoutResponse>(
            cancellationToken
        );

        Assert.NotNull(payload);
        Assert.Contains("test.auth0.com/v2/logout", payload.LogoutUrl);
    }
}
