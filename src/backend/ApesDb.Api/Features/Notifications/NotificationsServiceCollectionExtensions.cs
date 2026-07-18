using ApesDb.Api.Features.Notifications.NotificationsStream;

namespace ApesDb.Api.Features.Notifications;

public static class NotificationsServiceCollectionExtensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSingleton<NotificationStreamService>();

        return services;
    }
}
