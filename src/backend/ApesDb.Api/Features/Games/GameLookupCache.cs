namespace ApesDb.Api.Features.Games;

public static class GameLookupCache
{
    public const string CacheName = "GameLookups";
    public const string CacheKeyPrefix = "games:lookups:";

    public static readonly TimeSpan Expiration = TimeSpan.FromHours(1);
}
