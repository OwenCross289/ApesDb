using ApesDb.Common;
using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Notifications;

public sealed class ReadNotificationsEndpoint : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly NotificationStreamService _streamService;

    public ReadNotificationsEndpoint(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        NotificationStreamService streamService
    )
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _streamService = streamService;
    }

    public override void Configure()
    {
        Post(ApiRoutes.Notifications.Read);
        Summary(summary => summary.Summary = "Marks all active notifications as read.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var now = _dateTimeProvider.UtcNow;
        await _dbContext
            .Notifications.Where(notification =>
                notification.UserId == userId && notification.ResolvedAt == null && notification.ReadAt == null
            )
            .ExecuteUpdateAsync(setters => setters.SetProperty(notification => notification.ReadAt, now), ct);
        _streamService.Publish(
            userId,
            new NotificationStreamEvent(NotificationStreamEventKinds.Read, new NotificationReadEventData(now))
        );
        await Send.NoContentAsync(ct);
    }
}
