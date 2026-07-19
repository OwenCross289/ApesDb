using ApesDb.Api.Features.Games.GetGames;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Common;
using Microsoft.AspNetCore.WebUtilities;

namespace ApesDb.Api.Tests.Features.Games.GetGames;

public sealed class GetGamesTests
{
    private const string GamesEndpoint = "/api/games";

    private readonly SharedGetApiFactory _factory;

    public GetGamesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExistingUserCanGetGames()
    {
        await Verify(await GetGamesAsync());
    }

    [Fact]
    public async Task AnonymousUserCannotGetGames()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(GamesEndpoint, TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Fact]
    public async Task FiltersByGameTypeIds()
    {
        await Verify(await GetGamesAsync(("GameTypeIds", "1")));
    }

    [Fact]
    public async Task FiltersByGameStatusIds()
    {
        await Verify(await GetGamesAsync(("GameStatusIds", "0")));
    }

    [Fact]
    public async Task FiltersByGenreIds()
    {
        await Verify(await GetGamesAsync(("GenreIds", "5")));
    }

    [Fact]
    public async Task FiltersByThemeIds()
    {
        await Verify(await GetGamesAsync(("ThemeIds", "38")));
    }

    [Fact]
    public async Task FiltersByGameModeIds()
    {
        await Verify(await GetGamesAsync(("GameModeIds", "3")));
    }

    [Fact]
    public async Task FiltersByPlayerPerspectiveIds()
    {
        await Verify(await GetGamesAsync(("PlayerPerspectiveIds", "2")));
    }

    [Fact]
    public async Task FiltersByPlatformIds()
    {
        await Verify(await GetGamesAsync(("PlatformIds", "130")));
    }

    [Fact]
    public async Task FiltersByDeveloper()
    {
        await Verify(await GetGamesAsync(("Developer", "  gUeRrIlLa  ")));
    }

    [Fact]
    public async Task FiltersByPublisher()
    {
        await Verify(await GetGamesAsync(("Publisher", "  sOnY iNtErAcTiVe  ")));
    }

    [Fact]
    public async Task FiltersByCollection()
    {
        await Verify(await GetGamesAsync(("Collection", "  hOrIzOn  ")));
    }

    [Fact]
    public async Task FiltersByFranchise()
    {
        await Verify(await GetGamesAsync(("Franchise", "  tHe LeGeNd Of ZeLdA  ")));
    }

    [Fact]
    public async Task SearchesByName()
    {
        await Verify(await GetGamesAsync(("Search", "  nInTeNdO hEaRtS fUn TrIvIa QuIz  ")));
    }

    [Fact]
    public async Task FiltersByCoopGames()
    {
        await Verify(await GetGamesAsync(("IsCoop", "true")));
    }

    [Fact]
    public async Task FiltersByNonCoopGames()
    {
        await Verify(await GetGamesAsync(("IsCoop", "false")));
    }

    [Fact]
    public async Task FiltersBySteamGames()
    {
        await Verify(await GetGamesAsync(("IsSteam", "true")));
    }

    [Fact]
    public async Task FiltersByNonSteamGames()
    {
        await Verify(await GetGamesAsync(("IsSteam", "false")));
    }

    [Fact]
    public async Task FiltersByFranchiseAndGameType()
    {
        await Verify(await GetGamesAsync(("Franchise", "The Legend of Zelda"), ("GameTypeIds", "1")));
    }

    [Fact]
    public async Task AddsGameTypeIdFilters()
    {
        await Verify(await GetGamesAsync(("GameTypeIds", "0"), ("GameTypeIds", "2")));
    }

    [Fact]
    public async Task AddsGameStatusIdFilters()
    {
        await Verify(await GetGamesAsync(("GameStatusIds", "0"), ("GameStatusIds", "8")));
    }

    [Fact]
    public async Task AddsGenreIdFilters()
    {
        await Verify(await GetGamesAsync(("GenreIds", "5"), ("GenreIds", "31")));
    }

    [Fact]
    public async Task AddsThemeIdFilters()
    {
        await Verify(await GetGamesAsync(("ThemeIds", "1"), ("ThemeIds", "38")));
    }

    [Fact]
    public async Task AddsGameModeIdFilters()
    {
        await Verify(await GetGamesAsync(("GameModeIds", "1"), ("GameModeIds", "3")));
    }

    [Fact]
    public async Task AddsPlayerPerspectiveIdFilters()
    {
        await Verify(await GetGamesAsync(("PlayerPerspectiveIds", "1"), ("PlayerPerspectiveIds", "2")));
    }

    [Fact]
    public async Task AddsPlatformIdFilters()
    {
        await Verify(await GetGamesAsync(("PlatformIds", "48"), ("PlatformIds", "130")));
    }

    [Fact]
    public async Task PaginatesGames()
    {
        await Verify(await GetGamesAsync(("Page", "2"), ("PageSize", "5")));
    }

    [Fact]
    public async Task ExcludesVersionChildren()
    {
        await Verify(await GetGamesAsync(("Search", "Gears of War: E-Day Premium Edition")));
    }

    //This is slop but I do not want to build an sdk
    private async Task<HttpResponseSnapshot> GetGamesAsync(params (string Name, string? Value)[] queryParameters)
    {
        var parameters = queryParameters.Select(parameter => new KeyValuePair<string, string?>(
            parameter.Name,
            parameter.Value
        ));
        var requestUri = QueryHelpers.AddQueryString(GamesEndpoint, parameters);

        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find("owner")!);
        using var response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        return await HttpResponseSnapshot.CreateAsync<Pagable<GetGameResponse>>(response);
    }
}
