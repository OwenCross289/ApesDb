namespace ApesDb.Api.Endpoints.Auth;

public sealed record AuthUserResponse(Guid Id, string Email, string Name);
