using System.Security.Claims;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using EFCore.BulkExtensions;
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

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Auth0Subject = subject,
            Email = email,
            Name = name,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var bulkConfig = new BulkConfig
        {
            UpdateByProperties = [nameof(User.Auth0Subject)],
            PropertiesToIncludeOnUpdate = [nameof(User.Email), nameof(User.Name), nameof(User.UpdatedAt)],
        };

        await _dbContext.BulkInsertOrUpdateAsync([user], bulkConfig, cancellationToken: cancellationToken);

        var userId = await _dbContext
            .Users.Where(u => u.Auth0Subject == subject)
            .Select(u => u.Id)
            .FirstAsync(cancellationToken);

        await EnsureSoloTeamAsync(userId, name, now, cancellationToken);

        return new ProvisionedUser(userId);
    }

    private async Task EnsureSoloTeamAsync(
        Guid userId,
        string userName,
        DateTime now,
        CancellationToken cancellationToken
    )
    {
        var teamName = BuildSoloTeamName(userName);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
            INSERT INTO "Teams" ("Id", "OwnerUserId", "Name", "ProfilePicture", "Kind", "CreatedAt", "UpdatedAt")
            SELECT {Guid.CreateVersion7()}, {userId}, {teamName}, NULL, 'Solo', {now}, {now}
            WHERE NOT EXISTS (SELECT 1 FROM "Teams" WHERE "OwnerUserId" = {userId} AND "Kind" = 'Solo')
            ON CONFLICT ("OwnerUserId") WHERE "Kind" = 'Solo' DO NOTHING
            """,
            cancellationToken
        );
        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
            INSERT INTO "TeamMemberships" (
                "Id", "TeamId", "UserId", "Status", "InvitedByUserId", "InvitedAt", "AcceptedAt"
            )
            SELECT {Guid.CreateVersion7()}, "Id", {userId}, 1, NULL, {now}, {now}
            FROM "Teams"
            WHERE "OwnerUserId" = {userId} AND "Kind" = 'Solo'
            ON CONFLICT ("TeamId", "UserId") DO NOTHING
            """,
            cancellationToken
        );
        await transaction.CommitAsync(cancellationToken);
    }

    private static string BuildSoloTeamName(string userName)
    {
        var trimmed = userName.Trim();

        if (trimmed.Length == 0)
        {
            return "Solo Team";
        }

        return $"{trimmed}'s Team";
    }
}
