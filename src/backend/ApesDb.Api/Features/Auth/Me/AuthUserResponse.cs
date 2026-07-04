namespace ApesDb.Api.Features.Auth.Me;

public sealed record AuthUserResponse(Guid Id, string Email, string Name);
