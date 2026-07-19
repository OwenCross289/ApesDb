using ApesDb.Api.Features.Auth.Logout;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;

namespace ApesDb.Api.Tests.Features.Auth.Logout;

public sealed class LogoutTests : IClassFixture<MutableEndpointApiFactory>
{
    private readonly MutableEndpointApiFactory _factory;

    public LogoutTests(MutableEndpointApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCanLogout()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.PostAsync("/api/auth/logout", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<LogoutResponse>(response));
    }
}
