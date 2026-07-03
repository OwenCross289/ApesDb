namespace ApesDb.Igdb.Sdk;

public sealed class IgdbOptions
{
    public const string SectionName = "Igdb";

    public string ClientId { get; init; } = string.Empty;

    public string ClientSecret { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = "https://api.igdb.com/v4";

    public string TokenUrl { get; init; } = "https://id.twitch.tv/oauth2/token";
}
