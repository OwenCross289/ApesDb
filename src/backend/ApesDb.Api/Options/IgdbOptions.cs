namespace ApesDb.Api.Options;

public sealed class IgdbOptions
{
    public const string SectionName = "Igdb";

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }
}
