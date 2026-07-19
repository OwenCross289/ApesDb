using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using ApesDb.Api.Features.Teams;
using ApesDb.Api.Features.Teams.CreateTeam;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using SkiaSharp;
using TeamListResponse = ApesDb.Api.Features.Teams.GetTeams.TeamResponse;

namespace ApesDb.Api.Tests.Features.Teams.CreateTeam;

public sealed class CreateTeamTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
    private const int MaximumProfilePictureLength = 5 * 1024 * 1024;
    private const string TeamsEndpoint = "/api/teams";

    private static readonly byte[] GifImage = Convert.FromBase64String(
        "R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw=="
    );

    private readonly MutableEndpointApiFactory _factory;

    public CreateTeamTests(MutableEndpointApiFactory factory)
    {
        _factory = factory;
    }

    public async ValueTask InitializeAsync()
    {
        await _factory.ResetAsync(TestContext.Current.CancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    [Fact]
    public async Task ExistingUserCanCreateTeam()
    {
        using var content = CreateRequest("  New Test Team  ");

        await Verify(await CreateTeamAsync(content));
    }

    [Fact]
    public async Task ExistingUserCanCreateTeamWithProfilePicture()
    {
        using var content = CreateRequest("Picture Team", CreatePngImage(), "profile.png", "image/png");

        await Verify(await CreateTeamAsync(content));
    }

    [Fact]
    public async Task AnonymousUserCannotCreateTeam()
    {
        using var content = CreateRequest("Anonymous Team");

        await Verify(await RejectCreateTeamAsync(content, null));
    }

    [Fact]
    public async Task CannotCreateTeamWithoutName()
    {
        using var content = CreateRequest(string.Empty);

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    [Fact]
    public async Task CannotCreateTeamWithWhitespaceName()
    {
        using var content = CreateRequest("   ");

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    [Fact]
    public async Task CannotCreateTeamWithOverlongName()
    {
        using var content = CreateRequest(new string('a', 129));

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    [Fact]
    public async Task CannotCreateTeamWithOversizedProfilePicture()
    {
        var oversizedProfilePicture = new byte[MaximumProfilePictureLength + 1];
        using var content = CreateRequest(
            "Oversized Picture Team",
            oversizedProfilePicture,
            "profile.png",
            "image/png"
        );

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    [Fact]
    public async Task CannotCreateTeamWithInvalidProfilePicture()
    {
        var invalidProfilePicture = Encoding.UTF8.GetBytes("not an image");
        using var content = CreateRequest("Invalid Picture Team", invalidProfilePicture, "profile.png", "image/png");

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    [Fact]
    public async Task CannotCreateTeamWithUnsupportedProfilePicture()
    {
        using var content = CreateRequest("Unsupported Picture Team", GifImage, "profile.gif", "image/gif");

        await Verify(await RejectCreateTeamAsync(content, TestUsers.Owner));
    }

    private async Task<CreatedTeamSnapshot> CreateTeamAsync(MultipartFormDataContent requestContent)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var response = await client.PostAsync(
            TeamsEndpoint,
            requestContent,
            TestContext.Current.CancellationToken
        );
        var createHttp = await HttpResponseSnapshot.CreateAsync<TeamResponse>(response);
        var createResponse = CreateTeamHttpSnapshot(createHttp);

        TeamHttpSnapshot? getResponse = null;
        if (createHttp.Content is TeamResponse createdTeam)
        {
            using var getHttpResponse = await client.GetAsync(
                $"{TeamsEndpoint}/{createdTeam.Id}",
                TestContext.Current.CancellationToken
            );
            var getHttp = await HttpResponseSnapshot.CreateAsync<TeamResponse>(getHttpResponse);
            getResponse = CreateTeamHttpSnapshot(getHttp);
        }

        return new CreatedTeamSnapshot(createResponse, getResponse);
    }

    private async Task<RejectedCreateTeamSnapshot> RejectCreateTeamAsync(
        MultipartFormDataContent requestContent,
        TestUser? user
    )
    {
        var before = await GetTeamsAsync();
        ApiTestClient client;
        if (user is null)
        {
            client = ApiTestClient.CreateAnonymous(_factory);
        }
        else
        {
            client = ApiTestClient.CreateAuthenticated(_factory, user);
        }

        using (client)
        using (
            var response = await client.PostAsync(TeamsEndpoint, requestContent, TestContext.Current.CancellationToken)
        )
        {
            var mutation = await HttpResponseSnapshot.CreateAsync<ValidationProblemResponse>(response);
            var after = await GetTeamsAsync();
            return new RejectedCreateTeamSnapshot(mutation, before, after);
        }
    }

    private async Task<TeamListHttpSnapshot> GetTeamsAsync()
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var response = await client.GetAsync(TeamsEndpoint, TestContext.Current.CancellationToken);
        var http = await HttpResponseSnapshot.CreateAsync<TeamListResponse[]>(response);
        TeamListItemSnapshot[]? content = null;
        if (http.Content is TeamListResponse[] teams)
        {
            content = teams.Select(CreateTeamListItemSnapshot).ToArray();
        }

        return new TeamListHttpSnapshot(http.Response, content);
    }

    private static MultipartFormDataContent CreateRequest(
        string name,
        byte[]? profilePicture = null,
        string? fileName = null,
        string? contentType = null
    )
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(name), nameof(CreateTeamRequest.Name));
        if (profilePicture is null)
        {
            return content;
        }

        var pictureContent = new ByteArrayContent(profilePicture);
        if (contentType is not null)
        {
            pictureContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        }

        var resolvedFileName = fileName;
        if (resolvedFileName is null)
        {
            resolvedFileName = "profile-picture";
        }

        content.Add(pictureContent, nameof(CreateTeamRequest.ProfilePicture), resolvedFileName);
        return content;
    }

    private static TeamHttpSnapshot CreateTeamHttpSnapshot(HttpResponseSnapshot http)
    {
        TeamSnapshot? content = null;
        if (http.Content is TeamResponse team)
        {
            content = new TeamSnapshot(
                team.Id,
                team.Name,
                team.Kind,
                team.CreatedAt,
                CreateProfilePictureSnapshot(team.ProfilePicture),
                team.Members
            );
        }

        return new TeamHttpSnapshot(http.Response, content);
    }

    private static TeamListItemSnapshot CreateTeamListItemSnapshot(TeamListResponse team)
    {
        return new TeamListItemSnapshot(
            team.Id,
            team.Name,
            team.Kind,
            CreateProfilePictureSnapshot(team.ProfilePicture)
        );
    }

    private static ProfilePictureSnapshot? CreateProfilePictureSnapshot(TeamProfilePictureResponse? response)
    {
        if (response is null)
        {
            return null;
        }

        string? format = null;
        int? width = null;
        int? height = null;
        using var stream = new MemoryStream(response.Data);
        using var codec = SKCodec.Create(stream);
        if (codec is not null)
        {
            format = codec.EncodedFormat.ToString();
            width = codec.Info.Width;
            height = codec.Info.Height;
        }

        return new ProfilePictureSnapshot(
            response.ContentType,
            response.Data.Length,
            Convert.ToHexString(SHA256.HashData(response.Data)),
            format,
            width,
            height
        );
    }

    private static byte[] CreatePngImage()
    {
        using var bitmap = new SKBitmap(320, 180);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.CornflowerBlue);
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }

    private sealed record CreatedTeamSnapshot(TeamHttpSnapshot CreateResponse, TeamHttpSnapshot? GetResponse);

    private sealed record RejectedCreateTeamSnapshot(
        HttpResponseSnapshot MutationResponse,
        TeamListHttpSnapshot TeamsBefore,
        TeamListHttpSnapshot TeamsAfter
    );

    private sealed record TeamHttpSnapshot(HttpResponseDetails Response, TeamSnapshot? Content);

    private sealed record TeamListHttpSnapshot(HttpResponseDetails Response, TeamListItemSnapshot[]? Content);

    private sealed record TeamSnapshot(
        Guid Id,
        string Name,
        string Kind,
        DateTime CreatedAt,
        ProfilePictureSnapshot? ProfilePicture,
        TeamMemberResponse[] Members
    );

    private sealed record TeamListItemSnapshot(
        Guid Id,
        string Name,
        string Kind,
        ProfilePictureSnapshot? ProfilePicture
    );

    private sealed record ProfilePictureSnapshot(
        string ContentType,
        int Length,
        string Sha256,
        string? Format,
        int? Width,
        int? Height
    );

    private sealed record ValidationProblemResponse(
        string? Type,
        string? Title,
        int? Status,
        IReadOnlyDictionary<string, string[]>? Errors
    );
}
