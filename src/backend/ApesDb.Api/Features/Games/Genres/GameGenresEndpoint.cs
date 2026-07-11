using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Features.Games.Genres;

public sealed class GameGenresEndpoint : EndpointWithoutRequest<GenreResponse[]>
{
    public const string CacheKey = "genres";

    private readonly ApplicationDbContext _dbContext;
    private readonly IFusionCache _cache;

    public GameGenresEndpoint(ApplicationDbContext dbContext, IFusionCacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cache = cacheProvider.GetCache(GameLookupCache.CacheName);
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Genres);
        Summary(summary => summary.Summary = "Lists all synchronized game genres.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _cache.GetOrSetAsync(
            CacheKey,
            token =>
                _dbContext
                    .Genres.AsNoTracking()
                    .OrderBy(value => value.Name.ToLower())
                    .ThenBy(value => value.Name)
                    .ThenBy(value => value.Id)
                    .Select(value => new GenreResponse(value.Id, value.Name))
                    .ToArrayAsync(token),
            token: ct
        );

        await Send.OkAsync(response, ct);
    }
}
