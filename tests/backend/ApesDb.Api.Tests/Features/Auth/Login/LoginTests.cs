using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;

namespace ApesDb.Api.Tests.Features.Auth.Login;

public sealed class LoginTests
{
    private readonly SharedGetApiFactory _factory;

    public LoginTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task LoginWithoutOptionsRedirectsToAuth0()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/auth/login", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync(response));
    }

    [Fact]
    public async Task GoogleLoginPreservesALocalReturnUrl()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(
            "/api/auth/login?connection=google&returnUrl=/games/72",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync(response));
    }

    [Fact]
    public async Task LoginRejectsAnExternalReturnUrl()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(
            "/api/auth/login?returnUrl=https://attacker.example.test/phishing",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync(response));
    }

    [Fact]
    public async Task UserInAllowedListCanLogin()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var response = await client.GetAsync("/api/auth/me", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync(response));
    }

    [Fact]
    public async Task UserNotInAllowedListCannotLogin()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.NotAllowed);
        using var response = await client.GetAsync("/api/auth/me", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync(response));
    }
}
