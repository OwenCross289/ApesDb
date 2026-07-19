using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using GameDetailsResponse = ApesDb.Api.Features.Games.GetGameById.GetGameByIdResponse;

namespace ApesDb.Api.Tests.Features.Games.GetGameById;

public sealed class GetGameByIdTests
{
    private const long ExistingGameId = 11156L;

    private readonly SharedGetApiFactory _factory;

    public GetGameByIdTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameById()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync(
            $"/api/games/{ExistingGameId}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<GameDetailsResponse>(response));
    }

    [Fact]
    public async Task ExistingUserGetsNotFoundForUnknownGame()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync(
            $"/api/games/{long.MaxValue}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameById()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(
            $"/api/games/{ExistingGameId}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
