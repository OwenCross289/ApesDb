using FastEndpoints;
using FluentValidation;

namespace ApesDb.Api.Features.Games.GetGames;

public sealed class GetGamesValidator : Validator<GetGamesRequest>
{
    private const int MaximumFilterLength = 512;

    public GetGamesValidator()
    {
        RuleFor(request => request.Page).GreaterThanOrEqualTo(1);
        RuleFor(request => request.PageSize).InclusiveBetween(1, 100);

        RuleForEach(request => request.GameTypeIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.GameStatusIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.GenreIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.ThemeIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.GameModeIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.PlayerPerspectiveIds).GreaterThanOrEqualTo(0);
        RuleForEach(request => request.PlatformIds).GreaterThanOrEqualTo(0);

        RuleFor(request => request.Developer).MaximumLength(MaximumFilterLength);
        RuleFor(request => request.Publisher).MaximumLength(MaximumFilterLength);
        RuleFor(request => request.Collection).MaximumLength(MaximumFilterLength);
        RuleFor(request => request.Franchise).MaximumLength(MaximumFilterLength);
        RuleFor(request => request.Search).MaximumLength(MaximumFilterLength);
    }
}
