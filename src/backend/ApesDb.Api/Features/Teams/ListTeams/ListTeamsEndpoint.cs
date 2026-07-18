using ApesDb.Api.Features.Teams;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Teams;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.ListTeams;

public sealed class ListTeamsEndpoint : EndpointWithoutRequest<TeamResponse[]>
{
    private readonly ApplicationDbContext _dbContext;

    public ListTeamsEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Teams.List);
        Summary(summary => summary.Summary = "Lists the teams available to the current user.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();

        var teams = await _dbContext
            .Teams.AsNoTracking()
            .Where(team =>
                _dbContext.TeamMemberships.Any(membership =>
                    membership.TeamId == team.Id
                    && membership.UserId == userId
                    && membership.Status == TeamMembershipStatus.Accepted
                )
            )
            .OrderByDescending(team => team.Kind == TeamKind.Solo)
            .ThenBy(team => team.Name.ToLower())
            .ThenBy(team => team.Id)
            .ToArrayAsync(ct);

        var response = teams
            .Select(team => new TeamResponse(
                team.Id,
                team.Name,
                TeamResponseFactory.CreateProfilePicture(team.ProfilePicture),
                MapKind(team.Kind)
            ))
            .ToArray();

        await Send.OkAsync(response, ct);
    }

    private static string MapKind(TeamKind kind)
    {
        if (kind == TeamKind.Solo)
        {
            return "solo";
        }

        return "group";
    }
}
