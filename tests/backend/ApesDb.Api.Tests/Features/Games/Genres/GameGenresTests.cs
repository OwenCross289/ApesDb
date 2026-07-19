using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using GenreResponse = ApesDb.Api.Features.Games.Genres.GenreResponse;

namespace ApesDb.Api.Tests.Features.Games.Genres;

public sealed class GameGenresTests
{
    private readonly SharedGetApiFactory _factory;

    public GameGenresTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameGenres()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/genres", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<GenreResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameGenres()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/genres", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
