using System.Security.Claims;
using ApesDb.Common;
using ApesDb.Domain;
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
        var email =
            principal.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException("Missing email claim.");
        var name = principal.FindFirstValue(ClaimTypes.Name) ?? "Unknown Soldier";
        var pictureUrl = principal.FindFirstValue("picture");
        var now = _dateTimeProvider.UtcNow;
        var teamName = BuildSoloTeamName(name);

        var provisionedUserId = await _dbContext
            .Database.SqlQuery<Guid>(
                $"""
                WITH "upserted_user" AS (
                    INSERT INTO "public"."Users" (
                        "Auth0Subject", "Email", "Name", "PictureUrl", "CreatedAt", "UpdatedAt"
                    )
                    VALUES ({subject}, {email}, {name}, {pictureUrl}, {now}, {now})
                    ON CONFLICT ("Auth0Subject") DO UPDATE SET
                        "Email" = EXCLUDED."Email",
                        "Name" = EXCLUDED."Name",
                        "PictureUrl" = EXCLUDED."PictureUrl",
                        "UpdatedAt" = EXCLUDED."UpdatedAt"
                    RETURNING "Id"
                ),
                "inserted_if_not_exists_team" AS (
                    INSERT INTO "public"."Teams" (
                        "OwnerUserId", "Name", "Kind", "CreatedAt", "UpdatedAt"
                    )
                    SELECT "Id", {teamName}, 'Solo', {now}, {now}
                    FROM "upserted_user"
                    ON CONFLICT DO NOTHING
                    RETURNING "Id", "OwnerUserId"
                ),
                "solo_team" AS (
                    SELECT "Id", "OwnerUserId" FROM "inserted_if_not_exists_team"
                    UNION ALL
                    SELECT t."Id", t."OwnerUserId"
                    FROM "public"."Teams" AS t
                    JOIN "upserted_user" AS u ON t."OwnerUserId" = u."Id"
                    WHERE t."Kind" = 'Solo'
                ),
                "inserted_if_not_exists_membership" AS (
                    INSERT INTO "public"."TeamMemberships" (
                        "TeamId", "UserId", "Status", "InvitedAt", "AcceptedAt"
                    )
                    SELECT "Id", "OwnerUserId", 1, {now}, {now}
                    FROM "solo_team"
                    ON CONFLICT DO NOTHING
                    RETURNING "UserId"
                )
                SELECT "Id" AS "Value"
                FROM "upserted_user"
                """
            )
            .AsAsyncEnumerable()
            .SingleAsync(cancellationToken);

        return new ProvisionedUser(provisionedUserId);
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
