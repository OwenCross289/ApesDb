namespace ApesDb.Api.Features.Teams.ListTeams;

public sealed record TeamResponse(
    Guid Id,
    string Name,
    ApesDb.Api.Features.Teams.TeamProfilePictureResponse? ProfilePicture,
    string Kind
);
