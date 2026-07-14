using ApesDb.Auth.Options;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ApesDb.Api.Features.Auth.Login;

public sealed class LoginEndpoint : EndpointWithoutRequest
{
    private readonly IOptions<Auth0Options> _options;

    public LoginEndpoint(IOptions<Auth0Options> options)
    {
        _options = options;
    }

    public override void Configure()
    {
        Get($"{ApiRoutes.Auth.Prefix}/{ApiRoutes.Auth.Login}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var returnUrl = HttpContext.Request.Query["returnUrl"].ToString();

        if (string.IsNullOrWhiteSpace(returnUrl) || !IsLocalUrl(returnUrl))
        {
            returnUrl = "/";
        }

        var connection = ResolveConnection(HttpContext.Request.Query["connection"].ToString());
        var properties = new AuthenticationProperties { RedirectUri = returnUrl };

        if (!string.IsNullOrWhiteSpace(connection))
        {
            properties.Items["connection"] = connection;
        }

        await HttpContext.ChallengeAsync("Auth0", properties);
        await HttpContext.Response.StartAsync(ct);
    }

    private string? ResolveConnection(string? connection)
    {
        if (string.Equals(connection, "google", StringComparison.OrdinalIgnoreCase))
        {
            return _options.Value.GoogleConnectionName;
        }

        return null;
    }

    private static bool IsLocalUrl(string url)
    {
        return !string.IsNullOrEmpty(url) && (url[0] == '/' && (url.Length == 1 || url[1] != '/' && url[1] != '\\'))
            || (url.Length > 1 && url[0] == '~' && url[1] == '/');
    }
}
