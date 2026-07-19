using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ThemeResponse = ApesDb.Api.Features.Games.Themes.ThemeResponse;

namespace ApesDb.Api.Tests.Features.Games.Themes;

public sealed class GameThemesTests
{
    private readonly SharedGetApiFactory _factory;

    public GameThemesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGameThemes()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync("/api/games/themes", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<ThemeResponse[]>(response));
    }

    [Fact]
    public async Task AnonymousUserCannotGetGameThemes()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/games/themes", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }
}
