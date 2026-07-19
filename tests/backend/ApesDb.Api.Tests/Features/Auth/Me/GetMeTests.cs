using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using AuthUserResponse = ApesDb.Api.Features.Auth.Me.AuthUserResponse;

namespace ApesDb.Api.Tests.Features.Auth.Me;

public sealed class GetMeTests
{
    private readonly SharedGetApiFactory _factory;

    public GetMeTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCannotGetCurrentUser()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/auth/me", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Theory]
    [InlineData("owner")]
    [InlineData("member")]
    [InlineData("invitee")]
    [InlineData("outsider")]
    public async Task ExistingUserCanGetCurrentUser(string identityKey)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find(identityKey)!);
        using var response = await client.GetAsync("/api/auth/me", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<AuthUserResponse>(response)).UseParameters(identityKey);
    }
}
