using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using TeamListResponse = ApesDb.Api.Features.Teams.GetTeams.TeamResponse;

namespace ApesDb.Api.Tests.Features.Teams.GetTeams;

public sealed class GetTeamsTests
{
    private readonly SharedGetApiFactory _factory;

    public GetTeamsTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCannotGetTeams()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/teams", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Theory]
    [InlineData("owner")]
    [InlineData("member")]
    [InlineData("invitee")]
    [InlineData("outsider")]
    public async Task ExistingUserSeesExpectedTeams(string identityKey)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find(identityKey)!);
        using var response = await client.GetAsync("/api/teams", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<TeamListResponse[]>(response)).UseParameters(identityKey);
    }
}
