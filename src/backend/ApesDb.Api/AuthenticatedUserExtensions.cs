using System.Security.Claims;

namespace ApesDb.Api;

public static class AuthenticatedUserExtensions
{
    public static Guid GetApesDbUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue("ApesDbUserId");
        if (value is null || !Guid.TryParse(value, out var userId))
        {
            throw new InvalidOperationException("Missing or invalid ApesDb user id claim.");
        }

        return userId;
    }
}
