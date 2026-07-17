using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Teams.CreateTeam;

public sealed class CreateTeamEndpoint : Endpoint<CreateTeamRequest, TeamResponse>
{
    public const long MaximumProfilePictureLength = 5 * 1024 * 1024;

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
            if (request.ProfilePicture.Length > MaximumProfilePictureLength)
            {
                await Send.StatusCodeAsync(StatusCodes.Status413PayloadTooLarge, ct);
                return;
            }

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
        if (name.Length == 0)
        {
            AddError(request => request.Name, "A team name is required.");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

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

        var creatorName = await _dbContext
            .Users.Where(user => user.Id == userId)
            .Select(user => user.Name)
            .SingleAsync(ct);
        var response = new TeamResponse(
            team.Id,
            team.Name,
            TeamResponseFactory.CreateKind(team.Kind),
            team.CreatedAt,
            TeamResponseFactory.CreateProfilePicture(team.ProfilePicture),
            [new TeamMemberResponse(userId, creatorName)]
        );
        HttpContext.Response.Headers.Location = $"/{ApiRoutes.Api.Prefix}/teams/{team.Id}";
        await Send.ResponseAsync(response, StatusCodes.Status201Created, ct);
    }
}
