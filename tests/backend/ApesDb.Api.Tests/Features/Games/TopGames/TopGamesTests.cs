using ApesDb.Api.Features.Games.TopGames;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;

namespace ApesDb.Api.Tests.Features.Games.TopGames;

public sealed class TopGamesTests
{
    private readonly SharedGetApiFactory _factory;

    public TopGamesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserGetsTenRankedTopGames()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/top", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<TopGameResponse[]>(response));
    }
}
