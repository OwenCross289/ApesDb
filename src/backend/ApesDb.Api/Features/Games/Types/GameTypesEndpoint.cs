using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Types;

public sealed class GameTypesEndpoint : EndpointWithoutRequest<GameTypeResponse[]>
{
    public const string CacheKey = "types";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GameTypesEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Types);
        Summary(summary => summary.Summary = "Lists all synchronized game types.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .GameTypes.AsNoTracking()
                    .OrderBy(value => value.Name.ToLower())
                    .ThenBy(value => value.Name)
                    .ThenBy(value => value.Id)
                    .Select(value => new GameTypeResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
