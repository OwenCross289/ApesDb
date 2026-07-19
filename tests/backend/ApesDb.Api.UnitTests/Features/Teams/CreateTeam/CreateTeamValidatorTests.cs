using ApesDb.Api.Features.Teams.CreateTeam;
using Microsoft.AspNetCore.Http;

namespace ApesDb.Api.UnitTests.Features.Teams.CreateTeam;

public sealed class CreateTeamValidatorTests
{
    private readonly CreateTeamValidator _validator = new();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void NameMustNotBeEmpty(string name)
    {
        var request = new CreateTeamRequest { Name = name };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamRequest.Name));
    }

    [Fact]
    public void NameMustNotExceedMaximumLength()
    {
        var request = new CreateTeamRequest { Name = new string('a', 129) };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamRequest.Name));
    }

    [Fact]
    public void NameAtMaximumLengthIsValid()
    {
        var request = new CreateTeamRequest { Name = new string('a', 128) };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ProfilePictureAtMaximumLengthIsValid()
    {
        var request = new CreateTeamRequest
        {
            Name = "Team",
            ProfilePicture = CreateFormFile(CreateTeamValidator.MaximumProfilePictureLength),
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ProfilePictureMustNotExceedMaximumLength()
    {
        var request = new CreateTeamRequest
        {
            Name = "Team",
            ProfilePicture = CreateFormFile(CreateTeamValidator.MaximumProfilePictureLength + 1),
        };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamRequest.ProfilePicture));
    }

    private static IFormFile CreateFormFile(long length)
    {
        return new FormFile(Stream.Null, 0, length, "profilePicture", "profile-picture.png");
    }
}
