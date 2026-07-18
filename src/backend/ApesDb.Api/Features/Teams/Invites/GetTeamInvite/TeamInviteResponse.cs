namespace ApesDb.Api.Features.Teams.Invites.GetTeamInvite;

public sealed record TeamInviteTeamResponse(Guid Id, string Name, TeamProfilePictureResponse? ProfilePicture);

public sealed record TeamInviteResponse(
    Guid Id,
    TeamInviteTeamResponse Team,
    TeamMemberResponse InvitedBy,
    DateTime CreatedAt
);
