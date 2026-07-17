namespace ApesDb.Api.Features.Teams.Invites.RespondToTeamInvite;

public sealed class RespondToTeamInviteRequest
{
    public Guid InviteId { get; init; }

    public bool Accept { get; init; }
}
