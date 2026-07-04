using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;

namespace ApesDb.Api.Authentication;

public sealed class RedisTicketStore : ITicketStore
{
    private const string DataProtectorPurpose = "ApesDb.AuthTicket";
    private static readonly TimeSpan Expiration = TimeSpan.FromDays(7);

    private readonly IDistributedCache _cache;
    private readonly IDataProtector _protector;

    public RedisTicketStore(IDistributedCache cache, IDataProtectionProvider dataProtectionProvider)
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
            new DistributedCacheEntryOptions { SlidingExpiration = Expiration }
        );
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var protectedTicket = await _cache.GetAsync(GetCacheKey(key));

        if (protectedTicket is null)
        {
            return null;
        }

        var serialized = _protector.Unprotect(protectedTicket);
        return TicketSerializer.Default.Deserialize(serialized);
    }

    public Task RemoveAsync(string key)
    {
        return _cache.RemoveAsync(GetCacheKey(key));
    }

    private static string GetCacheKey(string key) => $"auth:ticket:{key}";
}
