using ApesDb.Auth.Options;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace ApesDb.Api.Endpoints.Auth;

public sealed class LogoutEndpoint : EndpointWithoutRequest<LogoutResponse>
{
    private readonly IOptions<Auth0Options> _options;

    public LogoutEndpoint(IOptions<Auth0Options> options)
    {
        _options = options;
    }

    public override void Configure()
    {
        Post($"{ApiRoutes.Auth.Prefix}/{ApiRoutes.Auth.Logout}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var auth0 = _options.Value;
        var request = HttpContext.Request;
        var returnTo = $"{request.Scheme}://{request.Host}{request.PathBase}{auth0.PostLogoutRedirectUri}";

        var logoutUrl =
            $"https://{auth0.Domain}/v2/logout?client_id={Uri.EscapeDataString(auth0.ClientId)}"
            + $"&returnTo={Uri.EscapeDataString(returnTo)}";

        await Send.OkAsync(new LogoutResponse(logoutUrl), ct);
    }
}
