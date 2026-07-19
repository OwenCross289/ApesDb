using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain.Entities.Users;

namespace ApesDb.Api.Tests.TestData;

internal static class UserTestData
{
    public static User[] Create()
    {
        return TestUsers
            .Existing.Select(user => new User
            {
                Id = user.SeededUserId!.Value,
                Auth0Subject = user.Auth0Subject,
                Email = user.Email,
                Name = user.Name,
                PictureUrl = user.PictureUrl,
                CreatedAt = TestClock.UtcNow,
                UpdatedAt = TestClock.UtcNow,
            })
            .ToArray();
    }
}
