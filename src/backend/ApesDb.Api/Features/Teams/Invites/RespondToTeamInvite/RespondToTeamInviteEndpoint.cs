using ApesDb.Api.Features.Notifications;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.Invites.RespondToTeamInvite;

public sealed class RespondToTeamInviteEndpoint : Endpoint<RespondToTeamInviteRequest>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly NotificationStreamService _streamService;

    public RespondToTeamInviteEndpoint(
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
        Post(ApiRoutes.Teams.RespondToInvite);
        Summary(summary => summary.Summary = "Accepts or declines a team invitation.");
    }

    public override async Task HandleAsync(RespondToTeamInviteRequest request, CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var now = _dateTimeProvider.UtcNow;
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        var affectedMemberships = 0;
        if (request.Accept)
        {
            affectedMemberships = await _dbContext
                .TeamMemberships.Where(membership =>
                    membership.Id == request.InviteId
                    && membership.UserId == userId
                    && membership.Status == TeamMembershipStatus.Invited
                )
                .ExecuteUpdateAsync(
                    setters =>
                        setters
                            .SetProperty(membership => membership.Status, TeamMembershipStatus.Accepted)
                            .SetProperty(membership => membership.AcceptedAt, now),
                    ct
                );
        }
        else
        {
            affectedMemberships = await _dbContext
                .TeamMemberships.Where(membership =>
                    membership.Id == request.InviteId
                    && membership.UserId == userId
                    && membership.Status == TeamMembershipStatus.Invited
                )
                .ExecuteDeleteAsync(ct);
        }

        if (affectedMemberships == 0)
        {
            var status = await _dbContext
                .TeamMemberships.AsNoTracking()
                .Where(membership => membership.Id == request.InviteId && membership.UserId == userId)
                .Select(membership => (TeamMembershipStatus?)membership.Status)
                .SingleOrDefaultAsync(ct);
            await transaction.RollbackAsync(ct);

            if (status == TeamMembershipStatus.Accepted)
            {
                if (request.Accept)
                {
                    await Send.NoContentAsync(ct);
                    return;
                }

                await Send.StatusCodeAsync(StatusCodes.Status409Conflict, ct);
                return;
            }

            await Send.NotFoundAsync(ct);
            return;
        }

        await _dbContext
            .Notifications.Where(notification =>
                notification.UserId == userId
                && notification.Type == NotificationType.TeamInvite
                && notification.ResourceId == request.InviteId
                && notification.ResolvedAt == null
            )
            .ExecuteUpdateAsync(
                setters =>
                    setters
                        .SetProperty(notification => notification.IsActionable, false)
                        .SetProperty(notification => notification.ReadAt, now)
                        .SetProperty(notification => notification.ResolvedAt, now),
                ct
            );
        await transaction.CommitAsync(ct);
        _streamService.Publish(
            userId,
            new NotificationStreamEvent(
                NotificationStreamEventKinds.Resolved,
                new NotificationResolvedEventData(request.InviteId)
            )
        );
        await Send.NoContentAsync(ct);
    }
}
