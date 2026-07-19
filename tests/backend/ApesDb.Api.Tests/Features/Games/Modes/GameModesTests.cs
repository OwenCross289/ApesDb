using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using GameModeResponse = ApesDb.Api.Features.Games.Modes.GameModeResponse;

namespace ApesDb.Api.Tests.Features.Games.Modes;

public sealed class GameModesTests
{
    private readonly SharedGetApiFactory _factory;

    public GameModesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameModes()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/modes", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<GameModeResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameModes()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/modes", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
