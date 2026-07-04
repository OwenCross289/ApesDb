using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Igdb.Sdk;

public sealed class IgdbGameService : IIgdbGameService
{
    private readonly IIgdbApi _api;

    public IgdbGameService(IIgdbApi api)
    {
        _api = api;
    }

    public async Task<IReadOnlyList<TopIgdbGame>> ListTopGamesAsync(
        int limit = 10,
        CancellationToken cancellationToken = default
    )
    {
        if (limit <= 0)
        {
            return [];
        }

        var popularityQuery =
            "fields game_id,value,popularity_type; "
            + $"sort value desc; limit {limit}; where popularity_type = 1;";

        var popularity = await _api.QueryPopularityPrimitivesAsync(
            popularityQuery,
            cancellationToken
        );

        if (popularity.Count == 0)
        {
            return [];
        }

        var gameIds = popularity.Select(game => game.GameId).Distinct().ToArray();
        var gamesQuery =
            "fields id,name,slug,summary,total_rating,first_release_date,cover.image_id; "
            + $"where id = ({string.Join(",", gameIds)}); limit {gameIds.Length};";
        var games = await _api.QueryGamesAsync(gamesQuery, cancellationToken);
        var gamesById = games.ToDictionary(game => game.Id);

        return popularity
            .Select(
                (entry, index) =>
                    gamesById.TryGetValue(entry.GameId, out var game)
                        ? new TopIgdbGame(
                            index + 1,
                            game.Id,
                            game.Name,
                            game.Slug,
                            game.Summary,
                            game.TotalRating,
                            ToDateTimeOffset(game.FirstReleaseDate),
                            game.Cover?.ImageId,
                            entry.Value
                        )
                        : null
            )
            .OfType<TopIgdbGame>()
            .ToArray();
    }

    private static DateTimeOffset? ToDateTimeOffset(long? unixSeconds)
    {
        return unixSeconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(unixSeconds.Value) : null;
    }
}
