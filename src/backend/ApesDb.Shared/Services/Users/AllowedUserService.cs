using System.Net.Mail;
using ApesDb.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Shared.Services.Users;

public sealed class AllowedUserService : IAllowedUserService
{
    private const int MaximumEmailLength = 256;

    private readonly ApplicationDbContext _dbContext;

    public AllowedUserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsAllowedAsync(string? email, CancellationToken cancellationToken = default)
    {
        if (!TryNormalize(email, out var normalizedEmail))
        {
            return Task.FromResult(false);
        }

        return _dbContext.AllowedUsers.AnyAsync(value => value.Email == normalizedEmail, cancellationToken);
    }

    public async Task<bool> AddAsync(string email, CancellationToken cancellationToken = default)
    {
        if (!TryNormalize(email, out var normalizedEmail))
        {
            throw new ArgumentException("A valid email address is required.", nameof(email));
        }

        var affectedRows = await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
            INSERT INTO "public"."AllowedUsers" ("Email")
            VALUES ({normalizedEmail})
            ON CONFLICT ("Email") DO NOTHING
            """,
            cancellationToken
        );

        return affectedRows == 1;
    }

    private static bool TryNormalize(string? email, out string normalizedEmail)
    {
        normalizedEmail = string.Empty;

        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var trimmedEmail = email.Trim();
        if (trimmedEmail.Length > MaximumEmailLength)
        {
            return false;
        }

        if (!MailAddress.TryCreate(trimmedEmail, out var parsedEmail))
        {
            return false;
        }

        if (!string.Equals(parsedEmail.Address, trimmedEmail, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        normalizedEmail = trimmedEmail.ToLowerInvariant();
        return true;
    }
}
