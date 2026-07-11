using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Statuses;

public sealed class GameStatusesEndpoint : EndpointWithoutRequest<GameStatusResponse[]>
{
    public const string CacheKey = "statuses";

    private const long ReleasedStatusId = 0;
    private const string ReleasedStatusName = "Released";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GameStatusesEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Statuses);
        Summary(summary => summary.Summary = "Lists all synchronized game statuses.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            async token =>
            {
                var stored = await _dbContext
                    .GameStatuses.AsNoTracking()
                    .Select(value => new GameStatusResponse(value.Id, value.Name))
                    .ToArrayAsync(token);
                var values = stored.Any(value => value.Id == ReleasedStatusId)
                    ? stored
                    : [.. stored, new GameStatusResponse(ReleasedStatusId, ReleasedStatusName)];

                return values
                    .OrderBy(value => value.Name, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(value => value.Name, StringComparer.Ordinal)
                    .ThenBy(value => value.Id)
                    .ToArray();
            },
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
