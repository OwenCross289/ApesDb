namespace ApesDb.Api.Features.Games;

public static class GameCache
{
    public const string CacheName = "Games";
    public const string CacheKeyPrefix = "games:";

    public static readonly TimeSpan Expiration = TimeSpan.FromHours(1);
}
