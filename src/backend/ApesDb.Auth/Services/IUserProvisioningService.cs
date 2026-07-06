using System.Security.Claims;

namespace ApesDb.Auth.Services;

public sealed record ProvisionedUser(Guid Id);

public interface IUserProvisioningService
{
    Task<ProvisionedUser> EnsureUserFromPrincipalAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    );
}
