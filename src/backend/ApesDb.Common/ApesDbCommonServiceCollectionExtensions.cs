using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ApesDb.Common;

public static class ApesDbCommonServiceCollectionExtensions
{
    public static IServiceCollection AddApesDbCommon(this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
