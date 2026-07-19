using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain.Entities.Teams;

namespace ApesDb.Api.Tests.TestData;

internal static class TeamTestData
{
    public static Team[] Create()
    {
        return
        [
            CreateSoloTeam("01910000-0000-7000-8000-000000002001", TestUsers.Owner, "Owner's Team"),
            CreateSoloTeam("01910000-0000-7000-8000-000000002002", TestUsers.Member, "Member's Team"),
            CreateSoloTeam("01910000-0000-7000-8000-000000002003", TestUsers.Invitee, "Invitee's Team"),
            CreateSoloTeam("01910000-0000-7000-8000-000000002004", TestUsers.Outsider, "Outsider's Team"),
            new()
            {
                Id = BaseTestData.SharedTeamId,
                OwnerUserId = TestUsers.Owner.SeededUserId!.Value,
                Name = "Shared Test Team",
                Kind = TeamKind.Group,
                CreatedAt = TestClock.UtcNow,
                UpdatedAt = TestClock.UtcNow,
            },
        ];
    }

    private static Team CreateSoloTeam(string id, TestUser user, string name)
    {
        return new Team
        {
            Id = Guid.Parse(id),
            OwnerUserId = user.SeededUserId!.Value,
            Name = name,
            Kind = TeamKind.Solo,
            CreatedAt = TestClock.UtcNow,
            UpdatedAt = TestClock.UtcNow,
        };
    }
}
