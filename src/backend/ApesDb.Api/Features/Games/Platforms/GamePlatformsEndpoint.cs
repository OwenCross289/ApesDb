using System.ComponentModel;
using ApesDb.Common;
using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Platforms;

public sealed class GamePlatformsEndpoint : EndpointWithoutRequest<PlatformResponse[]>
{
    public const string CacheKey = "platforms";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GamePlatformsEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Platforms);
        Summary(summary => summary.Summary = "Lists all synchronized game platforms.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .Platforms.AsNoTracking()
                    .SortBy(
                        ListSortDirection.Ascending,
                        value => value.Name.ToLower(),
                        value => value.Name,
                        value => value.Id
                    )
                    .Select(value => new PlatformResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
