using ApesDb.Api.Features.Profiles;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Common;
using Microsoft.AspNetCore.WebUtilities;

namespace ApesDb.Api.Tests.Features.Profiles.GetProfiles;

public sealed class GetProfilesTests
{
    private const string ProfilesEndpoint = "/api/profiles";

    private readonly SharedGetApiFactory _factory;

    public GetProfilesTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCanGetAlphabetizedPublicProfiles()
    {
        await Verify(await GetProfilesAsync());
    }

    [Fact]
    public async Task SearchesPublicProfilesByName()
    {
        await Verify(await GetProfilesAsync(("Search", "  oWnEr  ")));
    }

    [Fact]
    public async Task PaginatesPublicProfiles()
    {
        await Verify(await GetProfilesAsync(("Page", "2"), ("PageSize", "1")));
    }

    [Fact]
    public async Task RejectsInvalidPagination()
    {
        var requestUri = QueryHelpers.AddQueryString(
            ProfilesEndpoint,
            new Dictionary<string, string?> { ["Page"] = "0", ["PageSize"] = "101" }
        );
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<ValidationProblemResponse>(response));
    }

    private async Task<HttpResponseSnapshot> GetProfilesAsync(params (string Name, string? Value)[] queryParameters)
    {
        var parameters = queryParameters.Select(parameter => new KeyValuePair<string, string?>(
            parameter.Name,
            parameter.Value
        ));
        var requestUri = QueryHelpers.AddQueryString(ProfilesEndpoint, parameters);

        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync(requestUri, TestContext.Current.CancellationToken);

        return await HttpResponseSnapshot.CreateAsync<Pagable<ProfileResponse>>(response);
    }

    private sealed record ValidationProblemResponse(
        string? Type,
        string? Title,
        int? Status,
        IReadOnlyDictionary<string, string[]>? Errors
    );
}
