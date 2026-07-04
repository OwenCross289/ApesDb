using System.Security.Claims;
using ApesDb.Domain.Entities;

namespace ApesDb.Api.Services;

public interface IUserService
{
    Task<User> EnsureUserAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    );
}
