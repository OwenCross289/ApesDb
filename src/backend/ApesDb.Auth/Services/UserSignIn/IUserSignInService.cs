using System.Security.Claims;
using ApesDb.Auth.Services.UserProvisioning;
using OneOf;

namespace ApesDb.Auth.Services.UserSignIn;

public interface IUserSignInService
{
    Task<OneOf<NotAllowed, ProvisionedUser>> TrySignInAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    );
}
