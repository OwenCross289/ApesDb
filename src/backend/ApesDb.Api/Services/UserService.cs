using System.Security.Claims;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Services;

public sealed class UserService(ApplicationDbContext dbContext) : IUserService
{
    public async Task<User> EnsureUserAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    )
    {
        var subject =
            principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("Missing subject claim.");
        var email = principal.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var name = principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        var user = await dbContext.Users.FirstOrDefaultAsync(
            u => u.Auth0Subject == subject,
            cancellationToken
        );

        if (user is null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                Auth0Subject = subject,
                Email = email,
                Name = name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            dbContext.Users.Add(user);
        }
        else
        {
            user.Email = email;
            user.Name = name;
            user.UpdatedAt = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }
}
