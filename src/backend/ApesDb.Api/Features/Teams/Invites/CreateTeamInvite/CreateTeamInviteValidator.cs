using FastEndpoints;
using FluentValidation;

namespace ApesDb.Api.Features.Teams.Invites.CreateTeamInvite;

public sealed class CreateTeamInviteValidator : Validator<CreateTeamInviteRequest>
{
    public CreateTeamInviteValidator()
    {
        RuleFor(request => request.Email).NotEmpty().MaximumLength(256).EmailAddress();
    }
}
