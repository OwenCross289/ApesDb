using ApesDb.Api.Features.Games.GetGames;

namespace ApesDb.Api.UnitTests.Features.Games.GetGames;

public sealed class GetGamesValidatorTests
{
    private const int MaximumFilterLength = 512;
    private readonly GetGamesValidator _validator = new();

    [Fact]
    public void DefaultRequestIsValid()
    {
        var result = _validator.Validate(new GetGamesRequest());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void PageMustBeAtLeastOne(int page)
    {
        var request = new GetGamesRequest { Page = page };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(GetGamesRequest.Page));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void PageSizeMustBeBetweenOneAndOneHundred(int pageSize)
    {
        var request = new GetGamesRequest { PageSize = pageSize };

        var result = _validator.Validate(request);

        Assert.Contains(result.Errors, error => error.PropertyName == nameof(GetGamesRequest.PageSize));
    }

    [Fact]
    public void FilterIdsMustNotBeNegative()
    {
        var request = new GetGamesRequest
        {
            GameTypeIds = [-1],
            GameStatusIds = [-1],
            GenreIds = [-1],
            ThemeIds = [-1],
            GameModeIds = [-1],
            PlayerPerspectiveIds = [-1],
            PlatformIds = [-1],
        };

        var result = _validator.Validate(request);

        Assert.Collection(
            result.Errors,
            error => Assert.Equal("GameTypeIds[0]", error.PropertyName),
            error => Assert.Equal("GameStatusIds[0]", error.PropertyName),
            error => Assert.Equal("GenreIds[0]", error.PropertyName),
            error => Assert.Equal("ThemeIds[0]", error.PropertyName),
            error => Assert.Equal("GameModeIds[0]", error.PropertyName),
            error => Assert.Equal("PlayerPerspectiveIds[0]", error.PropertyName),
            error => Assert.Equal("PlatformIds[0]", error.PropertyName)
        );
    }

    [Fact]
    public void FiltersAtMaximumLengthAreValid()
    {
        var maximumLengthFilter = new string('a', MaximumFilterLength);
        var request = new GetGamesRequest
        {
            Developer = maximumLengthFilter,
            Publisher = maximumLengthFilter,
            Collection = maximumLengthFilter,
            Franchise = maximumLengthFilter,
            Search = maximumLengthFilter,
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void FiltersMustNotExceedMaximumLength()
    {
        var overlongFilter = new string('a', MaximumFilterLength + 1);
        var request = new GetGamesRequest
        {
            Developer = overlongFilter,
            Publisher = overlongFilter,
            Collection = overlongFilter,
            Franchise = overlongFilter,
            Search = overlongFilter,
        };

        var result = _validator.Validate(request);

        Assert.Collection(
            result.Errors,
            error => Assert.Equal(nameof(GetGamesRequest.Developer), error.PropertyName),
            error => Assert.Equal(nameof(GetGamesRequest.Publisher), error.PropertyName),
            error => Assert.Equal(nameof(GetGamesRequest.Collection), error.PropertyName),
            error => Assert.Equal(nameof(GetGamesRequest.Franchise), error.PropertyName),
            error => Assert.Equal(nameof(GetGamesRequest.Search), error.PropertyName)
        );
    }
}
