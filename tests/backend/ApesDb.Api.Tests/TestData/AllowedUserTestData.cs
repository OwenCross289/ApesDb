using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain.Entities.Users;

namespace ApesDb.Api.Tests.TestData;

internal static class AllowedUserTestData
{
    public static AllowedUser[] Create()
    {
        return
        [
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000004001"),
                Email = TestUsers.Owner.Email,
                CreatedAt = TestClock.UtcNow,
            },
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000004002"),
                Email = TestUsers.Member.Email,
                CreatedAt = TestClock.UtcNow,
            },
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000004003"),
                Email = TestUsers.Invitee.Email,
                CreatedAt = TestClock.UtcNow,
            },
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000004004"),
                Email = TestUsers.Outsider.Email,
                CreatedAt = TestClock.UtcNow,
            },
            new()
            {
                Id = Guid.Parse("01910000-0000-7000-8000-000000004005"),
                Email = TestUsers.SignupCandidate.Email,
                CreatedAt = TestClock.UtcNow,
            },
        ];
    }
}
