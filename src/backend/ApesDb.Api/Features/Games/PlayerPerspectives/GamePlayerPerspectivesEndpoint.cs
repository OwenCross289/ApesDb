using System.ComponentModel;
using ApesDb.Common;
using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.PlayerPerspectives;

public sealed class GamePlayerPerspectivesEndpoint : EndpointWithoutRequest<PlayerPerspectiveResponse[]>
{
    public const string CacheKey = "player-perspectives";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GamePlayerPerspectivesEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.PlayerPerspectives);
        Summary(summary => summary.Summary = "Lists all synchronized player perspectives.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .PlayerPerspectives.AsNoTracking()
                    .SortBy(
                        ListSortDirection.Ascending,
                        value => value.Name.ToLower(),
                        value => value.Name,
                        value => value.Id
                    )
                    .Select(value => new PlayerPerspectiveResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
