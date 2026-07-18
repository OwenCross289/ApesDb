using ApesDb.Api.Features.Teams.CreateTeam;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ApesDb.Teams.IntegrationTests;

public sealed class CreateTeamValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_RejectsEmptyOrWhitespaceName(string name)
    {
        var validator = new CreateTeamValidator();
        var result = validator.Validate(new CreateTeamRequest { Name = name });

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamRequest.Name));
    }

    [Fact]
    public void Validate_AcceptsProfilePictureAtMaximumLength()
    {
        var validator = new CreateTeamValidator();
        var profilePicture = CreateFormFile(CreateTeamValidator.MaximumProfilePictureLength);

        var result = validator.Validate(new CreateTeamRequest { Name = "Apes", ProfilePicture = profilePicture });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_RejectsProfilePictureOverMaximumLength()
    {
        var validator = new CreateTeamValidator();
        var profilePicture = CreateFormFile(CreateTeamValidator.MaximumProfilePictureLength + 1);

        var result = validator.Validate(new CreateTeamRequest { Name = "Apes", ProfilePicture = profilePicture });

        var error = Assert.Single(
            result.Errors,
            error => error.PropertyName == nameof(CreateTeamRequest.ProfilePicture)
        );
        Assert.Equal("Profile picture must not exceed 5 MB.", error.ErrorMessage);
    }

    private static IFormFile CreateFormFile(long length)
    {
        return new FormFile(Stream.Null, 0, length, "ProfilePicture", "avatar.png");
    }
}
