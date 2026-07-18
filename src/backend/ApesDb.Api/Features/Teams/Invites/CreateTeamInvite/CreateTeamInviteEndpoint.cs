using ApesDb.Api.Features.Notifications.ListNotifications;
using ApesDb.Api.Features.Notifications.NotificationsStream;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Notifications;
using ApesDb.Domain.Entities.Teams;
using ApesDb.Domain.Entities.Users;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;

public sealed class CreateTeamInviteEndpoint : Endpoint<CreateTeamInviteRequest>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly NotificationStreamService _streamService;

    public CreateTeamInviteEndpoint(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        NotificationStreamService streamService
    )
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _streamService = streamService;
    }

    public override void Configure()
    {
        Post(ApiRoutes.Teams.Invites);
        Summary(summary => summary.Summary = "Invites an existing user to a team.");
    }

    public override async Task HandleAsync(CreateTeamInviteRequest request, CancellationToken ct)
    {
        var inviterId = User.GetApesDbUserId();
        var canInvite = await _dbContext.Teams.AnyAsync(
            team =>
                team.Id == request.TeamId
                && team.Kind == TeamKind.Group
                && _dbContext.TeamMemberships.Any(membership =>
                    membership.TeamId == team.Id
                    && membership.UserId == inviterId
                    && membership.Status == TeamMembershipStatus.Accepted
                ),
            ct
        );
        if (!canInvite)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var targetIds = await _dbContext
            .Users.AsNoTracking()
            .Where(user => user.Email.ToLower() == normalizedEmail)
            .OrderBy(user => user.Id)
            .Select(user => user.Id)
            .Take(2)
            .ToArrayAsync(ct);
        if (targetIds.Length != 1 || targetIds[0] == inviterId)
        {
            await Send.StatusCodeAsync(StatusCodes.Status202Accepted, ct);
            return;
        }

        var targetId = targetIds[0];
        var alreadyBelongsToTeam = await _dbContext.TeamMemberships.AnyAsync(
            membership => membership.TeamId == request.TeamId && membership.UserId == targetId,
            ct
        );
        if (alreadyBelongsToTeam)
        {
            await Send.StatusCodeAsync(StatusCodes.Status202Accepted, ct);
            return;
        }

        var now = _dateTimeProvider.UtcNow;
        var invite = new TeamMembership
        {
            Id = Guid.CreateVersion7(),
            TeamId = request.TeamId,
            UserId = targetId,
            Status = TeamMembershipStatus.Invited,
            InvitedByUserId = inviterId,
            InvitedAt = now,
        };
        var notification = new Notification
        {
            Id = Guid.CreateVersion7(),
            UserId = targetId,
            Type = NotificationType.TeamInvite,
            ResourceId = invite.Id,
            IsActionable = true,
            CreatedAt = now,
        };

        _dbContext.TeamMemberships.Add(invite);
        _dbContext.Notifications.Add(notification);
        try
        {
            await _dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException exception) when (IsUniqueViolation(exception))
        {
            _dbContext.ChangeTracker.Clear();
            await Send.StatusCodeAsync(StatusCodes.Status202Accepted, ct);
            return;
        }

        _streamService.Publish(
            targetId,
            new NotificationStreamEvent(
                NotificationStreamEventKinds.Created,
                new NotificationResponse(
                    notification.Id,
                    notification.Type.ToString(),
                    notification.ResourceId,
                    now,
                    null,
                    true,
                    true
                )
            )
        );

        await Send.StatusCodeAsync(StatusCodes.Status202Accepted, ct);
    }

    private static bool IsUniqueViolation(DbUpdateException exception)
    {
        return exception.InnerException is PostgresException postgresException
            && postgresException.SqlState == PostgresErrorCodes.UniqueViolation;
    }
}
