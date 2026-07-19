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

namespace ApesDb.Api.Tests.Features.Notifications.NotificationsStream;

public sealed class NotificationsStreamTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
    private readonly MutableEndpointApiFactory _factory;

    public NotificationsStreamTests(MutableEndpointApiFactory factory)
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
    public async Task AnonymousUserCannotStreamNotifications()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/notifications/stream", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Fact]
    public async Task ExistingUserCanConnectToNotificationsStream()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var response = await client.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/event-stream", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal("no-cache", response.Headers.CacheControl?.ToString());
        Assert.True(response.Headers.TryGetValues("X-Accel-Buffering", out var bufferingValues));
        Assert.Equal("no", Assert.Single(bufferingValues));
        var streamHttp = HttpResponseSnapshot.CreateWithoutContent(response);

        await using var stream = await response.Content.ReadAsStreamAsync(timeout.Token);
        using var reader = new StreamReader(stream);
        var initialFrame = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(": connected", initialFrame);
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));

        await Verify(new { StreamResponse = streamHttp.Response, InitialFrame = initialFrame });
    }

    [Fact]
    public async Task CreatingInvitePublishesCreatedEvent()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var streamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Outsider);
        using var streamResponse = await streamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        await using var stream = await streamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var reader = new StreamReader(stream);
        Assert.Equal(": connected", await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));

        using var ownerClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var inviteContent = JsonContent.Create(new CreateTeamInviteRequest { Email = TestUsers.Outsider.Email });
        using var inviteResponse = await ownerClient.PostAsync(
            $"/api/teams/{BaseTestData.SharedTeamId}/invites",
            inviteContent,
            timeout.Token
        );
        Assert.Equal(HttpStatusCode.Accepted, inviteResponse.StatusCode);

        var eventLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        var dataLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));
        Assert.StartsWith("event: ", eventLine);
        Assert.StartsWith("data: ", dataLine);
        var eventName = eventLine["event: ".Length..];
        var eventData = JsonSerializer.Deserialize<NotificationResponse>(
            dataLine["data: ".Length..],
            JsonSerializerOptions.Web
        );
        Assert.NotNull(eventData);

        await Verify(new { StreamEvent = new { Name = eventName, Data = eventData } });
    }

    [Fact]
    public async Task ReadingNotificationsPublishesReadEvent()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var streamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var streamResponse = await streamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        await using var stream = await streamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var reader = new StreamReader(stream);
        Assert.Equal(": connected", await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));

        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var readResponse = await client.PostAsync("/api/notifications/read", timeout.Token);
        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);

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

        await Verify(new { StreamEvent = new { Name = eventName, Data = eventData } });
    }

    [Fact]
    public async Task RespondingToInvitePublishesResolvedEvent()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var streamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var streamResponse = await streamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        await using var stream = await streamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var reader = new StreamReader(stream);
        Assert.Equal(": connected", await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));

        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var respondContent = JsonContent.Create(new RespondToTeamInviteRequest { Accept = true });
        using var respondResponse = await client.PostAsync(
            $"/api/teams/invites/{BaseTestData.PendingInviteId}/respond",
            respondContent,
            timeout.Token
        );
        Assert.Equal(HttpStatusCode.NoContent, respondResponse.StatusCode);

        var eventLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        var dataLine = Assert.IsType<string>(await reader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await reader.ReadLineAsync(timeout.Token));
        Assert.StartsWith("event: ", eventLine);
        Assert.StartsWith("data: ", dataLine);
        var eventName = eventLine["event: ".Length..];
        var eventData = JsonSerializer.Deserialize<NotificationResolvedEventData>(
            dataLine["data: ".Length..],
            JsonSerializerOptions.Web
        );
        Assert.NotNull(eventData);

        await Verify(new { StreamEvent = new { Name = eventName, Data = eventData } });
    }

    [Fact]
    public async Task StreamEventsAreIsolatedByUser()
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(10));
        using var inviteeStreamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var inviteeStreamResponse = await inviteeStreamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        await using var inviteeStream = await inviteeStreamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var inviteeReader = new StreamReader(inviteeStream);
        Assert.Equal(": connected", await inviteeReader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await inviteeReader.ReadLineAsync(timeout.Token));

        using var outsiderStreamClient = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Outsider);
        using var outsiderStreamResponse = await outsiderStreamClient.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            timeout.Token
        );
        await using var outsiderStream = await outsiderStreamResponse.Content.ReadAsStreamAsync(timeout.Token);
        using var outsiderReader = new StreamReader(outsiderStream);
        Assert.Equal(": connected", await outsiderReader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await outsiderReader.ReadLineAsync(timeout.Token));

        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var readResponse = await client.PostAsync("/api/notifications/read", timeout.Token);
        Assert.Equal(HttpStatusCode.NoContent, readResponse.StatusCode);

        var eventLine = Assert.IsType<string>(await inviteeReader.ReadLineAsync(timeout.Token));
        var dataLine = Assert.IsType<string>(await inviteeReader.ReadLineAsync(timeout.Token));
        Assert.Equal(string.Empty, await inviteeReader.ReadLineAsync(timeout.Token));
        Assert.StartsWith("event: ", eventLine);
        Assert.StartsWith("data: ", dataLine);
        var eventName = eventLine["event: ".Length..];
        var eventData = JsonSerializer.Deserialize<NotificationReadEventData>(
            dataLine["data: ".Length..],
            JsonSerializerOptions.Web
        );
        Assert.NotNull(eventData);

        using var noEventTimeout = CancellationTokenSource.CreateLinkedTokenSource(timeout.Token);
        noEventTimeout.CancelAfter(TimeSpan.FromMilliseconds(250));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
            await outsiderReader.ReadLineAsync(noEventTimeout.Token)
        );

        await Verify(new { CurrentUserStreamEvent = new { Name = eventName, Data = eventData } });
    }

    [Fact]
    public async Task DisconnectCancelsNotificationStream()
    {
        using var requestCancellation = CancellationTokenSource.CreateLinkedTokenSource(
            TestContext.Current.CancellationToken
        );
        requestCancellation.CancelAfter(TimeSpan.FromSeconds(10));
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Invitee);
        using var response = await client.GetAsync(
            "/api/notifications/stream",
            HttpCompletionOption.ResponseHeadersRead,
            requestCancellation.Token
        );
        var streamHttp = HttpResponseSnapshot.CreateWithoutContent(response);
        await using var stream = await response.Content.ReadAsStreamAsync(requestCancellation.Token);
        using var reader = new StreamReader(stream);
        var initialFrame = Assert.IsType<string>(await reader.ReadLineAsync(requestCancellation.Token));
        Assert.Equal(": connected", initialFrame);
        Assert.Equal(string.Empty, await reader.ReadLineAsync(requestCancellation.Token));

        var pendingRead = reader.ReadLineAsync(requestCancellation.Token).AsTask();
        await requestCancellation.CancelAsync();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await pendingRead);

        await Verify(
            new
            {
                StreamResponse = streamHttp.Response,
                InitialFrame = initialFrame,
                Disconnected = true,
            }
        );
    }
}
