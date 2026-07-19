using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using GameStatusResponse = ApesDb.Api.Features.Games.Statuses.GameStatusResponse;

namespace ApesDb.Api.Tests.Features.Games.Statuses;

public sealed class GameStatusesTests
{
    private readonly SharedGetApiFactory _factory;

    public GameStatusesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameStatuses()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/statuses", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<GameStatusResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameStatuses()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/statuses", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
