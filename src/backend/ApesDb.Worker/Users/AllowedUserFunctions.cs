namespace ApesDb.Worker.Users;

internal static class AllowedUserFunctions
{
    public const string Add = "add-allowed-user";
}

public sealed record AddAllowedUserRequest(string Email);
