using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.TestData;
using TeamDetailsResponse = ApesDb.Api.Features.Teams.TeamResponse;

namespace ApesDb.Api.Tests.Features.Teams.GetTeam;

public sealed class GetTeamTests
{
    private readonly SharedGetApiFactory _factory;

    public GetTeamTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("owner")]
    [InlineData("member")]
    [InlineData("invitee")]
    [InlineData("outsider")]
    public async Task ExistingUserHasExpectedSharedTeamAccess(string identityKey)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find(identityKey)!);
        using var response = await client.GetAsync(
            $"/api/teams/{BaseTestData.SharedTeamId}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<TeamDetailsResponse>(response)).UseParameters(identityKey);
    }
}
