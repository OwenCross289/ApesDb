using System.Security.Claims;
using FastEndpoints;

namespace ApesDb.Api.Features.Auth.Me;

public sealed class MeEndpoint : EndpointWithoutRequest<AuthUserResponse>
{
    public override void Configure()
    {
        Get($"{ApiRoutes.Auth.Prefix}/{ApiRoutes.Auth.Me}");
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var name = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        var pictureUrl = User.FindFirstValue("picture");

        return Send.OkAsync(new AuthUserResponse(userId, email, name, pictureUrl), ct);
    }
}
