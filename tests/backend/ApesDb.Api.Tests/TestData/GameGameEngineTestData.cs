using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameGameEngineTestData
{
    public static GameGameEngine[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, GameEngine> gameEngines
    )
    {
        return
        [
            new()
            {
                GameId = 11156L,
                GameEngineId = 2L,
                Game = games[11156L],
                GameEngine = gameEngines[2L],
            },
            new()
            {
                GameId = 11156L,
                GameEngineId = 1L,
                Game = games[11156L],
                GameEngine = gameEngines[1L],
            },
        ];
    }
}
