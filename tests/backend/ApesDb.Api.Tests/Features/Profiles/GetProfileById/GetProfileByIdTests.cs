using ApesDb.Api.Features.Profiles;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;

namespace ApesDb.Api.Tests.Features.Profiles.GetProfileById;

public sealed class GetProfileByIdTests
{
    private readonly SharedGetApiFactory _factory;

    public GetProfileByIdTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCanGetPublicProfileById()
    {
        using var response = await GetProfileAsync(TestUsers.Owner.SeededUserId!.Value);

        await Verify(await HttpResponseSnapshot.CreateAsync<ProfileResponse>(response));
    }

    [Fact]
    public async Task PrivateProfileIsNotFound()
    {
        using var response = await GetProfileAsync(TestUsers.Outsider.SeededUserId!.Value);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Fact]
    public async Task OwnerCanGetOwnPrivateProfile()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Outsider);
        using var response = await client.GetAsync(
            $"/api/profiles/{TestUsers.Outsider.SeededUserId!.Value}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<ProfileResponse>(response));
    }

    [Fact]
    public async Task OtherUserCannotGetPrivateProfile()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var response = await client.GetAsync(
            $"/api/profiles/{TestUsers.Outsider.SeededUserId!.Value}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Fact]
    public async Task UnknownProfileIsNotFound()
    {
        using var response = await GetProfileAsync(Guid.Parse("01910000-0000-7000-8000-000000001099"));

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    private async Task<HttpResponseMessage> GetProfileAsync(Guid id)
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        return await client.GetAsync($"/api/profiles/{id}", TestContext.Current.CancellationToken);
    }
}
