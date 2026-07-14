using System.ComponentModel;
using ApesDb.Common;
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
            .SortBy(ListSortDirection.Ascending, popularGame => popularGame.Rank)
            .Take(ResultCount)
            .Select(popularGame => new TopGameProjection(
                popularGame.Rank,
                popularGame.Game.Id,
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
            .Select(game =>
            {
                double? totalRating = null;
                if (game.TotalRating.HasValue)
                {
                    totalRating = decimal.ToDouble(game.TotalRating.Value);
                }

                return new TopGameResponse(
                    game.Rank,
                    game.Id,
                    game.Name,
                    game.Slug,
                    game.Summary,
                    totalRating,
                    ToDateTimeOffset(game.FirstReleaseDate),
                    game.CoverImageId,
                    game.CoverSmallUrl,
                    game.CoverLargeUrl,
                    decimal.ToDouble(game.Popularity)
                );
            })
            .ToArray();

        await Send.OkAsync(response, ct);
    }

    private static DateTimeOffset? ToDateTimeOffset(DateTime? value)
    {
        if (!value.HasValue)
        {
            return null;
        }

        return new DateTimeOffset(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc));
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
