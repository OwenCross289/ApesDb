using System.Security.Claims;
using System.Threading.Tasks;
using ApesDb.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace ApesDb.Api.UnitTests.Authentication;

public sealed class RedisTicketStoreTests
{
    private static ITicketStore CreateStore()
    {
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider());
        services.AddSingleton<ITicketStore, RedisTicketStore>();
        return services.BuildServiceProvider().GetRequiredService<ITicketStore>();
    }

    private static AuthenticationTicket CreateTicket()
    {
        var identity = new ClaimsIdentity(
            new[] { new Claim(ClaimTypes.NameIdentifier, "test-sub") },
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return new AuthenticationTicket(
            new ClaimsPrincipal(identity),
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }

    [Fact]
    public async Task StoreAndRetrieveRoundTrip()
    {
        var store = CreateStore();
        var ticket = CreateTicket();

        var key = await store.StoreAsync(ticket, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrWhiteSpace(key));

        var retrieved = await store.RetrieveAsync(key, TestContext.Current.CancellationToken);

        Assert.NotNull(retrieved);
        Assert.Equal("test-sub", retrieved.Principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    [Fact]
    public async Task RemoveDeletesTicket()
    {
        var store = CreateStore();
        var ticket = CreateTicket();

        var key = await store.StoreAsync(ticket, TestContext.Current.CancellationToken);
        await store.RemoveAsync(key, TestContext.Current.CancellationToken);

        var retrieved = await store.RetrieveAsync(key, TestContext.Current.CancellationToken);

        Assert.Null(retrieved);
    }

    [Fact]
    public async Task RenewUpdatesTicket()
    {
        var store = CreateStore();
        var ticket = CreateTicket();

        var key = await store.StoreAsync(ticket, TestContext.Current.CancellationToken);
        var newIdentity = new ClaimsIdentity(
            new[] { new Claim(ClaimTypes.NameIdentifier, "updated-sub") },
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var newTicket = new AuthenticationTicket(
            new ClaimsPrincipal(newIdentity),
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        await store.RenewAsync(key, newTicket, TestContext.Current.CancellationToken);

        var retrieved = await store.RetrieveAsync(key, TestContext.Current.CancellationToken);

        Assert.NotNull(retrieved);
        Assert.Equal("updated-sub", retrieved.Principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
