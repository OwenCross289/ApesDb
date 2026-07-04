using System.Security.Claims;
using FastEndpoints;

namespace ApesDb.Api.Endpoints.Auth;

public sealed class MeEndpoint : EndpointWithoutRequest<AuthUserResponse>
{
    public override void Configure()
    {
        Get($"{ApiRoutes.Auth.Prefix}/{ApiRoutes.Auth.Me}");
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var userId =
            User.FindFirstValue("ApesDbUserId") ?? throw new InvalidOperationException("Missing user id claim.");
        var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var name = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        return Send.OkAsync(new AuthUserResponse(Guid.Parse(userId), email, name), ct);
    }
}
