using System.Security.Claims;
using ApesDb.Auth.Services.UserProvisioning;
using ApesDb.Shared.Services.Users;
using OneOf;

namespace ApesDb.Auth.Services.UserSignIn;

public sealed class UserSignInService : IUserSignInService
{
    private readonly IAllowedUserService _allowedUserService;
    private readonly IUserProvisioningService _userProvisioningService;

    public UserSignInService(IAllowedUserService allowedUserService, IUserProvisioningService userProvisioningService)
    {
        _allowedUserService = allowedUserService;
        _userProvisioningService = userProvisioningService;
    }

    public async Task<OneOf<NotAllowed, ProvisionedUser>> TrySignInAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    )
    {
        var email = principal.FindFirstValue(ClaimTypes.Email);
        if (!await _allowedUserService.IsAllowedAsync(email, cancellationToken))
        {
            return new NotAllowed();
        }

        return await _userProvisioningService.EnsureUserFromPrincipalAsync(principal, cancellationToken);
    }
}
