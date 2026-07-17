namespace ApesDb.Api.Features.Notifications;

public sealed record NotificationResponse(
    Guid Id,
    string Type,
    Guid ResourceId,
    DateTime CreatedAt,
    DateTime? ReadAt,
    bool IsUnread,
    bool IsActionable
);

public sealed record NotificationMetadataResponse(
    int TotalCount,
    int UnreadCount,
    int ActionableCount,
    int AttentionCount
);

public sealed record ListNotificationsResponse(NotificationResponse[] Items, NotificationMetadataResponse Metadata);
