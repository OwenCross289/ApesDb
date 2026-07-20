using System.ComponentModel;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Users;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Profiles.GetProfiles;

public sealed class GetProfilesEndpoint : Endpoint<GetProfilesRequest, Pagable<ProfileResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetProfilesEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Profiles.Get);
        AllowAnonymous();
        Summary(summary =>
        {
            summary.Summary = "Lists public profiles.";
            summary.Description = "Returns public profiles sorted by name with search and pagination support.";
        });
    }

    public override async Task HandleAsync(GetProfilesRequest request, CancellationToken ct)
    {
        var baseQuery = _dbContext.Profiles.AsNoTracking().Where(profile => profile.IsPublic);
        var query = baseQuery.WhereContains(request.Search, profile => profile.User.Name);

        var total = await baseQuery.CountAsync(ct);
        var filteredTotal = await query.CountAsync(ct);
        var items = await Query(query, request, ct);

        await Send.OkAsync(
            new Pagable<ProfileResponse>(items, total, filteredTotal, request.Page, request.PageSize),
            ct
        );
    }

    private static Task<ProfileResponse[]> Query(
        IQueryable<Profile> query,
        GetProfilesRequest request,
        CancellationToken ct
    )
    {
        return query
            .SortBy(
                ListSortDirection.Ascending,
                profile => profile.User.Name.ToLower(),
                profile => profile.User.Name,
                profile => profile.UserId
            )
            .Page(request.Page, request.PageSize)
            .Select(profile => new ProfileResponse(
                profile.UserId,
                profile.User.Name,
                profile.User.PictureUrl,
                profile.AboutMe,
                profile.IsPublic
            ))
            .ToArrayAsync(ct);
    }
}
