using FastEndpoints;
using FluentValidation;

namespace ApesDb.Api.Features.Teams.CreateTeam;

public sealed class CreateTeamValidator : Validator<CreateTeamRequest>
{
    public const long MaximumProfilePictureLength = 5 * 1024 * 1024;

    public CreateTeamValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(128);
        RuleFor(request => request.ProfilePicture)
            .Must(profilePicture => profilePicture is null || profilePicture.Length <= MaximumProfilePictureLength)
            .WithMessage("Profile picture must not exceed 5 MB.");
    }
}
