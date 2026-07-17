using Microsoft.AspNetCore.Http;

namespace ApesDb.Api.Features.Teams.CreateTeam;

public sealed class CreateTeamRequest
{
    public string Name { get; init; } = string.Empty;

    public IFormFile? ProfilePicture { get; init; }
}
