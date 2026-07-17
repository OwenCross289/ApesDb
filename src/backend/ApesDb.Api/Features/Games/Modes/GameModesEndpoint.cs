using System.ComponentModel;
using ApesDb.Common;
using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Modes;

public sealed class GameModesEndpoint : EndpointWithoutRequest<GameModeResponse[]>
{
    public const string CacheKey = "modes";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GameModesEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Modes);
        Summary(summary => summary.Summary = "Lists all synchronized game modes.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .GameModes.AsNoTracking()
                    .SortBy(
                        ListSortDirection.Ascending,
                        value => value.Name.ToLower(),
                        value => value.Name,
                        value => value.Id
                    )
                    .Select(value => new GameModeResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
