using System.Security.Claims;

namespace ApesDb.Api.Tests.Infrastructure.Authentication;

public sealed record TestUser(
    string Key,
    Guid? SeededUserId,
    string Auth0Subject,
    string Email,
    string Name,
    string? PictureUrl
);

public static class TestUsers
{
    public static readonly TestUser Owner = new(
        "owner",
        Guid.Parse("01910000-0000-7000-8000-000000001001"),
        "auth0|apesdb-test-owner",
        "owner@apesdb.test",
        "Olivia Owner",
        "https://images.apesdb.test/owner.png"
    );

    public static readonly TestUser Member = new(
        "member",
        Guid.Parse("01910000-0000-7000-8000-000000001002"),
        "auth0|apesdb-test-member",
        "member@apesdb.test",
        "Mason Member",
        "https://images.apesdb.test/member.png"
    );

    public static readonly TestUser Invitee = new(
        "invitee",
        Guid.Parse("01910000-0000-7000-8000-000000001003"),
        "auth0|apesdb-test-invitee",
        "invitee@apesdb.test",
        "Imani Invitee",
        "https://images.apesdb.test/invitee.png"
    );

    public static readonly TestUser Outsider = new(
        "outsider",
        Guid.Parse("01910000-0000-7000-8000-000000001004"),
        "auth0|apesdb-test-outsider",
        "outsider@apesdb.test",
        "Oscar Outsider",
        null
    );

    public static readonly TestUser SignupCandidate = new(
        "signup-candidate",
        null,
        "auth0|apesdb-test-signup-candidate",
        "signup@apesdb.test",
        "Sasha Signup",
        "https://images.apesdb.test/signup.png"
    );

    public static readonly TestUser NotAllowed = new(
        "not-allowed",
        null,
        "auth0|apesdb-test-not-allowed",
        "not-allowed@apesdb.test",
        "No Access",
        null
    );

    public static IReadOnlyList<TestUser> Existing { get; } = [Owner, Member, Invitee, Outsider];

    public static IReadOnlyList<TestUser> All { get; } =
    [Owner, Member, Invitee, Outsider, SignupCandidate, NotAllowed];

    public static TestUser? Find(string key)
    {
        foreach (var user in All)
        {
            if (string.Equals(user.Key, key, StringComparison.Ordinal))
            {
                return user;
            }
        }

        return null;
    }

    public static ClaimsPrincipal CreateAuth0Principal(TestUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Auth0Subject),
            new("sub", user.Auth0Subject),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name),
        };

        if (user.PictureUrl is not null)
        {
            claims.Add(new Claim("picture", user.PictureUrl));
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Auth0"));
    }
}
