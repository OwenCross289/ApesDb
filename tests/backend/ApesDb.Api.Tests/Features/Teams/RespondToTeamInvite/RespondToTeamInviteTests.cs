using System.Net;
using System.Net.Http.Json;
using ApesDb.Api.Features.Notifications.GetNotifications;
using ApesDb.Api.Features.Teams.Invites.RespondToTeamInvite;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.TestData;

namespace ApesDb.Api.Tests.Features.Teams.RespondToTeamInvite;

public sealed class RespondToTeamInviteTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
    private static readonly Guid UnknownInviteId = Guid.Parse("01910000-0000-7000-8000-000000003999");

    private readonly MutableEndpointApiFactory _factory;

    public RespondToTeamInviteTests(MutableEndpointApiFactory factory)
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
    public async Task InviteeCanAcceptTeamInvite()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var response = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponse = http.Response, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task InviteeCanDeclineTeamInvite()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var response = await PostResponseAsync(client, BaseTestData.PendingInviteId, false);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponse = http.Response, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task RepeatedAcceptIsIdempotent()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var firstResponse = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);
        using var secondResponse = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);

        Assert.Equal(HttpStatusCode.NoContent, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, secondResponse.StatusCode);
        var responses = await HttpResponseSnapshot.CreateAsync(firstResponse, secondResponse);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponses = responses, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task DeclineAfterAcceptReturnsConflict()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var acceptResponse = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);
        using var declineResponse = await PostResponseAsync(client, BaseTestData.PendingInviteId, false);

        Assert.Equal(HttpStatusCode.NoContent, acceptResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, declineResponse.StatusCode);
        var responses = await HttpResponseSnapshot.CreateAsync(acceptResponse, declineResponse);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponses = responses, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task WrongUserCannotRespondToTeamInvite()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Outsider);
        using var response = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponse = http.Response, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task CannotRespondToUnknownTeamInvite()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var response = await PostResponseAsync(client, UnknownInviteId, true);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponse = http.Response, NotificationsResponse = notificationsResponse });
    }

    [Fact]
    public async Task AnonymousUserCannotRespondToTeamInvite()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await PostResponseAsync(client, BaseTestData.PendingInviteId, true);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var http = await HttpResponseSnapshot.CreateAsync(response);
        var notificationsResponse = await GetNotificationsAsync(TestUsers.Invitee);

        await Verify(new { RespondResponse = http.Response, NotificationsResponse = notificationsResponse });
    }

    private async Task<HttpResponseSnapshot> GetNotificationsAsync(TestUser user)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, user);
        using var response = await client.GetAsync("/api/notifications", TestContext.Current.CancellationToken);
        return await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(response);
    }

    private static async Task<HttpResponseMessage> PostResponseAsync(ApiTestClient client, Guid inviteId, bool accept)
    {
        using var content = JsonContent.Create(new RespondToTeamInviteRequest { Accept = accept });
        return await client.PostAsync(
            $"/api/teams/invites/{inviteId}/respond",
            content,
            TestContext.Current.CancellationToken
        );
    }
}
