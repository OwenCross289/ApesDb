using ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;

namespace ApesDb.Api.UnitTests.Features.Teams.Invites.CreateTeamInvite;

public sealed class CreateTeamInviteValidatorTests
{
    private readonly CreateTeamInviteValidator _validator = new();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void EmailMustNotBeEmpty(string email)
    {
        var request = new CreateTeamInviteRequest { Email = email };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamInviteRequest.Email));
    }

    [Fact]
    public void EmailMustNotExceedMaximumLength()
    {
        var request = new CreateTeamInviteRequest { Email = $"{new string('a', 245)}@example.com" };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamInviteRequest.Email));
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("user@")]
    public void EmailMustHaveAValidFormat(string email)
    {
        var request = new CreateTeamInviteRequest { Email = email };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateTeamInviteRequest.Email));
    }

    [Fact]
    public void ValidEmailIsAccepted()
    {
        var request = new CreateTeamInviteRequest { Email = "member@example.com" };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
