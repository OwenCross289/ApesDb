namespace ApesDb.Api.Features.Teams.ListTeams;

public sealed record TeamResponse(Guid Id, string Name, string? ProfilePictureUrl, string Kind);
