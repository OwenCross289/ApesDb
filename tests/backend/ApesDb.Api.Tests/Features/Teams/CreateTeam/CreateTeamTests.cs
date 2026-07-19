using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using ApesDb.Api.Features.Teams;
using ApesDb.Api.Features.Teams.CreateTeam;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;

namespace ApesDb.Api.Tests.Features.Teams.CreateTeam;

public sealed class CreateTeamTests : IClassFixture<MutableEndpointApiFactory>, IAsyncLifetime
{
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

        await Verify(await CreateTeamAsync(content, false));
    }

    [Fact]
    public async Task ExistingUserCanCreateTeamWithProfilePicture()
    {
        using var content = CreateRequest("Picture Team", CreatePngImage(), "profile.png", "image/png");

        await Verify(await CreateTeamAsync(content, true));
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
        var oversizedProfilePicture = new byte[CreateTeamValidator.MaximumProfilePictureLength + 1];
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

    private async Task<CreatedTeamSnapshot> CreateTeamAsync(
        MultipartFormDataContent requestContent,
        bool expectsProfilePicture
    )
    {
        var before = await GetDatabaseCountsAsync();
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Owner);
        using var response = await client.PostAsync(
            TeamsEndpoint,
            requestContent,
            TestContext.Current.CancellationToken
        );
        var http = await HttpResponseSnapshot.CreateAsync<TeamResponse>(response);
        var payload = Assert.IsType<TeamResponse>(http.Content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal($"/api/teams/{payload.Id}", response.Headers.Location?.ToString());
        Assert.Equal(TestUsers.Owner.SeededUserId, payload.Members.Single().Id);
        Assert.Equal(TestUsers.Owner.Name, payload.Members.Single().Name);
        Assert.Equal(TestUsers.Owner.PictureUrl, payload.Members.Single().PictureUrl);
        Assert.Equal(TestClock.UtcNow, payload.CreatedAt);

        await using var scope = _factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var storedTeam = await dbContext
            .Teams.AsNoTracking()
            .SingleAsync(team => team.Id == payload.Id, TestContext.Current.CancellationToken);
        var storedMemberships = await dbContext
            .TeamMemberships.AsNoTracking()
            .Where(membership => membership.TeamId == payload.Id)
            .ToArrayAsync(TestContext.Current.CancellationToken);
        var storedMembership = Assert.Single(storedMemberships);
        var after = new DatabaseCounts(
            await dbContext.Teams.CountAsync(TestContext.Current.CancellationToken),
            await dbContext.TeamMemberships.CountAsync(TestContext.Current.CancellationToken)
        );

        Assert.Equal(before.Teams + 1, after.Teams);
        Assert.Equal(before.Memberships + 1, after.Memberships);
        Assert.Equal(payload.Id, storedMembership.TeamId);
        Assert.Equal(TestUsers.Owner.SeededUserId, storedMembership.UserId);
        Assert.Equal(TeamMembershipStatus.Accepted, storedMembership.Status);
        Assert.Null(storedMembership.InvitedByUserId);
        Assert.Equal(TestClock.UtcNow, storedMembership.InvitedAt);
        Assert.Equal(TestClock.UtcNow, storedMembership.AcceptedAt);

        var profilePicture = CreateProfilePictureSnapshot(
            payload.ProfilePicture,
            storedTeam.ProfilePicture,
            expectsProfilePicture
        );
        var responseContent = new TeamResponseSnapshot(
            payload.Id,
            payload.Name,
            payload.Kind,
            payload.CreatedAt,
            profilePicture,
            payload.Members
        );
        var team = new StoredTeamSnapshot(
            storedTeam.Id,
            storedTeam.OwnerUserId,
            storedTeam.Name,
            storedTeam.Kind.ToString(),
            storedTeam.CreatedAt,
            storedTeam.UpdatedAt,
            profilePicture
        );
        var membership = new StoredMembershipSnapshot(
            storedMembership.Id,
            storedMembership.TeamId,
            storedMembership.UserId,
            storedMembership.Status.ToString(),
            storedMembership.InvitedByUserId,
            storedMembership.InvitedAt,
            storedMembership.AcceptedAt
        );
        var responseDetails = new CreatedResponseDetailsSnapshot(
            http.Response.StatusCode,
            http.Response.ReasonPhrase,
            http.Response.Version,
            "/api/teams/{teamId}",
            http.Response.ContentHeaders
        );
        return new CreatedTeamSnapshot(responseDetails, responseContent, team, membership, before, after);
    }

    private async Task<RejectedCreateTeamSnapshot> RejectCreateTeamAsync(
        MultipartFormDataContent requestContent,
        TestUser? user
    )
    {
        var before = await GetDatabaseCountsAsync();
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
            var http = await HttpResponseSnapshot.CreateAsync<ValidationProblemResponse>(response);
            if (user is null)
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
            else
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

            var after = await GetDatabaseCountsAsync();
            Assert.Equal(before, after);
            var problem = http.Content as ValidationProblemResponse;
            return new RejectedCreateTeamSnapshot(http.Response, problem, before, after);
        }
    }

    private async Task<DatabaseCounts> GetDatabaseCountsAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return new DatabaseCounts(
            await dbContext.Teams.CountAsync(TestContext.Current.CancellationToken),
            await dbContext.TeamMemberships.CountAsync(TestContext.Current.CancellationToken)
        );
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

    private static ProfilePictureSnapshot? CreateProfilePictureSnapshot(
        TeamProfilePictureResponse? response,
        byte[]? storedData,
        bool expected
    )
    {
        if (!expected)
        {
            Assert.Null(response);
            Assert.Null(storedData);
            return null;
        }

        Assert.NotNull(response);
        Assert.NotNull(storedData);
        Assert.Equal("image/webp", response.ContentType);
        var responseHash = Convert.ToHexString(SHA256.HashData(response.Data));
        var storedHash = Convert.ToHexString(SHA256.HashData(storedData));
        Assert.Equal(responseHash, storedHash);

        using var stream = new MemoryStream(response.Data);
        using var codec = SKCodec.Create(stream);
        Assert.NotNull(codec);
        Assert.Equal(SKEncodedImageFormat.Webp, codec.EncodedFormat);
        Assert.Equal(TeamProfilePictureProcessor.OutputSize, codec.Info.Width);
        Assert.Equal(TeamProfilePictureProcessor.OutputSize, codec.Info.Height);

        return new ProfilePictureSnapshot(
            response.ContentType,
            response.Data.Length,
            responseHash,
            codec.EncodedFormat.ToString(),
            codec.Info.Width,
            codec.Info.Height
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

    private sealed record CreatedTeamSnapshot(
        CreatedResponseDetailsSnapshot Response,
        TeamResponseSnapshot Content,
        StoredTeamSnapshot StoredTeam,
        StoredMembershipSnapshot StoredMembership,
        DatabaseCounts Before,
        DatabaseCounts After
    );

    private sealed record CreatedResponseDetailsSnapshot(
        int StatusCode,
        string? ReasonPhrase,
        string Version,
        string Location,
        IReadOnlyDictionary<string, string[]> ContentHeaders
    );

    private sealed record TeamResponseSnapshot(
        Guid Id,
        string Name,
        string Kind,
        DateTime CreatedAt,
        ProfilePictureSnapshot? ProfilePicture,
        TeamMemberResponse[] Members
    );

    private sealed record StoredTeamSnapshot(
        Guid Id,
        Guid OwnerUserId,
        string Name,
        string Kind,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        ProfilePictureSnapshot? ProfilePicture
    );

    private sealed record StoredMembershipSnapshot(
        Guid Id,
        Guid TeamId,
        Guid UserId,
        string Status,
        Guid? InvitedByUserId,
        DateTime InvitedAt,
        DateTime? AcceptedAt
    );

    private sealed record ProfilePictureSnapshot(
        string ContentType,
        int Length,
        string Sha256,
        string Format,
        int Width,
        int Height
    );

    private sealed record RejectedCreateTeamSnapshot(
        HttpResponseDetails Response,
        ValidationProblemResponse? Content,
        DatabaseCounts Before,
        DatabaseCounts After
    );

    private sealed record ValidationProblemResponse(
        string? Type,
        string? Title,
        int? Status,
        IReadOnlyDictionary<string, string[]>? Errors
    );

    private sealed record DatabaseCounts(int Teams, int Memberships);
}
