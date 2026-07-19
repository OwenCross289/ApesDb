using System.Security.Claims;

namespace ApesDb.Auth.Services.UserProvisioning;

public interface IUserProvisioningService
{
    Task<ProvisionedUser> EnsureUserFromPrincipalAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    );
}
