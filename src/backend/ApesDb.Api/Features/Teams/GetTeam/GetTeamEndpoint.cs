using ApesDb.Domain;
using ApesDb.Domain.Entities.Teams;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.GetTeam;

public sealed class GetTeamEndpoint : Endpoint<GetTeamRequest, TeamResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetTeamEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Teams.ById);
        Summary(summary => summary.Summary = "Gets a team and its accepted members.");
    }

    public override async Task HandleAsync(GetTeamRequest request, CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var team = await _dbContext
            .Teams.AsNoTracking()
            .Where(value =>
                value.Id == request.TeamId
                && _dbContext.TeamMemberships.Any(membership =>
                    membership.TeamId == value.Id
                    && membership.UserId == userId
                    && membership.Status == TeamMembershipStatus.Accepted
                )
            )
            .Select(value => new
            {
                value.Id,
                value.Name,
                value.Kind,
                value.CreatedAt,
                value.ProfilePicture,
            })
            .SingleOrDefaultAsync(ct);

        if (team is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var members = await _dbContext
            .TeamMemberships.AsNoTracking()
            .Where(membership =>
                membership.TeamId == request.TeamId && membership.Status == TeamMembershipStatus.Accepted
            )
            .OrderBy(membership => membership.User.Name.ToLower())
            .ThenBy(membership => membership.User.Name)
            .ThenBy(membership => membership.UserId)
            .Select(membership => new TeamMemberResponse(
                membership.UserId,
                membership.User.Name,
                membership.User.PictureUrl
            ))
            .ToArrayAsync(ct);

        await Send.OkAsync(
            new TeamResponse(
                team.Id,
                team.Name,
                TeamResponseFactory.CreateKind(team.Kind),
                team.CreatedAt,
                TeamResponseFactory.CreateProfilePicture(team.ProfilePicture),
                members
            ),
            ct
        );
    }
}
