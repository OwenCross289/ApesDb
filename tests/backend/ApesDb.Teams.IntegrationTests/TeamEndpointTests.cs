using System.Security.Claims;
using System.Text.Json;
using ApesDb.Api.Features.Notifications.ListNotifications;
using ApesDb.Api.Features.Notifications.NotificationsStream;
using ApesDb.Api.Features.Notifications.ReadNotifications;
using ApesDb.Api.Features.Teams;
using ApesDb.Api.Features.Teams.CreateTeam;
using ApesDb.Api.Features.Teams.GetTeam;
using ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;
using ApesDb.Api.Features.Teams.Invites.GetTeamInvite;
using ApesDb.Api.Features.Teams.Invites.RespondToTeamInvite;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Notifications;
using ApesDb.Domain.Entities.Teams;
using ApesDb.Domain.Entities.Users;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using Xunit;

namespace ApesDb.Teams.IntegrationTests;

public sealed class TeamEndpointTests : IClassFixture<TeamDatabaseFixture>
{
    private static readonly DateTime Now = new(2026, 7, 17, 20, 0, 0, DateTimeKind.Utc);

    private readonly TeamDatabaseFixture _database;
    private readonly FixedDateTimeProvider _dateTimeProvider = new(Now);
    private readonly NotificationStreamService _streamService = new();

    public TeamEndpointTests(TeamDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task CreateTeam_CreatesGroupAndAcceptedCreatorWithNormalizedPicture()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        await SeedAsync(owner);
        var png = CreatePng(400, 200);
        var formFile = new FormFile(new MemoryStream(png), 0, png.Length, "ProfilePicture", "avatar.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png",
        };

        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(owner.Id);
        var endpoint = Factory.Create<CreateTeamEndpoint>(
            context,
            dbContext,
            _dateTimeProvider,
            new TeamProfilePictureProcessor()
        );
        await endpoint.HandleAsync(new CreateTeamRequest { Name = "  Apes  ", ProfilePicture = formFile }, default);

        Assert.Equal(StatusCodes.Status201Created, context.Response.StatusCode);
        var team = await dbContext.Teams.SingleAsync();
        Assert.Equal("Apes", team.Name);
        Assert.Equal(TeamKind.Group, team.Kind);
        Assert.Equal(owner.Id, team.OwnerUserId);
        Assert.NotNull(team.ProfilePicture);
        using var imageStream = new MemoryStream(team.ProfilePicture);
        using var codec = SKCodec.Create(imageStream);
        Assert.NotNull(codec);
        Assert.Equal(SKEncodedImageFormat.Webp, codec.EncodedFormat);
        using var decoded = SKBitmap.Decode(codec);
        Assert.NotNull(decoded);
        Assert.Equal(256, decoded.Width);
        Assert.Equal(256, decoded.Height);
        var membership = await dbContext.TeamMemberships.SingleAsync();
        Assert.Equal(TeamMembershipStatus.Accepted, membership.Status);
        Assert.Equal(Now, membership.AcceptedAt);
    }

    [Fact]
    public async Task CreateTeam_RejectsOversizedAndInvalidPicturesWithoutCreatingTeam()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        await SeedAsync(owner);

        var oversized = new byte[CreateTeamValidator.MaximumProfilePictureLength + 1];
        var oversizedFile = new FormFile(
            new MemoryStream(oversized),
            0,
            oversized.Length,
            "ProfilePicture",
            "large.png"
        );
        var oversizedStatus = await CreateTeamAsync(owner.Id, oversizedFile);
        Assert.Equal(StatusCodes.Status400BadRequest, oversizedStatus);

        var invalid = new byte[] { 1, 2, 3, 4 };
        var invalidFile = new FormFile(new MemoryStream(invalid), 0, invalid.Length, "ProfilePicture", "invalid.png");
        var invalidStatus = await CreateTeamAsync(owner.Id, invalidFile);
        Assert.Equal(StatusCodes.Status400BadRequest, invalidStatus);

