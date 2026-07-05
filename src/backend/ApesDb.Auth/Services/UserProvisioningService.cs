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

    public UserProvisioningService(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
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
        var now = _dateTimeProvider.UtcNow;

        var upserted = await _dbContext.Users.Upsert(
                new User
                {
                    Auth0Subject = subject,
                    Email = email,
                    Name = name,
                    CreatedAt = now,
                    UpdatedAt = now,
                }
            )
            .On(u => u.Auth0Subject)
            .WhenMatched((existing, inserted) => new User
            {
                Id = existing.Id,
                Auth0Subject = existing.Auth0Subject,
                Email = inserted.Email,
                Name = inserted.Name,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = inserted.UpdatedAt,
            })
            .RunAndReturnAsync(cancellationToken);

        return new ProvisionedUser(upserted.First().Id);
    }
}
