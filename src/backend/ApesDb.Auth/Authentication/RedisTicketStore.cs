using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Auth.Authentication;

public sealed class RedisTicketStore : ITicketStore
{
    public const string CacheName = "AuthTickets";
    public const string CacheKeyPrefix = "auth:ticket:";

    private const string DataProtectorPurpose = "ApesDb.AuthTicket";
    private static readonly TimeSpan Expiration = TimeSpan.FromDays(7);

    private readonly IFusionCache _cache;
    private readonly IDataProtector _protector;

    public RedisTicketStore(
        IFusionCacheProvider cacheProvider,
        IDataProtectionProvider dataProtectionProvider
    )
    {
        _cache = cacheProvider.GetCache(CacheName);
        _protector = dataProtectionProvider.CreateProtector(DataProtectorPurpose);
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString("n");
        await RenewAsync(key, ticket);
        return key;
    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        var serialized = TicketSerializer.Default.Serialize(ticket);
        var protectedTicket = _protector.Protect(serialized);

        await _cache.SetAsync(key, protectedTicket, options => options.SetDuration(Expiration));
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var protectedTicket = await _cache.GetOrDefaultAsync<byte[]>(key);

        if (protectedTicket is null)
        {
            return null;
        }

        await _cache.SetAsync(key, protectedTicket, options => options.SetDuration(Expiration));

        var serialized = _protector.Unprotect(protectedTicket);
        return TicketSerializer.Default.Deserialize(serialized);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}
