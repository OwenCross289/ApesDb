namespace ApesDb.Api;

public static class ApiRoutes
{
    public static class Api
    {
        public const string Prefix = "api";
    }

    public static class Ping
    {
        public const string Path = "ping";
    }

    public static class Games
    {
        public const string Top = "games/top";
    }

    public static class Auth
    {
        public const string Prefix = "auth";
        public const string Login = "login";
        public const string Logout = "logout";
        public const string Me = "me";
    }
}
