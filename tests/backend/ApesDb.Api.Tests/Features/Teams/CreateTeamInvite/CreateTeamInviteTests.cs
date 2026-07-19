using System.Net;
using System.Net.Http.Json;
using ApesDb.Api.Features.Notifications.GetNotifications;
using ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.TestData;

namespace ApesDb.Api.Tests.Features.Teams.CreateTeamInvite;

public sealed class CreateTeamInviteTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
    private static readonly Guid OwnerSoloTeamId = Guid.Parse("01910000-0000-7000-8000-000000002001");
    private static readonly Guid UnknownTeamId = Guid.Parse("01910000-0000-7000-8000-000000002999");

    private readonly MutableEndpointApiFactory _factory;

    public CreateTeamInviteTests(MutableEndpointApiFactory factory)
    {
        _factory = factory;
    }

    public async ValueTask InitializeAsync()
    {
        await _factory.ResetAsync(TestContext.Current.CancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    [Fact]
    public async Task OwnerCanInviteExistingUser()
    {
        await Verify(await CreateSuccessfulInviteAsync(TestUsers.Owner, "  OUTSIDER@APESDB.TEST  "));
    }

    [Fact]
    public async Task AcceptedMemberCanInviteExistingUser()
    {
        await Verify(await CreateSuccessfulInviteAsync(TestUsers.Member, TestUsers.Outsider.Email));
    }

    [Fact]
    public async Task AnonymousUserCannotCreateTeamInvite()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                null,
                BaseTestData.SharedTeamId,
                TestUsers.Outsider.Email,
                HttpStatusCode.Unauthorized,
                TestUsers.Outsider
            )
        );
    }

    [Fact]
    public async Task PendingMemberCannotCreateTeamInvite()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Invitee,
                BaseTestData.SharedTeamId,
                TestUsers.Outsider.Email,
                HttpStatusCode.NotFound,
                TestUsers.Outsider
            )
        );
    }

    [Fact]
    public async Task NonMemberCannotCreateTeamInvite()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Outsider,
                BaseTestData.SharedTeamId,
                TestUsers.Member.Email,
                HttpStatusCode.NotFound,
                TestUsers.Member
            )
        );
    }

    [Fact]
    public async Task CannotCreateInviteForSoloTeam()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                OwnerSoloTeamId,
                TestUsers.Outsider.Email,
                HttpStatusCode.NotFound,
                TestUsers.Outsider
            )
        );
    }

    [Fact]
    public async Task CannotCreateInviteForUnknownTeam()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                UnknownTeamId,
                TestUsers.Outsider.Email,
                HttpStatusCode.NotFound,
                TestUsers.Outsider
            )
        );
    }

    [Fact]
    public async Task InvitingSelfIsAcceptedWithoutChanges()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                BaseTestData.SharedTeamId,
                TestUsers.Owner.Email,
                HttpStatusCode.Accepted,
                TestUsers.Owner
            )
        );
    }

    [Fact]
    public async Task InvitingUnknownUserIsAcceptedWithoutChanges()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                BaseTestData.SharedTeamId,
                "unknown@apesdb.test",
                HttpStatusCode.Accepted,
                TestUsers.Owner
            )
        );
    }

    [Fact]
    public async Task InvitingExistingMemberIsAcceptedWithoutChanges()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                BaseTestData.SharedTeamId,
                TestUsers.Member.Email,
                HttpStatusCode.Accepted,
                TestUsers.Member
            )
        );
    }

    [Fact]
    public async Task InvitingAlreadyInvitedUserIsAcceptedWithoutChanges()
    {
        await Verify(
            await CreateNoOpInviteAsync(
                TestUsers.Owner,
                BaseTestData.SharedTeamId,
                TestUsers.Invitee.Email,
                HttpStatusCode.Accepted,
                TestUsers.Invitee
            )
        );
    }

    [Fact]
    public async Task SequentialDuplicateInvitesAreIdempotent()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var firstResponse = await PostInviteAsync(client, BaseTestData.SharedTeamId, TestUsers.Outsider.Email);
        using var secondResponse = await PostInviteAsync(client, BaseTestData.SharedTeamId, TestUsers.Outsider.Email);

        Assert.Equal(HttpStatusCode.Accepted, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Accepted, secondResponse.StatusCode);
        var responses = await HttpResponseSnapshot.CreateAsync(firstResponse, secondResponse);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Outsider);

        await Verify(new { InviteResponses = responses, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task ConcurrentDuplicateInvitesAreIdempotent()
    {
        using var firstClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var secondClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        var firstTask = PostInviteAsync(firstClient, BaseTestData.SharedTeamId, TestUsers.Outsider.Email);
        var secondTask = PostInviteAsync(secondClient, BaseTestData.SharedTeamId, TestUsers.Outsider.Email);
        await Task.WhenAll(firstTask, secondTask);
        using var firstResponse = await firstTask;
        using var secondResponse = await secondTask;

        Assert.Equal(HttpStatusCode.Accepted, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Accepted, secondResponse.StatusCode);
        var responses = await HttpResponseSnapshot.CreateAsync(firstResponse, secondResponse);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Outsider);

        await Verify(new { InviteResponses = responses, NotificationsResponse = notificationsResponse });
    }

    private async Task<object> CreateSuccessfulInviteAsync(TestUser inviter, string email)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, inviter);
        using var response = await PostInviteAsync(client, BaseTestData.SharedTeamId, email);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Outsider);
        return new { InviteResponse = http.Response, NotificationsResponse = notificationsResponse };
    }

    private async Task<object> CreateNoOpInviteAsync(
        TestUser? inviter,
        Guid teamId,
        string email,
        HttpStatusCode expectedStatus,
        TestUser notificationsUser
    )
    {
        ApiTestClient client;
        if (inviter is null)
        {
            client = ApiTestClient.CreateAnonymous(_factory);
        }
        else
        {
            client = ApiTestClient.CreateAuthenticated(_factory, inviter);
        }

        using (client)
        using (var response = await PostInviteAsync(client, teamId, email))
        {
            Assert.Equal(expectedStatus, response.StatusCode);
            var http = await HttpResponseSnapshot.CreateAsync(response);
            var notificationsHttp = await GetNotificationsAsync(notificationsUser);

            return new { InviteResponse = http.Response, NotificationsResponse = notificationsHttp };
        }
    }

    private async Task<HttpResponseSnapshot> GetNotificationsAsync(TestUser user)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, user);
        using var response = await client.GetAsync("/api/notifications", TestContext.Current.CancellationToken);
        return await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(response);
    }

    private static async Task<HttpResponseMessage> PostInviteAsync(ApiTestClient client, Guid teamId, string email)
    {
        using var content = JsonContent.Create(new CreateTeamInviteRequest { Email = email });
        return await client.PostAsync($"/api/teams/{teamId}/invites", content, TestContext.Current.CancellationToken);
    }
}
