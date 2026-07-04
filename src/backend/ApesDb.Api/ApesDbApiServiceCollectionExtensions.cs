using ApesDb.Api.Options;
using Microsoft.AspNetCore.HttpOverrides;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Api;

public static class ApesDbApiServiceCollectionExtensions
{
    public static IServiceCollection AddApesDbCache(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<CacheOptions>()
            .BindConfiguration(CacheOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var cacheOptions = configuration.GetRequiredSection(CacheOptions.SectionName).Get<CacheOptions>()!;
        var redisConfiguration = BuildRedisConfiguration(cacheOptions);

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = redisConfiguration;
        });

        services.AddFusionCacheSystemTextJsonSerializer();
        services.AddFusionCacheStackExchangeRedisBackplane(options =>
        {
            options.ConfigurationOptions = redisConfiguration;
        });

        services.AddFusionCache().TryWithAutoSetup();

        return services;
    }

    public static IServiceCollection AddApesDbForwardedHeaders(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
        });

        return services;
    }

    private static ConfigurationOptions BuildRedisConfiguration(CacheOptions options)
    {
        var configuration = ConfigurationOptions.Parse(options.ConnectionString);
        configuration.Password = options.Password;
        return configuration;
    }
}
