using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ApesDb.Api.Features.Notifications.GetNotifications;
using ApesDb.Api.Features.Notifications.NotificationsStream;
using ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;
using ApesDb.Api.Features.Teams.Invites.RespondToTeamInvite;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.TestData;

namespace ApesDb.Api.Tests.Features.Notifications.ReadNotifications;

public sealed class ReadNotificationsTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
    private readonly MutableEndpointApiFactory _factory;

    public ReadNotificationsTests(MutableEndpointApiFactory factory)
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
    public async Task ExistingUserCanMarkActiveNotificationsAsRead()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var readResponse = await client.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );

        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);
        var readHttp = await HttpResponseSnapshot.CreateAsync(readResponse);
        using var notificationsResponse = await client.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var notificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(notificationsResponse);

        await Verify(new { ReadResponse = readHttp.Response, NotificationsResponse = notificationsHttp });
    }

    [Fact]
    public async Task OnlyCurrentUsersNotificationsAreMarkedAsRead()
    {
        using var ownerClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var inviteContent = JsonContent.Create(new CreateTeamInviteRequest { Email = TestUsers.Outsider.Email });
        using var inviteResponse = await ownerClient.PostAsync(
            $"/api/teams/{BaseTestData.SharedTeamId}/invites",
            inviteContent,
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.Accepted, inviteResponse.StatusCode);

        using var inviteeClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var readResponse = await inviteeClient.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);
        var readHttp = await HttpResponseSnapshot.CreateAsync(readResponse);

        using var inviteeNotificationsResponse = await inviteeClient.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var inviteeNotificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(
            inviteeNotificationsResponse
        );
        using var outsiderClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Outsider);
        using var outsiderNotificationsResponse = await outsiderClient.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var outsiderNotificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(
            outsiderNotificationsResponse
        );

        await Verify(
            new
            {
                ReadResponse = readHttp.Response,
                CurrentUserNotificationsResponse = inviteeNotificationsHttp,
                OtherUserNotificationsResponse = outsiderNotificationsHttp,
            }
        );
    }

    [Fact]
    public async Task ResolvedNotificationsRemainExcludedWhenReadingNotifications()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var respondContent = JsonContent.Create(new RespondToTeamInviteRequest { Accept = true });
        using var respondResponse = await client.PostAsync(
            $"/api/teams/invites/{BaseTestData.PendingInviteId}/respond",
            respondContent,
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.NoContent, respondResponse.StatusCode);

        using var readResponse = await client.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);
        var readHttp = await HttpResponseSnapshot.CreateAsync(readResponse);
        using var notificationsResponse = await client.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var notificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(notificationsResponse);

        await Verify(new { ReadResponse = readHttp.Response, NotificationsResponse = notificationsHttp });
    }

    [Fact]
    public async Task ReadingNotificationsIsIdempotent()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var firstResponse = await client.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );
        using var secondResponse = await client.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );

        Assert.Equal(HttpStatusCode.NoContent, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, secondResponse.StatusCode);
        var readResponses = await HttpResponseSnapshot.CreateAsync(firstResponse, secondResponse);
        using var notificationsResponse = await client.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var notificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(notificationsResponse);

        await Verify(new { ReadResponses = readResponses, NotificationsResponse = notificationsHttp });
    }

    [Fact]
    public async Task ReadingNotificationsPublishesStreamEvent()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var streamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var streamResponse = await streamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        Assert.Equal(HttpStatusCode.OK, streamResponse.StatusCode);
        Assert.Equal("text/event-stream", streamResponse.Content.Headers.ContentType?.MediaType);

        await using var stream = await streamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var reader = new StreamReader(stream);
        Assert.Equal(": connected", await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));

        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var readResponse = await client.PostAsync("/api/notifications/read", timeout.Token);
        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);
        var readHttp = await HttpResponseSnapshot.CreateAsync(readResponse);

        var eventLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        var dataLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));
        Assert.StartsWith("event: ", eventLine);
        Assert.StartsWith("data: ", dataLine);
        var eventName = eventLine["event: ".Length..];
        var eventData = JsonSerializer.Deserialize<NotificationReadEventData>(
            dataLine["data: ".Length..],
            JsonSerializerOptions.Web
        );
        Assert.NotNull(eventData);

        using var notificationsResponse = await client.GetAsync("/api/notifications", timeout.Token);
        var notificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(notificationsResponse);

        await Verify(
            new
            {
                ReadResponse = readHttp.Response,
                StreamEvent = new { Name = eventName, Data = eventData },
                NotificationsResponse = notificationsHttp,
            }
        );
    }

    [Fact]
    public async Task AnonymousUserCannotReadNotifications()
    {
        using var anonymousClient = ApiTestClient.CreateAnonymous(_factory);
        using var readResponse = await anonymousClient.PostAsync(
            "/api/notifications/read",
            TestContext.Current.CancellationToken
        );

        Assert.Equal(HttpStatusCode.Unauthorized, readResponse.StatusCode);
        var readHttp = await HttpResponseSnapshot.CreateAsync(readResponse);
        using var inviteeClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var notificationsResponse = await inviteeClient.GetAsync(
            "/api/notifications",
            TestContext.Current.CancellationToken
        );
        var notificationsHttp = await HttpResponseSnapshot.CreateAsync<NotificationsResponse>(notificationsResponse);

        await Verify(new { ReadResponse = readHttp.Response, NotificationsResponse = notificationsHttp });
    }
}
