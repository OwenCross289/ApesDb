using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.TestData;
using TeamInviteResponse = ApesDb.Api.Features.Teams.Invites.GetTeamInvite.TeamInviteResponse;

namespace ApesDb.Api.Tests.Features.Teams.GetTeamInvite;

public sealed class GetTeamInviteTests
{
    private readonly SharedGetApiFactory _factory;

    public GetTeamInviteTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("owner")]
    [InlineData("member")]
    [InlineData("invitee")]
    [InlineData("outsider")]
    public async Task ExistingUserHasExpectedInvitationAccess(string identityKey)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find(identityKey)!);
        using var response = await client.GetAsync(
            $"/api/teams/invites/{BaseTestData.PendingInviteId}",
            TestContext.Current.CancellationToken
        );

        await Verify(await HttpResponseSnapshot.CreateAsync<TeamInviteResponse>(response)).UseParameters(identityKey);
    }
}
