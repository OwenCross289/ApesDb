using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain.Entities.Teams;
using ApesDb.Domain.Entities.Users;

namespace ApesDb.Api.Tests.TestData;

internal static class TeamMembershipTestData
{
    public static TeamMembership[] Create(
        IReadOnlyList<Team> teams,
        IReadOnlyDictionary<Guid, Team> teamsById,
        IReadOnlyDictionary<Guid, User> usersById
    )
    {
        return
        [
            CreateAcceptedMembership("01910000-0000-7000-8000-000000003001", teams[0], TestUsers.Owner, usersById),
            CreateAcceptedMembership("01910000-0000-7000-8000-000000003002", teams[1], TestUsers.Member, usersById),
            CreateAcceptedMembership("01910000-0000-7000-8000-000000003003", teams[2], TestUsers.Invitee, usersById),
            CreateAcceptedMembership("01910000-0000-7000-8000-000000003004", teams[3], TestUsers.Outsider, usersById),
            CreateAcceptedMembership(
                "01910000-0000-7000-8000-000000003005",
                teamsById[BaseTestData.SharedTeamId],
                TestUsers.Owner,
                usersById
            ),
            CreateAcceptedMembership(
                "01910000-0000-7000-8000-000000003006",
                teamsById[BaseTestData.SharedTeamId],
                TestUsers.Member,
                usersById
            ),
            new()
            {
                Id = BaseTestData.PendingInviteId,
                TeamId = BaseTestData.SharedTeamId,
                Team = teamsById[BaseTestData.SharedTeamId],
                UserId = TestUsers.Invitee.SeededUserId!.Value,
                User = usersById[TestUsers.Invitee.SeededUserId.Value],
                Status = TeamMembershipStatus.Invited,
                InvitedByUserId = TestUsers.Owner.SeededUserId,
                InvitedByUser = usersById[TestUsers.Owner.SeededUserId!.Value],
                InvitedAt = TestClock.UtcNow,
            },
        ];
    }

    private static TeamMembership CreateAcceptedMembership(
        string id,
        Team team,
        TestUser user,
        IReadOnlyDictionary<Guid, User> usersById
    )
    {
        return new TeamMembership
        {
            Id = Guid.Parse(id),
            TeamId = team.Id,
            Team = team,
            UserId = user.SeededUserId!.Value,
            User = usersById[user.SeededUserId.Value],
            Status = TeamMembershipStatus.Accepted,
            InvitedAt = TestClock.UtcNow,
            AcceptedAt = TestClock.UtcNow,
        };
    }
}
