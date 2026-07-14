using System.ComponentModel;
using ApesDb.Common;
using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Themes;

public sealed class GameThemesEndpoint : EndpointWithoutRequest<ThemeResponse[]>
{
    public const string CacheKey = "themes";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GameThemesEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Themes);
        Summary(summary => summary.Summary = "Lists all synchronized game themes.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .Themes.AsNoTracking()
                    .SortBy(
                        ListSortDirection.Ascending,
                        value => value.Name.ToLower(),
                        value => value.Name,
                        value => value.Id
                    )
                    .Select(value => new ThemeResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
