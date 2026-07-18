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
                "upserted_team" AS (
                    INSERT INTO "public"."Teams" (
                        "OwnerUserId", "Name", "Kind", "CreatedAt", "UpdatedAt"
                    )
                    SELECT "Id", {teamName}, 'Solo', {now}, {now}
                    FROM "upserted_user"
                    ON CONFLICT ("OwnerUserId") WHERE "Kind" = 'Solo' DO UPDATE SET
                        "Name" = EXCLUDED."Name",
                        "UpdatedAt" = EXCLUDED."UpdatedAt"
                    RETURNING "Id", "OwnerUserId"
                ),
                "upserted_membership" AS (
                    INSERT INTO "public"."TeamMemberships" (
                        "TeamId", "UserId", "Status", "InvitedByUserId", "InvitedAt", "AcceptedAt"
                    )
                    SELECT
                        "upserted_team"."Id",
                        "upserted_team"."OwnerUserId",
                        1,
                        NULL,
                        {now},
                        {now}
                    FROM "upserted_team"
                    ON CONFLICT ("TeamId", "UserId") DO UPDATE SET
                        "Status" = 1,
                        "InvitedByUserId" = NULL,
                        "AcceptedAt" = COALESCE(
                            "TeamMemberships"."AcceptedAt",
                            EXCLUDED."AcceptedAt"
                        )
                    RETURNING "UserId"
                )
                SELECT "UserId" AS "Value"
                FROM "upserted_membership"
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
