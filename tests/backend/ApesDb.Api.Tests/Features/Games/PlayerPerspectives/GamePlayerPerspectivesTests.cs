using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using PlayerPerspectiveResponse = ApesDb.Api.Features.Games.PlayerPerspectives.PlayerPerspectiveResponse;

namespace ApesDb.Api.Tests.Features.Games.PlayerPerspectives;

public sealed class GamePlayerPerspectivesTests
{
    private readonly SharedGetApiFactory _factory;

    public GamePlayerPerspectivesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGamePlayerPerspectives()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync(
            "/api/games/player-perspectives",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<PlayerPerspectiveResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGamePlayerPerspectives()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(
            "/api/games/player-perspectives",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
