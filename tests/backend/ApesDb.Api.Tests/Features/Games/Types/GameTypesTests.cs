using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using GameTypeResponse = ApesDb.Api.Features.Games.Types.GameTypeResponse;

namespace ApesDb.Api.Tests.Features.Games.Types;

public sealed class GameTypesTests
{
    private readonly SharedGetApiFactory _factory;

    public GameTypesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameTypes()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/types", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<GameTypeResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameTypes()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/types", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
