using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Domain.Entities.Users;

namespace ApesDb.Api.Tests.TestData;

internal static class ProfileTestData
{
    public static Profile[] Create(IReadOnlyDictionary<Guid, User> usersById)
    {
        return
        [
            CreatePublic(TestUsers.Owner, usersById, "Building a calmer home for our game nights."),
            CreatePublic(TestUsers.Member, usersById, "Co-op strategist and achievement hunter."),
            CreatePublic(TestUsers.Invitee, usersById, null),
            new Profile
            {
                UserId = TestUsers.Outsider.SeededUserId!.Value,
                User = usersById[TestUsers.Outsider.SeededUserId.Value],
                AboutMe = "This description must remain private.",
                IsPublic = false,
            },
        ];
    }

    private static Profile CreatePublic(TestUser testUser, IReadOnlyDictionary<Guid, User> usersById, string? aboutMe)
    {
        return new Profile
        {
            UserId = testUser.SeededUserId!.Value,
            User = usersById[testUser.SeededUserId.Value],
            AboutMe = aboutMe,
            IsPublic = true,
        };
    }
}
