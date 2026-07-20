namespace ApesDb.Api.Features.Profiles;

public sealed record ProfileResponse(Guid Id, string Name, string? PictureUrl, string? AboutMe, bool IsPublic);
