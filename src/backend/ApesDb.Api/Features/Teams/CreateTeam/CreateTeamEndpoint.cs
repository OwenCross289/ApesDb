using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Teams;
using ApesDb.Domain.Entities.Users;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.CreateTeam;

public sealed class CreateTeamEndpoint : Endpoint<CreateTeamRequest, TeamResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITeamProfilePictureProcessor _profilePictureProcessor;

    public CreateTeamEndpoint(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        ITeamProfilePictureProcessor profilePictureProcessor
    )
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _profilePictureProcessor = profilePictureProcessor;
    }

    public override void Configure()
    {
        Post(ApiRoutes.Teams.Create);
        AllowFileUploads();
        Summary(summary => summary.Summary = "Creates a team for the authenticated user.");
    }

    public override async Task HandleAsync(CreateTeamRequest request, CancellationToken ct)
    {
        byte[]? profilePicture = null;
        if (request.ProfilePicture is not null)
        {
            try
            {
                await using var stream = request.ProfilePicture.OpenReadStream();
                profilePicture = _profilePictureProcessor.Process(stream);
            }
            catch (InvalidTeamProfilePictureException exception)
            {
                AddError(request => request.ProfilePicture, exception.Message);
                await Send.ErrorsAsync(cancellation: ct);
                return;
            }
        }

        var name = request.Name.Trim();
        var userId = User.GetApesDbUserId();
        var now = _dateTimeProvider.UtcNow;
        var team = new Team
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            OwnerUserId = userId,
            ProfilePicture = profilePicture,
            Kind = TeamKind.Group,
            CreatedAt = now,
            UpdatedAt = now,
        };
        var membership = new TeamMembership
        {
            Id = Guid.CreateVersion7(),
            TeamId = team.Id,
            UserId = userId,
            Status = TeamMembershipStatus.Accepted,
            InvitedAt = now,
            AcceptedAt = now,
        };

        _dbContext.Teams.Add(team);
        _dbContext.TeamMemberships.Add(membership);
        await _dbContext.SaveChangesAsync(ct);

        var creator = await _dbContext
            .Users.Where(user => user.Id == userId)
            .Select(user => new { user.Name, user.PictureUrl })
            .SingleAsync(ct);
        var response = new TeamResponse(
            team.Id,
            team.Name,
            TeamResponseFactory.CreateKind(team.Kind),
            team.CreatedAt,
            TeamResponseFactory.CreateProfilePicture(team.ProfilePicture),
            [new TeamMemberResponse(userId, creator.Name, creator.PictureUrl)]
        );
        HttpContext.Response.Headers.Location = $"/{ApiRoutes.Api.Prefix}/teams/{team.Id}";
        await Send.ResponseAsync(response, StatusCodes.Status201Created, ct);
    }
}
