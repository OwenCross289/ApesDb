using ApesDb.Igdb.Sdk;
using FastEndpoints;

namespace ApesDb.Api.Features.Games.TopGames;

public sealed class TopGamesEndpoint : EndpointWithoutRequest
{
    private readonly IIgdbGameService _gameService;

    public TopGamesEndpoint(IIgdbGameService gameService)
    {
        _gameService = gameService;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.Top);
        AllowAnonymous();
        Summary(summary =>
        {
            summary.Summary = "Lists the top 10 games from IGDB.";
            summary.Description = "Returns the top 10 IGDB games by IGDB visits, preserving the popularity rank.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var games = await _gameService.ListTopGamesAsync(10, ct);

        await Send.OkAsync(games, ct);
    }
}
