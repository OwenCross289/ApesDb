using FastEndpoints;
using FluentValidation;

namespace ApesDb.Api.Features.Teams.CreateTeam;

public sealed class CreateTeamValidator : Validator<CreateTeamRequest>
{
    public CreateTeamValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(128);
    }
}
