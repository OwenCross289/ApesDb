using ApesDb.Api.Features.Teams.CreateTeam;
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
}
