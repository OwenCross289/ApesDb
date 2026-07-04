using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api.Authentication;

public sealed class RedisTicketStore : ITicketStore
{
    private const string DataProtectorPurpose = "ApesDb.AuthTicket";
    private static readonly TimeSpan Expiration = TimeSpan.FromDays(7);

    private readonly IFusionCache _cache;
    private readonly IDataProtector _protector;

    public RedisTicketStore(IFusionCache cache, IDataProtectionProvider dataProtectionProvider)
    {
        _cache = cache;
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

        await _cache.SetAsync(
            GetCacheKey(key),
            protectedTicket,
            options => options.SetDuration(Expiration)
        );
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var cacheKey = GetCacheKey(key);
        var protectedTicket = await _cache.GetOrDefaultAsync<byte[]>(cacheKey);

        if (protectedTicket is null)
        {
            return null;
        }

        await _cache.SetAsync(
            cacheKey,
            protectedTicket,
            options => options.SetDuration(Expiration)
        );

        var serialized = _protector.Unprotect(protectedTicket);
        return TicketSerializer.Default.Deserialize(serialized);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(GetCacheKey(key));
    }

    private static string GetCacheKey(string key) => $"auth:ticket:{key}";
}
