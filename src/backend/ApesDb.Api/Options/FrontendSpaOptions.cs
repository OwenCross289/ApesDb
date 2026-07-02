namespace ApesDb.Api.Options;

public sealed class FrontendSpaOptions
{
    public const string SectionName = "Spa";

    public required string DevServerUrl { get; init; }
}
