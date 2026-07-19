using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameGameModeTestData
{
    public static GameGameMode[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, GameMode> gameModes
    )
    {
        return
        [
            new()
            {
                GameId = 492L,
                GameModeId = 1L,
                Game = games[492L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 492L,
                GameModeId = 2L,
                Game = games[492L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 492L,
                GameModeId = 3L,
                Game = games[492L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 492L,
                GameModeId = 4L,
                Game = games[492L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 534L,
                GameModeId = 1L,
                Game = games[534L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 547L,
                GameModeId = 1L,
                Game = games[547L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 547L,
                GameModeId = 2L,
                Game = games[547L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 547L,
                GameModeId = 3L,
                Game = games[547L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 547L,
                GameModeId = 4L,
                Game = games[547L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 548L,
                GameModeId = 1L,
                Game = games[548L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 548L,
                GameModeId = 2L,
                Game = games[548L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 548L,
                GameModeId = 3L,
                Game = games[548L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 548L,
                GameModeId = 4L,
                Game = games[548L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 1022L,
                GameModeId = 1L,
                Game = games[1022L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1025L,
                GameModeId = 1L,
                Game = games[1025L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1026L,
                GameModeId = 1L,
                Game = games[1026L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1027L,
                GameModeId = 1L,
                Game = games[1027L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1028L,
                GameModeId = 1L,
                Game = games[1028L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1029L,
                GameModeId = 1L,
                Game = games[1029L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1030L,
                GameModeId = 1L,
                Game = games[1030L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1032L,
                GameModeId = 1L,
                Game = games[1032L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1033L,
                GameModeId = 1L,
                Game = games[1033L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1034L,
                GameModeId = 1L,
                Game = games[1034L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1034L,
                GameModeId = 2L,
                Game = games[1034L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 1034L,
                GameModeId = 3L,
                Game = games[1034L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 1035L,
                GameModeId = 1L,
                Game = games[1035L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1036L,
                GameModeId = 1L,
                Game = games[1036L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1037L,
                GameModeId = 1L,
                Game = games[1037L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1038L,
                GameModeId = 1L,
                Game = games[1038L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1039L,
                GameModeId = 1L,
                Game = games[1039L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1041L,
                GameModeId = 1L,
                Game = games[1041L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1319L,
                GameModeId = 1L,
                Game = games[1319L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1319L,
                GameModeId = 2L,
                Game = games[1319L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 1319L,
                GameModeId = 3L,
                Game = games[1319L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 1319L,
                GameModeId = 4L,
                Game = games[1319L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 1626L,
                GameModeId = 1L,
                Game = games[1626L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1626L,
                GameModeId = 2L,
                Game = games[1626L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 1626L,
                GameModeId = 3L,
                Game = games[1626L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 1626L,
                GameModeId = 4L,
                Game = games[1626L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 1627L,
                GameModeId = 1L,
                Game = games[1627L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1627L,
                GameModeId = 2L,
                Game = games[1627L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 1627L,
                GameModeId = 3L,
                Game = games[1627L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 1628L,
                GameModeId = 1L,
                Game = games[1628L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 1628L,
                GameModeId = 2L,
                Game = games[1628L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 1628L,
                GameModeId = 3L,
                Game = games[1628L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 1628L,
                GameModeId = 4L,
                Game = games[1628L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 2172L,
                GameModeId = 1L,
                Game = games[2172L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 2172L,
                GameModeId = 2L,
                Game = games[2172L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 2172L,
                GameModeId = 3L,
                Game = games[2172L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 2172L,
                GameModeId = 4L,
                Game = games[2172L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 2276L,
                GameModeId = 1L,
                Game = games[2276L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 2350L,
                GameModeId = 1L,
                Game = games[2350L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 2350L,
                GameModeId = 2L,
                Game = games[2350L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 2350L,
                GameModeId = 4L,
                Game = games[2350L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 2909L,
                GameModeId = 1L,
                Game = games[2909L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 4746L,
                GameModeId = 1L,
                Game = games[4746L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 4973L,
                GameModeId = 1L,
                Game = games[4973L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 4973L,
                GameModeId = 2L,
                Game = games[4973L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 4973L,
                GameModeId = 4L,
                Game = games[4973L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 5314L,
                GameModeId = 1L,
                Game = games[5314L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 5314L,
                GameModeId = 3L,
                Game = games[5314L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 6401L,
                GameModeId = 1L,
                Game = games[6401L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 6402L,
                GameModeId = 1L,
                Game = games[6402L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 7346L,
                GameModeId = 1L,
                Game = games[7346L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 8532L,
                GameModeId = 1L,
                Game = games[8532L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 8533L,
                GameModeId = 1L,
                Game = games[8533L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 8534L,
                GameModeId = 1L,
                Game = games[8534L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 8593L,
                GameModeId = 1L,
                Game = games[8593L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 9602L,
                GameModeId = 1L,
                Game = games[9602L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 9602L,
                GameModeId = 2L,
                Game = games[9602L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 9602L,
                GameModeId = 3L,
                Game = games[9602L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 9602L,
                GameModeId = 4L,
                Game = games[9602L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 9621L,
                GameModeId = 1L,
                Game = games[9621L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 9621L,
                GameModeId = 2L,
                Game = games[9621L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 11156L,
                GameModeId = 1L,
                Game = games[11156L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 11186L,
                GameModeId = 1L,
                Game = games[11186L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 11186L,
                GameModeId = 2L,
                Game = games[11186L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 11186L,
                GameModeId = 3L,
                Game = games[11186L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 11186L,
                GameModeId = 4L,
                Game = games[11186L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 11193L,
                GameModeId = 1L,
                Game = games[11193L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 11193L,
                GameModeId = 2L,
                Game = games[11193L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 11194L,
                GameModeId = 1L,
                Game = games[11194L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 11194L,
                GameModeId = 2L,
                Game = games[11194L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 11194L,
                GameModeId = 3L,
                Game = games[11194L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 11607L,
                GameModeId = 1L,
                Game = games[11607L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 18017L,
                GameModeId = 1L,
                Game = games[18017L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 18066L,
                GameModeId = 1L,
                Game = games[18066L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 20054L,
                GameModeId = 1L,
                Game = games[20054L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 22141L,
                GameModeId = 1L,
                Game = games[22141L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 22141L,
                GameModeId = 2L,
                Game = games[22141L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 22141L,
                GameModeId = 4L,
                Game = games[22141L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 23825L,
                GameModeId = 1L,
                Game = games[23825L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 23825L,
                GameModeId = 3L,
                Game = games[23825L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 25840L,
                GameModeId = 1L,
                Game = games[25840L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 26764L,
                GameModeId = 1L,
                Game = games[26764L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 26764L,
                GameModeId = 2L,
                Game = games[26764L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 26764L,
                GameModeId = 4L,
                Game = games[26764L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 37083L,
                GameModeId = 1L,
                Game = games[37083L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 38319L,
                GameModeId = 1L,
                Game = games[38319L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 41825L,
                GameModeId = 1L,
                Game = games[41825L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 41826L,
                GameModeId = 1L,
                Game = games[41826L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 41829L,
                GameModeId = 1L,
                Game = games[41829L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 42308L,
                GameModeId = 1L,
                Game = games[42308L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 42920L,
                GameModeId = 1L,
                Game = games[42920L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 44030L,
                GameModeId = 1L,
                Game = games[44030L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45133L,
                GameModeId = 1L,
                Game = games[45133L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45135L,
                GameModeId = 1L,
                Game = games[45135L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45136L,
                GameModeId = 1L,
                Game = games[45136L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45139L,
                GameModeId = 1L,
                Game = games[45139L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45142L,
                GameModeId = 1L,
                Game = games[45142L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45143L,
                GameModeId = 1L,
                Game = games[45143L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 45143L,
                GameModeId = 2L,
                Game = games[45143L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 45143L,
                GameModeId = 3L,
                Game = games[45143L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 47604L,
                GameModeId = 1L,
                Game = games[47604L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 47604L,
                GameModeId = 2L,
                Game = games[47604L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 47828L,
                GameModeId = 1L,
                Game = games[47828L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 72870L,
                GameModeId = 1L,
                Game = games[72870L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 77336L,
                GameModeId = 1L,
                Game = games[77336L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 77336L,
                GameModeId = 2L,
                Game = games[77336L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 77336L,
                GameModeId = 3L,
                Game = games[77336L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 81147L,
                GameModeId = 1L,
                Game = games[81147L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 81147L,
                GameModeId = 2L,
                Game = games[81147L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 81147L,
                GameModeId = 3L,
                Game = games[81147L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 81147L,
                GameModeId = 4L,
                Game = games[81147L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 89904L,
                GameModeId = 1L,
                Game = games[89904L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 90101L,
                GameModeId = 1L,
                Game = games[90101L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 90101L,
                GameModeId = 2L,
                Game = games[90101L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 90101L,
                GameModeId = 3L,
                Game = games[90101L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 91680L,
                GameModeId = 1L,
                Game = games[91680L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 91680L,
                GameModeId = 2L,
                Game = games[91680L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 91680L,
                GameModeId = 3L,
                Game = games[91680L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95040L,
                GameModeId = 1L,
                Game = games[95040L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95040L,
                GameModeId = 3L,
                Game = games[95040L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95041L,
                GameModeId = 1L,
                Game = games[95041L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95041L,
                GameModeId = 3L,
                Game = games[95041L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95042L,
                GameModeId = 1L,
                Game = games[95042L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95042L,
                GameModeId = 3L,
                Game = games[95042L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95043L,
                GameModeId = 1L,
                Game = games[95043L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95043L,
                GameModeId = 3L,
                Game = games[95043L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95044L,
                GameModeId = 1L,
                Game = games[95044L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95044L,
                GameModeId = 3L,
                Game = games[95044L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95045L,
                GameModeId = 1L,
                Game = games[95045L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95045L,
                GameModeId = 3L,
                Game = games[95045L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95046L,
                GameModeId = 1L,
                Game = games[95046L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95046L,
                GameModeId = 3L,
                Game = games[95046L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95047L,
                GameModeId = 1L,
                Game = games[95047L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95047L,
                GameModeId = 3L,
                Game = games[95047L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 95048L,
                GameModeId = 1L,
                Game = games[95048L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 95048L,
                GameModeId = 3L,
                Game = games[95048L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 103291L,
                GameModeId = 1L,
                Game = games[103291L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 103292L,
                GameModeId = 1L,
                Game = games[103292L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 103292L,
                GameModeId = 2L,
                Game = games[103292L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 103292L,
                GameModeId = 3L,
                Game = games[103292L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 112874L,
                GameModeId = 1L,
                Game = games[112874L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 115284L,
                GameModeId = 1L,
                Game = games[115284L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 116419L,
                GameModeId = 1L,
                Game = games[116419L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 116419L,
                GameModeId = 3L,
                Game = games[116419L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 119388L,
                GameModeId = 1L,
                Game = games[119388L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 133935L,
                GameModeId = 1L,
                Game = games[133935L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 134014L,
                GameModeId = 1L,
                Game = games[134014L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 134500L,
                GameModeId = 1L,
                Game = games[134500L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 135912L,
                GameModeId = 1L,
                Game = games[135912L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 135912L,
                GameModeId = 3L,
                Game = games[135912L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 136150L,
                GameModeId = 1L,
                Game = games[136150L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 136162L,
                GameModeId = 1L,
                Game = games[136162L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 136280L,
                GameModeId = 1L,
                Game = games[136280L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 136841L,
                GameModeId = 1L,
                Game = games[136841L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 138343L,
                GameModeId = 1L,
                Game = games[138343L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 138343L,
                GameModeId = 2L,
                Game = games[138343L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 138343L,
                GameModeId = 3L,
                Game = games[138343L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 138343L,
                GameModeId = 4L,
                Game = games[138343L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 141589L,
                GameModeId = 1L,
                Game = games[141589L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 141706L,
                GameModeId = 1L,
                Game = games[141706L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 141706L,
                GameModeId = 3L,
                Game = games[141706L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 141742L,
                GameModeId = 1L,
                Game = games[141742L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 141742L,
                GameModeId = 3L,
                Game = games[141742L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 143614L,
                GameModeId = 1L,
                Game = games[143614L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 143618L,
                GameModeId = 1L,
                Game = games[143618L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 143618L,
                GameModeId = 3L,
                Game = games[143618L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 143618L,
                GameModeId = 4L,
                Game = games[143618L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 148431L,
                GameModeId = 1L,
                Game = games[148431L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 152361L,
                GameModeId = 1L,
                Game = games[152361L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 152362L,
                GameModeId = 1L,
                Game = games[152362L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 163572L,
                GameModeId = 1L,
                Game = games[163572L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 163572L,
                GameModeId = 2L,
                Game = games[163572L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 163572L,
                GameModeId = 3L,
                Game = games[163572L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 168557L,
                GameModeId = 1L,
                Game = games[168557L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 170034L,
                GameModeId = 1L,
                Game = games[170034L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 171276L,
                GameModeId = 1L,
                Game = games[171276L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 172501L,
                GameModeId = 1L,
                Game = games[172501L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 176087L,
                GameModeId = 1L,
                Game = games[176087L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178049L,
                GameModeId = 1L,
                Game = games[178049L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178050L,
                GameModeId = 1L,
                Game = games[178050L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178051L,
                GameModeId = 1L,
                Game = games[178051L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178053L,
                GameModeId = 1L,
                Game = games[178053L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178054L,
                GameModeId = 1L,
                Game = games[178054L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178056L,
                GameModeId = 1L,
                Game = games[178056L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178058L,
                GameModeId = 1L,
                Game = games[178058L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178059L,
                GameModeId = 1L,
                Game = games[178059L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178061L,
                GameModeId = 1L,
                Game = games[178061L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178062L,
                GameModeId = 1L,
                Game = games[178062L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178063L,
                GameModeId = 1L,
                Game = games[178063L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 178064L,
                GameModeId = 1L,
                Game = games[178064L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 184509L,
                GameModeId = 1L,
                Game = games[184509L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 184509L,
                GameModeId = 2L,
                Game = games[184509L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 184510L,
                GameModeId = 1L,
                Game = games[184510L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 184510L,
                GameModeId = 2L,
                Game = games[184510L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 186309L,
                GameModeId = 1L,
                Game = games[186309L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 191440L,
                GameModeId = 1L,
                Game = games[191440L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 199417L,
                GameModeId = 1L,
                Game = games[199417L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 206370L,
                GameModeId = 1L,
                Game = games[206370L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 206539L,
                GameModeId = 1L,
                Game = games[206539L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 208493L,
                GameModeId = 1L,
                Game = games[208493L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 213363L,
                GameModeId = 1L,
                Game = games[213363L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 213363L,
                GameModeId = 2L,
                Game = games[213363L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 213594L,
                GameModeId = 1L,
                Game = games[213594L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 216236L,
                GameModeId = 1L,
                Game = games[216236L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 227890L,
                GameModeId = 1L,
                Game = games[227890L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 227987L,
                GameModeId = 1L,
                Game = games[227987L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 227987L,
                GameModeId = 2L,
                Game = games[227987L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 228533L,
                GameModeId = 1L,
                Game = games[228533L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 228549L,
                GameModeId = 1L,
                Game = games[228549L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 229416L,
                GameModeId = 1L,
                Game = games[229416L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 232696L,
                GameModeId = 1L,
                Game = games[232696L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 235330L,
                GameModeId = 1L,
                Game = games[235330L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 237289L,
                GameModeId = 1L,
                Game = games[237289L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 239007L,
                GameModeId = 1L,
                Game = games[239007L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 257514L,
                GameModeId = 1L,
                Game = games[257514L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 268842L,
                GameModeId = 1L,
                Game = games[268842L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 293728L,
                GameModeId = 1L,
                Game = games[293728L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 293728L,
                GameModeId = 3L,
                Game = games[293728L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 293728L,
                GameModeId = 4L,
                Game = games[293728L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 298870L,
                GameModeId = 1L,
                Game = games[298870L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 298870L,
                GameModeId = 2L,
                Game = games[298870L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 298870L,
                GameModeId = 3L,
                Game = games[298870L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 299862L,
                GameModeId = 1L,
                Game = games[299862L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 305003L,
                GameModeId = 1L,
                Game = games[305003L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 305003L,
                GameModeId = 2L,
                Game = games[305003L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 305003L,
                GameModeId = 3L,
                Game = games[305003L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 305160L,
                GameModeId = 1L,
                Game = games[305160L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 305160L,
                GameModeId = 2L,
                Game = games[305160L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 305160L,
                GameModeId = 3L,
                Game = games[305160L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 306149L,
                GameModeId = 1L,
                Game = games[306149L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 313619L,
                GameModeId = 1L,
                Game = games[313619L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 313619L,
                GameModeId = 2L,
                Game = games[313619L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 317103L,
                GameModeId = 1L,
                Game = games[317103L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 320682L,
                GameModeId = 1L,
                Game = games[320682L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 320682L,
                GameModeId = 2L,
                Game = games[320682L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 320682L,
                GameModeId = 3L,
                Game = games[320682L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 321589L,
                GameModeId = 1L,
                Game = games[321589L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 321589L,
                GameModeId = 2L,
                Game = games[321589L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 321589L,
                GameModeId = 3L,
                Game = games[321589L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 322256L,
                GameModeId = 1L,
                Game = games[322256L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 322256L,
                GameModeId = 2L,
                Game = games[322256L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 322256L,
                GameModeId = 3L,
                Game = games[322256L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 328591L,
                GameModeId = 1L,
                Game = games[328591L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 328666L,
                GameModeId = 1L,
                Game = games[328666L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 328861L,
                GameModeId = 1L,
                Game = games[328861L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 328949L,
                GameModeId = 1L,
                Game = games[328949L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 338072L,
                GameModeId = 1L,
                Game = games[338072L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 338073L,
                GameModeId = 1L,
                Game = games[338073L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 338085L,
                GameModeId = 1L,
                Game = games[338085L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 338085L,
                GameModeId = 2L,
                Game = games[338085L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 338085L,
                GameModeId = 4L,
                Game = games[338085L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 342721L,
                GameModeId = 1L,
                Game = games[342721L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 342721L,
                GameModeId = 2L,
                Game = games[342721L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 342721L,
                GameModeId = 3L,
                Game = games[342721L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 342721L,
                GameModeId = 4L,
                Game = games[342721L],
                GameMode = gameModes[4L],
            },
            new()
            {
                GameId = 350670L,
                GameModeId = 1L,
                Game = games[350670L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 360934L,
                GameModeId = 1L,
                Game = games[360934L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 360935L,
                GameModeId = 1L,
                Game = games[360935L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 377555L,
                GameModeId = 1L,
                Game = games[377555L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 377555L,
                GameModeId = 2L,
                Game = games[377555L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 377555L,
                GameModeId = 5L,
                Game = games[377555L],
                GameMode = gameModes[5L],
            },
            new()
            {
                GameId = 388464L,
                GameModeId = 1L,
                Game = games[388464L],
                GameMode = gameModes[1L],
            },
            new()
            {
                GameId = 388464L,
                GameModeId = 2L,
                Game = games[388464L],
                GameMode = gameModes[2L],
            },
            new()
            {
                GameId = 388464L,
                GameModeId = 3L,
                Game = games[388464L],
                GameMode = gameModes[3L],
            },
            new()
            {
                GameId = 405460L,
                GameModeId = 1L,
                Game = games[405460L],
                GameMode = gameModes[1L],
            },
        ];
    }
}
