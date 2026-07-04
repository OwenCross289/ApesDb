using System.Security.Claims;
using ApesDb.Api.Data.Entities;

namespace ApesDb.Api.Services;

public interface IUserService
{
    Task<User> EnsureUserAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    );
}
