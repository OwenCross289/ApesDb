using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.Invites.GetTeamInvite;

public sealed class GetTeamInviteEndpoint : Endpoint<GetTeamInviteRequest, TeamInviteResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetTeamInviteEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Teams.InviteById);
        Summary(summary => summary.Summary = "Gets a pending team invitation.");
    }

    public override async Task HandleAsync(GetTeamInviteRequest request, CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var invite = await _dbContext
            .TeamMemberships.AsNoTracking()
            .Where(membership =>
                membership.Id == request.InviteId
                && membership.UserId == userId
                && membership.Status == TeamMembershipStatus.Invited
                && membership.InvitedByUserId != null
            )
            .Select(membership => new
            {
                membership.Id,
                TeamId = membership.Team.Id,
                TeamName = membership.Team.Name,
                membership.Team.ProfilePicture,
                InvitedById = membership.InvitedByUserId!.Value,
                InvitedByName = membership.InvitedByUser!.Name,
                InvitedByPictureUrl = membership.InvitedByUser.PictureUrl,
                membership.InvitedAt,
            })
            .SingleOrDefaultAsync(ct);
        if (invite is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(
            new TeamInviteResponse(
                invite.Id,
                new TeamInviteTeamResponse(
                    invite.TeamId,
                    invite.TeamName,
                    TeamResponseFactory.CreateProfilePicture(invite.ProfilePicture)
                ),
                new TeamMemberResponse(invite.InvitedById, invite.InvitedByName, invite.InvitedByPictureUrl),
                invite.InvitedAt
            ),
            ct
        );
    }
}
