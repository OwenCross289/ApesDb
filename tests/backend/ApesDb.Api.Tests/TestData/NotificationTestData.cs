using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain.Entities.Notifications;
using ApesDb.Domain.Entities.Users;

namespace ApesDb.Api.Tests.TestData;

internal static class NotificationTestData
{
    public static Notification[] Create(IReadOnlyDictionary<Guid, User> usersById)
    {
        return
        [
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000005001"),
                UserId = TestUsers.Invitee.SeededUserId!.Value,
                User = usersById[TestUsers.Invitee.SeededUserId.Value],
                Type = NotificationType.TeamInvite,
                ResourceId = BaseTestData.PendingInviteId,
                IsActionable = true,
                CreatedAt = TestClock.UtcNow,
            },
        ];
    }
}
