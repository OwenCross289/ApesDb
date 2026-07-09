using ApesDb.Domain;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Games.TopGames;

public sealed class TopGamesEndpoint : EndpointWithoutRequest
{
    private const int ResultCount = 10;

    private readonly ApplicationDbContext _dbContext;

    public TopGamesEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Top);
        AllowAnonymous();
        Summary(summary =>
        {
            summary.Summary = "Lists the top 10 games from the synchronized IGDB catalog.";
            summary.Description = "Returns the latest successful IGDB visits ranking stored in the database.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var games = await _dbContext
            .PopularGames.AsNoTracking()
            .OrderBy(popularGame => popularGame.Rank)
            .Take(ResultCount)
            .Select(popularGame => new TopGameProjection(
                popularGame.Rank,
                popularGame.Game.IgdbId,
                popularGame.Game.Name,
                popularGame.Game.Slug,
                popularGame.Game.Summary,
                popularGame.Game.TotalRating,
                popularGame.Game.FirstReleaseDate,
                popularGame.Game.CoverImageId,
                popularGame.Game.CoverSmallUrl,
                popularGame.Game.CoverLargeUrl,
                popularGame.Score
            ))
            .ToArrayAsync(ct);

        var response = games
            .Select(game => new TopGameResponse(
                game.Rank,
                game.Id,
                game.Name,
                game.Slug,
                game.Summary,
                game.TotalRating.HasValue ? decimal.ToDouble(game.TotalRating.Value) : null,
                ToDateTimeOffset(game.FirstReleaseDate),
                game.CoverImageId,
                game.CoverSmallUrl,
                game.CoverLargeUrl,
                decimal.ToDouble(game.Popularity)
            ))
            .ToArray();

        await Send.OkAsync(response, ct);
    }

    private static DateTimeOffset? ToDateTimeOffset(DateTime? value)
    {
        return value.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)) : null;
    }

    private sealed record TopGameProjection(
        int Rank,
        long Id,
        string Name,
        string? Slug,
        string? Summary,
        decimal? TotalRating,
        DateTime? FirstReleaseDate,
        string? CoverImageId,
        string? CoverSmallUrl,
        string? CoverLargeUrl,
        decimal Popularity
    );
}
