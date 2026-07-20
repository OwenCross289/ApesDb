namespace ApesDb.Api.Features.Profiles.GetProfiles;

public sealed class GetProfilesRequest
{
    public string? Search { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}
