using System.Security.Claims;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        var pictureUrl = principal.FindFirstValue("picture");
        var now = _dateTimeProvider.UtcNow;

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Auth0Subject = subject,
            Email = email,
            Name = name,
            PictureUrl = pictureUrl,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var bulkConfig = new BulkConfig
        {
            UpdateByProperties = [nameof(User.Auth0Subject)],
            PropertiesToIncludeOnUpdate =
            [
                nameof(User.Email),
                nameof(User.Name),
                nameof(User.PictureUrl),
                nameof(User.UpdatedAt),
            ],
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

        var team = await _dbContext.Teams.SingleOrDefaultAsync(
            existingTeam => existingTeam.OwnerUserId == userId && existingTeam.Kind == TeamKind.Solo,
            cancellationToken
        );

        if (team is null)
        {
            team = new Team
            {
                Id = Guid.CreateVersion7(),
                OwnerUserId = userId,
                Name = teamName,
                Kind = TeamKind.Solo,
                CreatedAt = now,
                UpdatedAt = now,
            };
            _dbContext.Teams.Add(team);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception) when (IsUniqueViolation(exception))
            {
                // A concurrent login provisioned the solo team first.
                _dbContext.ChangeTracker.Clear();
                team = await _dbContext.Teams.SingleAsync(
                    existingTeam => existingTeam.OwnerUserId == userId && existingTeam.Kind == TeamKind.Solo,
                    cancellationToken
                );
            }
        }

        var membershipExists = await _dbContext.TeamMemberships.AnyAsync(
            membership => membership.TeamId == team.Id && membership.UserId == userId,
            cancellationToken
        );

        if (membershipExists)
        {
            return;
        }

        _dbContext.TeamMemberships.Add(
            new TeamMembership
            {
                Id = Guid.CreateVersion7(),
                TeamId = team.Id,
                UserId = userId,
                Status = TeamMembershipStatus.Accepted,
                InvitedAt = now,
                AcceptedAt = now,
            }
        );

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception) when (IsUniqueViolation(exception))
        {
            // A concurrent login provisioned the membership first.
            _dbContext.ChangeTracker.Clear();
        }
    }

    private static bool IsUniqueViolation(DbUpdateException exception)
    {
        return exception.InnerException is PostgresException postgresException
            && postgresException.SqlState == PostgresErrorCodes.UniqueViolation;
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
