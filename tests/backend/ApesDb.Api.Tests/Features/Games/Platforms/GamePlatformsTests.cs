using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using PlatformResponse = ApesDb.Api.Features.Games.Platforms.PlatformResponse;

namespace ApesDb.Api.Tests.Features.Games.Platforms;

public sealed class GamePlatformsTests
{
    private readonly SharedGetApiFactory _factory;

    public GamePlatformsTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGamePlatforms()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/platforms", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<PlatformResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGamePlatforms()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/platforms", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
