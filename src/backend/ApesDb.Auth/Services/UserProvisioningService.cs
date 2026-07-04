using System.Security.Claims;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Auth.Services;

public sealed class UserProvisioningService : IUserProvisioningService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserProvisioningService(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ProvisionedUser> EnsureUserFromPrincipalAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default
    )
    {
        var subject =
            principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("Missing subject claim.");
        var email = principal.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var name = principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Auth0Subject == subject, cancellationToken);

        if (user is null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                Auth0Subject = subject,
                Email = email,
                Name = name,
                CreatedAt = _dateTimeProvider.UtcNow,
                UpdatedAt = _dateTimeProvider.UtcNow,
            };
            _dbContext.Users.Add(user);
        }
        else
        {
            user.Email = email;
            user.Name = name;
            user.UpdatedAt = _dateTimeProvider.UtcNow;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ProvisionedUser(user.Id);
    }
}
