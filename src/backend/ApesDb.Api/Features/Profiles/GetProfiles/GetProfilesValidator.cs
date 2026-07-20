using FastEndpoints;
using FluentValidation;

namespace ApesDb.Api.Features.Profiles.GetProfiles;

public sealed class GetProfilesValidator : Validator<GetProfilesRequest>
{
    private const int MaximumSearchLength = 256;

    public GetProfilesValidator()
    {
        RuleFor(request => request.Page).GreaterThanOrEqualTo(1);
        RuleFor(request => request.PageSize).InclusiveBetween(1, 100);
        RuleFor(request => request.Search).MaximumLength(MaximumSearchLength);
    }
}
