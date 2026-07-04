using FastEndpoints;
using Microsoft.AspNetCore.Authentication;

namespace ApesDb.Api.Endpoints.Auth;

public sealed class LoginEndpoint : EndpointWithoutRequest
{
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

        var properties = new AuthenticationProperties { RedirectUri = returnUrl };
        await HttpContext.ChallengeAsync("Auth0", properties);
        await HttpContext.Response.StartAsync(ct);
    }

    private static bool IsLocalUrl(string url)
    {
        return !string.IsNullOrEmpty(url) && (url[0] == '/' && (url.Length == 1 || url[1] != '/' && url[1] != '\\'))
            || (url.Length > 1 && url[0] == '~' && url[1] == '/');
    }
}
