namespace ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;

public sealed class CreateTeamInviteRequest
{
    public Guid TeamId { get; init; }

    public string Email { get; init; } = string.Empty;
}
