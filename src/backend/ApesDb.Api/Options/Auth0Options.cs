using System.ComponentModel.DataAnnotations;

namespace ApesDb.Api.Options;

public sealed class Auth0Options
{
    public const string SectionName = "Auth0";

    [Required]
    public required string Domain { get; init; }

    [Required]
    public required string ClientId { get; init; }

    [Required]
    public required string ClientSecret { get; init; }

    public string CallbackPath { get; init; } = "/api/auth/callback";

    public string PostLogoutRedirectUri { get; init; } = "/";
}