        await using var dbContext = _database.CreateDbContext();
        Assert.Empty(await dbContext.Teams.ToArrayAsync());
        Assert.Empty(await dbContext.TeamMemberships.ToArrayAsync());
    }

    [Fact]
    public async Task GetTeam_ReturnsNotFoundForUserWithoutAcceptedMembership()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var outsider = CreateUser("outsider@example.com", "Outsider");
        var team = CreateTeam(owner.Id);
        await SeedAsync(owner, outsider, team, CreateAcceptedMembership(team.Id, owner.Id));

        var status = await GetTeamStatusAsync(outsider.Id, team.Id);

        Assert.Equal(StatusCodes.Status404NotFound, status);
    }

    [Fact]
    public async Task Invite_RejectsSoloTeamWithoutCreatingMembershipOrNotification()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var team = CreateTeam(owner.Id, TeamKind.Solo);
        await SeedAsync(owner, invitee, team, CreateAcceptedMembership(team.Id, owner.Id));

        var status = await InviteAsync(owner.Id, team.Id, invitee.Email);

        Assert.Equal(StatusCodes.Status404NotFound, status);
        await using var dbContext = _database.CreateDbContext();
        Assert.Single(await dbContext.TeamMemberships.ToArrayAsync());
        Assert.Empty(await dbContext.Notifications.ToArrayAsync());
    }

    [Fact]
    public async Task InviteNotificationReadAndAcceptFlow_UpdatesCountsAndMembership()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var team = CreateTeam(owner.Id);
        var ownerMembership = CreateAcceptedMembership(team.Id, owner.Id);
        await SeedAsync(owner, invitee, team, ownerMembership);

        var missingStatus = await InviteAsync(owner.Id, team.Id, "missing@example.com");
        Assert.Equal(StatusCodes.Status202Accepted, missingStatus);
        await using (var missingContext = _database.CreateDbContext())
        {
            Assert.Empty(await missingContext.Notifications.ToArrayAsync());
        }

        var inviteStatus = await InviteAsync(owner.Id, team.Id, "INVITEE@EXAMPLE.COM");
        Assert.Equal(StatusCodes.Status202Accepted, inviteStatus);
        Guid inviteId;
        await using (var inviteContext = _database.CreateDbContext())
        {
            var invite = await inviteContext.TeamMemberships.SingleAsync(value => value.UserId == invitee.Id);
            inviteId = invite.Id;
            Assert.Equal(TeamMembershipStatus.Invited, invite.Status);
            Assert.Single(await inviteContext.Notifications.ToArrayAsync());
        }

        var teamResponse = await GetTeamAsync(owner.Id, team.Id);
        Assert.Single(teamResponse.Members);
        Assert.Equal(owner.Id, teamResponse.Members[0].Id);
        Assert.Equal(owner.PictureUrl, teamResponse.Members[0].PictureUrl);

        var inviteResponse = await GetInviteAsync(invitee.Id, inviteId);
        Assert.Equal(team.Id, inviteResponse.Team.Id);
        Assert.Equal(owner.Id, inviteResponse.InvitedBy.Id);
        Assert.Equal(owner.PictureUrl, inviteResponse.InvitedBy.PictureUrl);

        var beforeRead = await ListNotificationsAsync(invitee.Id);
        Assert.Equal(1, beforeRead.Metadata.TotalCount);
        Assert.Equal(1, beforeRead.Metadata.UnreadCount);
        Assert.Equal(1, beforeRead.Metadata.ActionableCount);
        Assert.Equal(1, beforeRead.Metadata.AttentionCount);
        Assert.Equal("TeamInvite", beforeRead.Items[0].Type);
        Assert.Equal(inviteId, beforeRead.Items[0].ResourceId);

        await ReadNotificationsAsync(invitee.Id);
        var afterRead = await ListNotificationsAsync(invitee.Id);
        Assert.Equal(0, afterRead.Metadata.UnreadCount);
        Assert.Equal(1, afterRead.Metadata.ActionableCount);
        Assert.Equal(1, afterRead.Metadata.AttentionCount);

        var responseStatus = await RespondAsync(invitee.Id, inviteId, true);
        Assert.Equal(StatusCodes.Status204NoContent, responseStatus);
        var afterAccept = await ListNotificationsAsync(invitee.Id);
        Assert.Equal(0, afterAccept.Metadata.TotalCount);
        await using var acceptedContext = _database.CreateDbContext();
        var accepted = await acceptedContext.TeamMemberships.SingleAsync(value => value.Id == inviteId);
        Assert.Equal(TeamMembershipStatus.Accepted, accepted.Status);
        Assert.Equal(Now, accepted.AcceptedAt);
        var notification = await acceptedContext.Notifications.SingleAsync();
        Assert.Equal(Now, notification.ReadAt);
        Assert.Equal(Now, notification.ResolvedAt);

        var idempotentStatus = await RespondAsync(invitee.Id, inviteId, true);
        Assert.Equal(StatusCodes.Status204NoContent, idempotentStatus);
        var declineAcceptedStatus = await RespondAsync(invitee.Id, inviteId, false);
        Assert.Equal(StatusCodes.Status409Conflict, declineAcceptedStatus);
    }

    [Fact]
    public async Task Decline_RemovesPendingMembershipRetainsNotificationAndAllowsReinvite()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var outsider = CreateUser("outsider@example.com", "Outsider");
        var team = CreateTeam(owner.Id);
        await SeedAsync(owner, invitee, outsider, team, CreateAcceptedMembership(team.Id, owner.Id));
        await InviteAsync(owner.Id, team.Id, invitee.Email);

        Guid firstInviteId;
        await using (var context = _database.CreateDbContext())
        {
            firstInviteId = await context
                .TeamMemberships.Where(value => value.UserId == invitee.Id)
                .Select(value => value.Id)
                .SingleAsync();
        }

        var wrongUserStatus = await RespondAsync(outsider.Id, firstInviteId, false);
        Assert.Equal(StatusCodes.Status404NotFound, wrongUserStatus);
        var declineStatus = await RespondAsync(invitee.Id, firstInviteId, false);
        Assert.Equal(StatusCodes.Status204NoContent, declineStatus);
        await using (var declinedContext = _database.CreateDbContext())
        {
            Assert.False(await declinedContext.TeamMemberships.AnyAsync(value => value.Id == firstInviteId));
            var resolved = await declinedContext.Notifications.SingleAsync();
            Assert.Equal(Now, resolved.ResolvedAt);
        }

        await InviteAsync(owner.Id, team.Id, invitee.Email);
        await using var reinvitedContext = _database.CreateDbContext();
        var secondInviteId = await reinvitedContext
            .TeamMemberships.Where(value => value.UserId == invitee.Id)
            .Select(value => value.Id)
            .SingleAsync();
        Assert.NotEqual(firstInviteId, secondInviteId);
        Assert.Equal(2, await reinvitedContext.Notifications.CountAsync());
        Assert.Single(await reinvitedContext.Notifications.Where(value => value.ResolvedAt == null).ToArrayAsync());
    }

    [Fact]
    public async Task ConcurrentInvites_CreateOnePendingMembershipAndNotification()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var team = CreateTeam(owner.Id);
        await SeedAsync(owner, invitee, team, CreateAcceptedMembership(team.Id, owner.Id));

        var statuses = await Task.WhenAll(
            InviteAsync(owner.Id, team.Id, invitee.Email),
            InviteAsync(owner.Id, team.Id, invitee.Email)
        );

        Assert.All(statuses, status => Assert.Equal(StatusCodes.Status202Accepted, status));
        await using var dbContext = _database.CreateDbContext();
        Assert.Single(await dbContext.TeamMemberships.Where(value => value.UserId == invitee.Id).ToArrayAsync());
        Assert.Single(await dbContext.Notifications.Where(value => value.UserId == invitee.Id).ToArrayAsync());
    }

    [Fact]
    public async Task Invite_PublishesCreatedNotificationEventToInvitee()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var team = CreateTeam(owner.Id);
        await SeedAsync(owner, invitee, team, CreateAcceptedMembership(team.Id, owner.Id));

        using var subscription = _streamService.Subscribe(invitee.Id);
        var status = await InviteAsync(owner.Id, team.Id, invitee.Email);

        Assert.Equal(StatusCodes.Status202Accepted, status);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var streamEvent = await subscription.Reader.ReadAsync(cts.Token);
        Assert.Equal(NotificationStreamEventKinds.Created, streamEvent.Kind);
        var payload = Assert.IsType<NotificationResponse>(streamEvent.Data);
        await using var dbContext = _database.CreateDbContext();
        var notification = await dbContext.Notifications.SingleAsync();
        Assert.Equal(notification.Id, payload.Id);
        Assert.Equal("TeamInvite", payload.Type);
        Assert.Equal(notification.ResourceId, payload.ResourceId);
        Assert.Equal(Now, payload.CreatedAt);
        Assert.Null(payload.ReadAt);
        Assert.True(payload.IsUnread);
        Assert.True(payload.IsActionable);
    }

    [Fact]
    public async Task RespondAndRead_PublishResolvedAndReadNotificationEvents()
    {
        await _database.ResetAsync();
        var owner = CreateUser("owner@example.com", "Owner");
        var invitee = CreateUser("invitee@example.com", "Invitee");
        var team = CreateTeam(owner.Id);
        await SeedAsync(owner, invitee, team, CreateAcceptedMembership(team.Id, owner.Id));
        await InviteAsync(owner.Id, team.Id, invitee.Email);
        Guid inviteId;
        await using (var context = _database.CreateDbContext())
        {
            inviteId = await context
                .TeamMemberships.Where(value => value.UserId == invitee.Id)
                .Select(value => value.Id)
                .SingleAsync();
        }

        using var subscription = _streamService.Subscribe(invitee.Id);
        var respondStatus = await RespondAsync(invitee.Id, inviteId, true);
        Assert.Equal(StatusCodes.Status204NoContent, respondStatus);
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var resolvedEvent = await subscription.Reader.ReadAsync(cts.Token);
        Assert.Equal(NotificationStreamEventKinds.Resolved, resolvedEvent.Kind);
        var resolvedPayload = Assert.IsType<NotificationResolvedEventData>(resolvedEvent.Data);
        Assert.Equal(inviteId, resolvedPayload.ResourceId);

        await ReadNotificationsAsync(invitee.Id);
        var readEvent = await subscription.Reader.ReadAsync(cts.Token);
        Assert.Equal(NotificationStreamEventKinds.Read, readEvent.Kind);
        var readPayload = Assert.IsType<NotificationReadEventData>(readEvent.Data);
        Assert.Equal(Now, readPayload.ReadAt);
    }

    [Fact]
    public async Task StreamEndpoint_WritesPublishedEventsAsSseFrames()
    {
        var userId = Guid.CreateVersion7();
        var context = CreateHttpContext(userId);
        using var cts = new CancellationTokenSource();
        context.RequestAborted = cts.Token;
        var endpoint = Factory.Create<NotificationsStreamEndpoint>(context, _streamService);
        var handleTask = endpoint.HandleAsync(CancellationToken.None);

        var body = (MemoryStream)context.Response.Body;
        for (var attempt = 0; attempt < 50 && body.Length == 0; attempt++)
        {
            await Task.Delay(50);
        }

        Assert.True(body.Length > 0);
        var connectedLength = body.Length;
        var notification = new NotificationResponse(
            Guid.CreateVersion7(),
            "TeamInvite",
            Guid.CreateVersion7(),
            Now,
            null,
            true,
            true
        );
        _streamService.Publish(userId, new NotificationStreamEvent(NotificationStreamEventKinds.Created, notification));
        for (var attempt = 0; attempt < 50 && body.Length == connectedLength; attempt++)
        {
            await Task.Delay(50);
        }

        await cts.CancelAsync();
        await handleTask;

        body.Position = 0;
        var content = await new StreamReader(body).ReadToEndAsync();
        Assert.Equal("text/event-stream", context.Response.ContentType);
        Assert.Contains(": connected", content);
        Assert.Contains("event: notification.created", content);
        Assert.Contains($"\"resourceId\":\"{notification.ResourceId}\"", content);
    }

    private async Task<int> CreateTeamAsync(Guid userId, IFormFile profilePicture)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<CreateTeamEndpoint>(
            context,
            dbContext,
            _dateTimeProvider,
            new TeamProfilePictureProcessor()
        );
        await endpoint.HandleAsync(new CreateTeamRequest { Name = "Apes", ProfilePicture = profilePicture }, default);
        return context.Response.StatusCode;
    }

    private async Task<int> InviteAsync(Guid userId, Guid teamId, string email)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<CreateTeamInviteEndpoint>(context, dbContext, _dateTimeProvider, _streamService);
        await endpoint.HandleAsync(new CreateTeamInviteRequest { TeamId = teamId, Email = email }, default);
        return context.Response.StatusCode;
    }

    private async Task<TeamResponse> GetTeamAsync(Guid userId, Guid teamId)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<GetTeamEndpoint>(context, dbContext);
        await endpoint.HandleAsync(new GetTeamRequest { TeamId = teamId }, default);
        return await ReadResponseAsync<TeamResponse>(context);
    }

    private async Task<int> GetTeamStatusAsync(Guid userId, Guid teamId)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<GetTeamEndpoint>(context, dbContext);
        await endpoint.HandleAsync(new GetTeamRequest { TeamId = teamId }, default);
        return context.Response.StatusCode;
    }

    private async Task<TeamInviteResponse> GetInviteAsync(Guid userId, Guid inviteId)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<GetTeamInviteEndpoint>(context, dbContext);
        await endpoint.HandleAsync(new GetTeamInviteRequest { InviteId = inviteId }, default);
        return await ReadResponseAsync<TeamInviteResponse>(context);
    }

    private async Task<NotificationsResponse> ListNotificationsAsync(Guid userId)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<ListNotificationsEndpoint>(context, dbContext);
        await endpoint.HandleAsync(default);
        return await ReadResponseAsync<NotificationsResponse>(context);
    }

    private async Task ReadNotificationsAsync(Guid userId)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<ReadNotificationsEndpoint>(context, dbContext, _dateTimeProvider, _streamService);
        await endpoint.HandleAsync(default);
        Assert.Equal(StatusCodes.Status204NoContent, context.Response.StatusCode);
    }

    private async Task<int> RespondAsync(Guid userId, Guid inviteId, bool accept)
    {
        await using var dbContext = _database.CreateDbContext();
        var context = CreateHttpContext(userId);
        var endpoint = Factory.Create<RespondToTeamInviteEndpoint>(
            context,
            dbContext,
            _dateTimeProvider,
            _streamService
        );
        await endpoint.HandleAsync(new RespondToTeamInviteRequest { InviteId = inviteId, Accept = accept }, default);
        return context.Response.StatusCode;
    }

    private async Task SeedAsync(params object[] entities)
    {
        await using var dbContext = _database.CreateDbContext();
        dbContext.AddRange(entities);
        await dbContext.SaveChangesAsync();
    }

    private static DefaultHttpContext CreateHttpContext(Guid userId)
    {
        var context = new DefaultHttpContext();
        context.User = new ClaimsPrincipal(new ClaimsIdentity([new Claim("ApesDbUserId", userId.ToString())], "Test"));
        context.Response.Body = new MemoryStream();
        return context;
    }

    private static async Task<T> ReadResponseAsync<T>(DefaultHttpContext context)
    {
        context.Response.Body.Position = 0;
        var response = await JsonSerializer.DeserializeAsync<T>(
            context.Response.Body,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
        );
        return Assert.IsType<T>(response);
    }

    private static User CreateUser(string email, string name)
    {
        return new User
        {
            Id = Guid.CreateVersion7(),
            Auth0Subject = $"auth0|{Guid.NewGuid():N}",
            Email = email,
            Name = name,
            PictureUrl = $"https://images.example.com/{email}.png",
            CreatedAt = Now,
            UpdatedAt = Now,
        };
    }

    private static Team CreateTeam(Guid ownerId, TeamKind kind = TeamKind.Group)
    {
        return new Team
        {
            Id = Guid.CreateVersion7(),
            OwnerUserId = ownerId,
            Name = "Apes",
            Kind = kind,
            CreatedAt = Now,
            UpdatedAt = Now,
        };
    }

    private static TeamMembership CreateAcceptedMembership(Guid teamId, Guid userId)
    {
        return new TeamMembership
        {
            Id = Guid.CreateVersion7(),
            TeamId = teamId,
            UserId = userId,
            Status = TeamMembershipStatus.Accepted,
            InvitedAt = Now,
            AcceptedAt = Now,
        };
    }

    private static byte[] CreatePng(int width, int height)
    {
        using var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.CornflowerBlue);
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }
}
