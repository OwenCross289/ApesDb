using ApesDb.Shared.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ApesDb.Shared;

public static class ApesDbSharedServiceCollectionExtensions
{
    public static IServiceCollection AddApesDbShared(this IServiceCollection services)
    {
        services.AddScoped<IAllowedUserService, AllowedUserService>();

        return services;
    }
}
