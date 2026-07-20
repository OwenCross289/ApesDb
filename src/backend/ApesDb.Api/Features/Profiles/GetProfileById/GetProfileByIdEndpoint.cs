using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Profiles.GetProfileById;

public sealed class GetProfileByIdEndpoint : Endpoint<GetProfileByIdRequest, ProfileResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetProfileByIdEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Profiles.ById);
        AllowAnonymous();
        Summary(summary =>
        {
            summary.Summary = "Gets a visible profile by ID.";
            summary.Description = "Returns a public profile, or the authenticated user's own private profile.";
        });
    }

    public override async Task HandleAsync(GetProfileByIdRequest request, CancellationToken ct)
    {
        var query = _dbContext.Profiles.AsNoTracking().Where(value => value.UserId == request.Id);

        if (User.Identity?.IsAuthenticated != true)
        {
            query = query.Where(value => value.IsPublic);
        }
        else
        {
            var viewerId = User.GetApesDbUserId();
            if (viewerId != request.Id)
            {
                query = query.Where(value => value.IsPublic);
            }
        }

        var profile = await query
            .Select(value => new ProfileResponse(
                value.UserId,
                value.User.Name,
                value.User.PictureUrl,
                value.AboutMe,
                value.IsPublic
            ))
            .SingleOrDefaultAsync(ct);

        if (profile is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(profile, ct);
    }
}
