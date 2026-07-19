using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameFranchiseTestData
{
    public static GameFranchise[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, Franchise> franchises
    )
    {
        return
        [
            new()
            {
                GameId = 492L,
                FranchiseId = 7084L,
                Game = games[492L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 534L,
                FranchiseId = 596L,
                Game = games[534L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 547L,
                FranchiseId = 7084L,
                Game = games[547L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 548L,
                FranchiseId = 7084L,
                Game = games[548L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 1022L,
                FranchiseId = 596L,
                Game = games[1022L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1025L,
                FranchiseId = 596L,
                Game = games[1025L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1026L,
                FranchiseId = 596L,
                Game = games[1026L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1027L,
                FranchiseId = 596L,
                Game = games[1027L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1028L,
                FranchiseId = 596L,
                Game = games[1028L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1029L,
                FranchiseId = 596L,
                Game = games[1029L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1030L,
                FranchiseId = 596L,
                Game = games[1030L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1032L,
                FranchiseId = 596L,
                Game = games[1032L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1033L,
                FranchiseId = 596L,
                Game = games[1033L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1034L,
                FranchiseId = 596L,
                Game = games[1034L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1035L,
                FranchiseId = 596L,
                Game = games[1035L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1036L,
                FranchiseId = 596L,
                Game = games[1036L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1037L,
                FranchiseId = 596L,
                Game = games[1037L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1038L,
                FranchiseId = 596L,
                Game = games[1038L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1039L,
                FranchiseId = 596L,
                Game = games[1039L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1041L,
                FranchiseId = 596L,
                Game = games[1041L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1319L,
                FranchiseId = 7084L,
                Game = games[1319L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 24L,
                Game = games[1626L],
                Franchise = franchises[24L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 60L,
                Game = games[1626L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 123L,
                Game = games[1626L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 596L,
                Game = games[1626L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 756L,
                Game = games[1626L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 763L,
                Game = games[1626L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 771L,
                Game = games[1626L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 789L,
                Game = games[1626L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 845L,
                Game = games[1626L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 1567L,
                Game = games[1626L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 1764L,
                Game = games[1626L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 1626L,
                FranchiseId = 1787L,
                Game = games[1626L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 60L,
                Game = games[1627L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 123L,
                Game = games[1627L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 596L,
                Game = games[1627L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 756L,
                Game = games[1627L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 763L,
                Game = games[1627L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 769L,
                Game = games[1627L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 771L,
                Game = games[1627L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 789L,
                Game = games[1627L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 845L,
                Game = games[1627L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 1567L,
                Game = games[1627L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 1764L,
                Game = games[1627L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 1627L,
                FranchiseId = 1787L,
                Game = games[1627L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 60L,
                Game = games[1628L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 123L,
                Game = games[1628L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 457L,
                Game = games[1628L],
                Franchise = franchises[457L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 551L,
                Game = games[1628L],
                Franchise = franchises[551L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 596L,
                Game = games[1628L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 756L,
                Game = games[1628L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 763L,
                Game = games[1628L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 769L,
                Game = games[1628L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 771L,
                Game = games[1628L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 789L,
                Game = games[1628L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 845L,
                Game = games[1628L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1518L,
                Game = games[1628L],
                Franchise = franchises[1518L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1567L,
                Game = games[1628L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1752L,
                Game = games[1628L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1764L,
                Game = games[1628L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1787L,
                Game = games[1628L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 1820L,
                Game = games[1628L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 1628L,
                FranchiseId = 5396L,
                Game = games[1628L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 596L,
                Game = games[2172L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 756L,
                Game = games[2172L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 763L,
                Game = games[2172L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 845L,
                Game = games[2172L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 1518L,
                Game = games[2172L],
                Franchise = franchises[1518L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 1567L,
                Game = games[2172L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 1764L,
                Game = games[2172L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 2172L,
                FranchiseId = 1820L,
                Game = games[2172L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 2276L,
                FranchiseId = 596L,
                Game = games[2276L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 24L,
                Game = games[2350L],
                Franchise = franchises[24L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 596L,
                Game = games[2350L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 763L,
                Game = games[2350L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 845L,
                Game = games[2350L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 1567L,
                Game = games[2350L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 1752L,
                Game = games[2350L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 1764L,
                Game = games[2350L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 1793L,
                Game = games[2350L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 2350L,
                FranchiseId = 1820L,
                Game = games[2350L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 2909L,
                FranchiseId = 596L,
                Game = games[2909L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 4746L,
                FranchiseId = 596L,
                Game = games[4746L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 4746L,
                FranchiseId = 845L,
                Game = games[4746L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 4746L,
                FranchiseId = 1821L,
                Game = games[4746L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 4746L,
                FranchiseId = 3106L,
                Game = games[4746L],
                Franchise = franchises[3106L],
            },
            new()
            {
                GameId = 4973L,
                FranchiseId = 596L,
                Game = games[4973L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 5314L,
                FranchiseId = 596L,
                Game = games[5314L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 5314L,
                FranchiseId = 2775L,
                Game = games[5314L],
                Franchise = franchises[2775L],
            },
            new()
            {
                GameId = 6401L,
                FranchiseId = 596L,
                Game = games[6401L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 6401L,
                FranchiseId = 763L,
                Game = games[6401L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 6401L,
                FranchiseId = 845L,
                Game = games[6401L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 6401L,
                FranchiseId = 1793L,
                Game = games[6401L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 596L,
                Game = games[6402L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 756L,
                Game = games[6402L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 789L,
                Game = games[6402L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 845L,
                Game = games[6402L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 1752L,
                Game = games[6402L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 1821L,
                Game = games[6402L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 6402L,
                FranchiseId = 5396L,
                Game = games[6402L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 7346L,
                FranchiseId = 596L,
                Game = games[7346L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 8532L,
                FranchiseId = 596L,
                Game = games[8532L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 8533L,
                FranchiseId = 596L,
                Game = games[8533L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 8534L,
                FranchiseId = 596L,
                Game = games[8534L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 8593L,
                FranchiseId = 596L,
                Game = games[8593L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 38L,
                Game = games[9602L],
                Franchise = franchises[38L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 60L,
                Game = games[9602L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 123L,
                Game = games[9602L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 457L,
                Game = games[9602L],
                Franchise = franchises[457L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 596L,
                Game = games[9602L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 756L,
                Game = games[9602L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 763L,
                Game = games[9602L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 769L,
                Game = games[9602L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 771L,
                Game = games[9602L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 789L,
                Game = games[9602L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 845L,
                Game = games[9602L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 892L,
                Game = games[9602L],
                Franchise = franchises[892L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1518L,
                Game = games[9602L],
                Franchise = franchises[1518L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1567L,
                Game = games[9602L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1752L,
                Game = games[9602L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1764L,
                Game = games[9602L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1787L,
                Game = games[9602L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1820L,
                Game = games[9602L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1821L,
                Game = games[9602L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 1932L,
                Game = games[9602L],
                Franchise = franchises[1932L],
            },
            new()
            {
                GameId = 9602L,
                FranchiseId = 5396L,
                Game = games[9602L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 38L,
                Game = games[9621L],
                Franchise = franchises[38L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 60L,
                Game = games[9621L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 123L,
                Game = games[9621L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 457L,
                Game = games[9621L],
                Franchise = franchises[457L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 596L,
                Game = games[9621L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 756L,
                Game = games[9621L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 763L,
                Game = games[9621L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 769L,
                Game = games[9621L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 771L,
                Game = games[9621L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 789L,
                Game = games[9621L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 845L,
                Game = games[9621L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 892L,
                Game = games[9621L],
                Franchise = franchises[892L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1518L,
                Game = games[9621L],
                Franchise = franchises[1518L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1567L,
                Game = games[9621L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1764L,
                Game = games[9621L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1787L,
                Game = games[9621L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1820L,
                Game = games[9621L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1821L,
                Game = games[9621L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 1932L,
                Game = games[9621L],
                Franchise = franchises[1932L],
            },
            new()
            {
                GameId = 9621L,
                FranchiseId = 5396L,
                Game = games[9621L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 11156L,
                FranchiseId = 2000L,
                Game = games[11156L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 11186L,
                FranchiseId = 7084L,
                Game = games[11186L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 11193L,
                FranchiseId = 596L,
                Game = games[11193L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 11193L,
                FranchiseId = 2775L,
                Game = games[11193L],
                Franchise = franchises[2775L],
            },
            new()
            {
                GameId = 11194L,
                FranchiseId = 596L,
                Game = games[11194L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 11607L,
                FranchiseId = 596L,
                Game = games[11607L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 18017L,
                FranchiseId = 596L,
                Game = games[18017L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 18066L,
                FranchiseId = 596L,
                Game = games[18066L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 596L,
                Game = games[20054L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 756L,
                Game = games[20054L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 763L,
                Game = games[20054L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 789L,
                Game = games[20054L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 845L,
                Game = games[20054L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 1793L,
                Game = games[20054L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 20054L,
                FranchiseId = 5396L,
                Game = games[20054L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 22141L,
                FranchiseId = 596L,
                Game = games[22141L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 22141L,
                FranchiseId = 845L,
                Game = games[22141L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 22141L,
                FranchiseId = 1764L,
                Game = games[22141L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 22141L,
                FranchiseId = 1793L,
                Game = games[22141L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 23825L,
                FranchiseId = 596L,
                Game = games[23825L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 25840L,
                FranchiseId = 596L,
                Game = games[25840L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 596L,
                Game = games[26764L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 763L,
                Game = games[26764L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 845L,
                Game = games[26764L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1228L,
                Game = games[26764L],
                Franchise = franchises[1228L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1567L,
                Game = games[26764L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1752L,
                Game = games[26764L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1764L,
                Game = games[26764L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1793L,
                Game = games[26764L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 26764L,
                FranchiseId = 1820L,
                Game = games[26764L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 37083L,
                FranchiseId = 2000L,
                Game = games[37083L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 38319L,
                FranchiseId = 596L,
                Game = games[38319L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 41825L,
                FranchiseId = 596L,
                Game = games[41825L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 41826L,
                FranchiseId = 596L,
                Game = games[41826L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 41829L,
                FranchiseId = 596L,
                Game = games[41829L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 42308L,
                FranchiseId = 596L,
                Game = games[42308L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 42920L,
                FranchiseId = 2000L,
                Game = games[42920L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 44030L,
                FranchiseId = 596L,
                Game = games[44030L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45133L,
                FranchiseId = 596L,
                Game = games[45133L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45135L,
                FranchiseId = 596L,
                Game = games[45135L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45136L,
                FranchiseId = 596L,
                Game = games[45136L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45137L,
                FranchiseId = 596L,
                Game = games[45137L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45139L,
                FranchiseId = 596L,
                Game = games[45139L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45142L,
                FranchiseId = 596L,
                Game = games[45142L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 45143L,
                FranchiseId = 596L,
                Game = games[45143L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 47384L,
                FranchiseId = 7084L,
                Game = games[47384L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 47456L,
                FranchiseId = 7084L,
                Game = games[47456L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 47604L,
                FranchiseId = 596L,
                Game = games[47604L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 47604L,
                FranchiseId = 2775L,
                Game = games[47604L],
                Franchise = franchises[2775L],
            },
            new()
            {
                GameId = 47828L,
                FranchiseId = 596L,
                Game = games[47828L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 50598L,
                FranchiseId = 596L,
                Game = games[50598L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 58782L,
                FranchiseId = 596L,
                Game = games[58782L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 60431L,
                FranchiseId = 596L,
                Game = games[60431L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 60431L,
                FranchiseId = 845L,
                Game = games[60431L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 72870L,
                FranchiseId = 2000L,
                Game = games[72870L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 75295L,
                FranchiseId = 596L,
                Game = games[75295L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 77336L,
                FranchiseId = 596L,
                Game = games[77336L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 81147L,
                FranchiseId = 596L,
                Game = games[81147L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 81147L,
                FranchiseId = 2775L,
                Game = games[81147L],
                Franchise = franchises[2775L],
            },
            new()
            {
                GameId = 89904L,
                FranchiseId = 596L,
                Game = games[89904L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 4L,
                Game = games[90101L],
                Franchise = franchises[4L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 38L,
                Game = games[90101L],
                Franchise = franchises[38L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 60L,
                Game = games[90101L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 123L,
                Game = games[90101L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 457L,
                Game = games[90101L],
                Franchise = franchises[457L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 551L,
                Game = games[90101L],
                Franchise = franchises[551L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 596L,
                Game = games[90101L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 756L,
                Game = games[90101L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 763L,
                Game = games[90101L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 769L,
                Game = games[90101L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 771L,
                Game = games[90101L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 789L,
                Game = games[90101L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 792L,
                Game = games[90101L],
                Franchise = franchises[792L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 845L,
                Game = games[90101L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 892L,
                Game = games[90101L],
                Franchise = franchises[892L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 895L,
                Game = games[90101L],
                Franchise = franchises[895L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1228L,
                Game = games[90101L],
                Franchise = franchises[1228L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1518L,
                Game = games[90101L],
                Franchise = franchises[1518L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1567L,
                Game = games[90101L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1752L,
                Game = games[90101L],
                Franchise = franchises[1752L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1764L,
                Game = games[90101L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1787L,
                Game = games[90101L],
                Franchise = franchises[1787L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1820L,
                Game = games[90101L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1821L,
                Game = games[90101L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 1932L,
                Game = games[90101L],
                Franchise = franchises[1932L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 3081L,
                Game = games[90101L],
                Franchise = franchises[3081L],
            },
            new()
            {
                GameId = 90101L,
                FranchiseId = 5396L,
                Game = games[90101L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 91680L,
                FranchiseId = 596L,
                Game = games[91680L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95040L,
                FranchiseId = 596L,
                Game = games[95040L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95041L,
                FranchiseId = 596L,
                Game = games[95041L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95042L,
                FranchiseId = 596L,
                Game = games[95042L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95043L,
                FranchiseId = 596L,
                Game = games[95043L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95044L,
                FranchiseId = 596L,
                Game = games[95044L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95045L,
                FranchiseId = 596L,
                Game = games[95045L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95046L,
                FranchiseId = 596L,
                Game = games[95046L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95047L,
                FranchiseId = 596L,
                Game = games[95047L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 95048L,
                FranchiseId = 596L,
                Game = games[95048L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 100169L,
                FranchiseId = 596L,
                Game = games[100169L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 103291L,
                FranchiseId = 7084L,
                Game = games[103291L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 103292L,
                FranchiseId = 7084L,
                Game = games[103292L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 112874L,
                FranchiseId = 2000L,
                Game = games[112874L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 115284L,
                FranchiseId = 596L,
                Game = games[115284L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 116419L,
                FranchiseId = 596L,
                Game = games[116419L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 119388L,
                FranchiseId = 596L,
                Game = games[119388L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 133935L,
                FranchiseId = 457L,
                Game = games[133935L],
                Franchise = franchises[457L],
            },
            new()
            {
                GameId = 133935L,
                FranchiseId = 596L,
                Game = games[133935L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 134014L,
                FranchiseId = 596L,
                Game = games[134014L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 134500L,
                FranchiseId = 596L,
                Game = games[134500L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 135912L,
                FranchiseId = 596L,
                Game = games[135912L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 136150L,
                FranchiseId = 2000L,
                Game = games[136150L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 136162L,
                FranchiseId = 596L,
                Game = games[136162L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 136280L,
                FranchiseId = 596L,
                Game = games[136280L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 136841L,
                FranchiseId = 596L,
                Game = games[136841L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 138343L,
                FranchiseId = 596L,
                Game = games[138343L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 138343L,
                FranchiseId = 2775L,
                Game = games[138343L],
                Franchise = franchises[2775L],
            },
            new()
            {
                GameId = 141589L,
                FranchiseId = 596L,
                Game = games[141589L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 141706L,
                FranchiseId = 596L,
                Game = games[141706L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 141742L,
                FranchiseId = 596L,
                Game = games[141742L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 143614L,
                FranchiseId = 596L,
                Game = games[143614L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 143618L,
                FranchiseId = 596L,
                Game = games[143618L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 148431L,
                FranchiseId = 596L,
                Game = games[148431L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 148431L,
                FranchiseId = 2422L,
                Game = games[148431L],
                Franchise = franchises[2422L],
            },
            new()
            {
                GameId = 152361L,
                FranchiseId = 596L,
                Game = games[152361L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 152362L,
                FranchiseId = 596L,
                Game = games[152362L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 163572L,
                FranchiseId = 596L,
                Game = games[163572L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 168557L,
                FranchiseId = 2000L,
                Game = games[168557L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 170034L,
                FranchiseId = 2000L,
                Game = games[170034L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 171276L,
                FranchiseId = 2000L,
                Game = games[171276L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 172501L,
                FranchiseId = 596L,
                Game = games[172501L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 176087L,
                FranchiseId = 596L,
                Game = games[176087L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178049L,
                FranchiseId = 596L,
                Game = games[178049L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178050L,
                FranchiseId = 596L,
                Game = games[178050L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178051L,
                FranchiseId = 596L,
                Game = games[178051L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178053L,
                FranchiseId = 596L,
                Game = games[178053L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178054L,
                FranchiseId = 596L,
                Game = games[178054L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178056L,
                FranchiseId = 596L,
                Game = games[178056L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178058L,
                FranchiseId = 596L,
                Game = games[178058L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178059L,
                FranchiseId = 596L,
                Game = games[178059L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178061L,
                FranchiseId = 596L,
                Game = games[178061L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178062L,
                FranchiseId = 596L,
                Game = games[178062L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178063L,
                FranchiseId = 596L,
                Game = games[178063L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 178064L,
                FranchiseId = 596L,
                Game = games[178064L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 184509L,
                FranchiseId = 596L,
                Game = games[184509L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 184510L,
                FranchiseId = 596L,
                Game = games[184510L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 186309L,
                FranchiseId = 2000L,
                Game = games[186309L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 191440L,
                FranchiseId = 596L,
                Game = games[191440L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 199417L,
                FranchiseId = 596L,
                Game = games[199417L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 206370L,
                FranchiseId = 596L,
                Game = games[206370L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 206539L,
                FranchiseId = 596L,
                Game = games[206539L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 208493L,
                FranchiseId = 596L,
                Game = games[208493L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 4L,
                Game = games[213363L],
                Franchise = franchises[4L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 123L,
                Game = games[213363L],
                Franchise = franchises[123L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 596L,
                Game = games[213363L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 633L,
                Game = games[213363L],
                Franchise = franchises[633L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 756L,
                Game = games[213363L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 763L,
                Game = games[213363L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 769L,
                Game = games[213363L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 771L,
                Game = games[213363L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 789L,
                Game = games[213363L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 792L,
                Game = games[213363L],
                Franchise = franchises[792L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 845L,
                Game = games[213363L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 895L,
                Game = games[213363L],
                Franchise = franchises[895L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 1567L,
                Game = games[213363L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 1764L,
                Game = games[213363L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 1811L,
                Game = games[213363L],
                Franchise = franchises[1811L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 1814L,
                Game = games[213363L],
                Franchise = franchises[1814L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 1821L,
                Game = games[213363L],
                Franchise = franchises[1821L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 2104L,
                Game = games[213363L],
                Franchise = franchises[2104L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 2685L,
                Game = games[213363L],
                Franchise = franchises[2685L],
            },
            new()
            {
                GameId = 213363L,
                FranchiseId = 7056L,
                Game = games[213363L],
                Franchise = franchises[7056L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 4L,
                Game = games[213594L],
                Franchise = franchises[4L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 38L,
                Game = games[213594L],
                Franchise = franchises[38L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 596L,
                Game = games[213594L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 633L,
                Game = games[213594L],
                Franchise = franchises[633L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 756L,
                Game = games[213594L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 763L,
                Game = games[213594L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 769L,
                Game = games[213594L],
                Franchise = franchises[769L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 771L,
                Game = games[213594L],
                Franchise = franchises[771L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 789L,
                Game = games[213594L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 792L,
                Game = games[213594L],
                Franchise = franchises[792L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 845L,
                Game = games[213594L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 872L,
                Game = games[213594L],
                Franchise = franchises[872L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 1567L,
                Game = games[213594L],
                Franchise = franchises[1567L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 1764L,
                Game = games[213594L],
                Franchise = franchises[1764L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 1811L,
                Game = games[213594L],
                Franchise = franchises[1811L],
            },
            new()
            {
                GameId = 213594L,
                FranchiseId = 1814L,
                Game = games[213594L],
                Franchise = franchises[1814L],
            },
            new()
            {
                GameId = 215237L,
                FranchiseId = 596L,
                Game = games[215237L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 216236L,
                FranchiseId = 596L,
                Game = games[216236L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 227890L,
                FranchiseId = 2000L,
                Game = games[227890L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 227987L,
                FranchiseId = 596L,
                Game = games[227987L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 227987L,
                FranchiseId = 2005L,
                Game = games[227987L],
                Franchise = franchises[2005L],
            },
            new()
            {
                GameId = 228533L,
                FranchiseId = 2000L,
                Game = games[228533L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 228549L,
                FranchiseId = 596L,
                Game = games[228549L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 229416L,
                FranchiseId = 596L,
                Game = games[229416L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 232696L,
                FranchiseId = 596L,
                Game = games[232696L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 235330L,
                FranchiseId = 596L,
                Game = games[235330L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 237289L,
                FranchiseId = 596L,
                Game = games[237289L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 239007L,
                FranchiseId = 596L,
                Game = games[239007L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 257514L,
                FranchiseId = 596L,
                Game = games[257514L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 268842L,
                FranchiseId = 2000L,
                Game = games[268842L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 293728L,
                FranchiseId = 596L,
                Game = games[293728L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 298870L,
                FranchiseId = 596L,
                Game = games[298870L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 596L,
                Game = games[299862L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 756L,
                Game = games[299862L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 763L,
                Game = games[299862L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 789L,
                Game = games[299862L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 845L,
                Game = games[299862L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 1793L,
                Game = games[299862L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 299862L,
                FranchiseId = 5396L,
                Game = games[299862L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 305003L,
                FranchiseId = 51L,
                Game = games[305003L],
                Franchise = franchises[51L],
            },
            new()
            {
                GameId = 305003L,
                FranchiseId = 2000L,
                Game = games[305003L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 305160L,
                FranchiseId = 7084L,
                Game = games[305160L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 306149L,
                FranchiseId = 596L,
                Game = games[306149L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 596L,
                Game = games[313619L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 756L,
                Game = games[313619L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 763L,
                Game = games[313619L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 789L,
                Game = games[313619L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 845L,
                Game = games[313619L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 1793L,
                Game = games[313619L],
                Franchise = franchises[1793L],
            },
            new()
            {
                GameId = 313619L,
                FranchiseId = 5396L,
                Game = games[313619L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 317103L,
                FranchiseId = 2000L,
                Game = games[317103L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 318793L,
                FranchiseId = 7084L,
                Game = games[318793L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 320682L,
                FranchiseId = 51L,
                Game = games[320682L],
                Franchise = franchises[51L],
            },
            new()
            {
                GameId = 320682L,
                FranchiseId = 2000L,
                Game = games[320682L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 321589L,
                FranchiseId = 51L,
                Game = games[321589L],
                Franchise = franchises[51L],
            },
            new()
            {
                GameId = 321589L,
                FranchiseId = 2000L,
                Game = games[321589L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 322256L,
                FranchiseId = 51L,
                Game = games[322256L],
                Franchise = franchises[51L],
            },
            new()
            {
                GameId = 322256L,
                FranchiseId = 2000L,
                Game = games[322256L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 328591L,
                FranchiseId = 60L,
                Game = games[328591L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 328591L,
                FranchiseId = 596L,
                Game = games[328591L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328591L,
                FranchiseId = 845L,
                Game = games[328591L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 328666L,
                FranchiseId = 596L,
                Game = games[328666L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328666L,
                FranchiseId = 756L,
                Game = games[328666L],
                Franchise = franchises[756L],
            },
            new()
            {
                GameId = 328666L,
                FranchiseId = 845L,
                Game = games[328666L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 328666L,
                FranchiseId = 895L,
                Game = games[328666L],
                Franchise = franchises[895L],
            },
            new()
            {
                GameId = 328861L,
                FranchiseId = 596L,
                Game = games[328861L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328862L,
                FranchiseId = 596L,
                Game = games[328862L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328863L,
                FranchiseId = 596L,
                Game = games[328863L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328864L,
                FranchiseId = 596L,
                Game = games[328864L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328865L,
                FranchiseId = 596L,
                Game = games[328865L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328866L,
                FranchiseId = 596L,
                Game = games[328866L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328867L,
                FranchiseId = 596L,
                Game = games[328867L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328868L,
                FranchiseId = 596L,
                Game = games[328868L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328869L,
                FranchiseId = 596L,
                Game = games[328869L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328870L,
                FranchiseId = 596L,
                Game = games[328870L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328871L,
                FranchiseId = 596L,
                Game = games[328871L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328872L,
                FranchiseId = 596L,
                Game = games[328872L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328873L,
                FranchiseId = 596L,
                Game = games[328873L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328874L,
                FranchiseId = 596L,
                Game = games[328874L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328875L,
                FranchiseId = 596L,
                Game = games[328875L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328876L,
                FranchiseId = 596L,
                Game = games[328876L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328877L,
                FranchiseId = 596L,
                Game = games[328877L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328949L,
                FranchiseId = 596L,
                Game = games[328949L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 328949L,
                FranchiseId = 6333L,
                Game = games[328949L],
                Franchise = franchises[6333L],
            },
            new()
            {
                GameId = 338072L,
                FranchiseId = 596L,
                Game = games[338072L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 338073L,
                FranchiseId = 596L,
                Game = games[338073L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 338085L,
                FranchiseId = 596L,
                Game = games[338085L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 342721L,
                FranchiseId = 7084L,
                Game = games[342721L],
                Franchise = franchises[7084L],
            },
            new()
            {
                GameId = 350670L,
                FranchiseId = 596L,
                Game = games[350670L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 350670L,
                FranchiseId = 845L,
                Game = games[350670L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 354416L,
                FranchiseId = 596L,
                Game = games[354416L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 354416L,
                FranchiseId = 1820L,
                Game = games[354416L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 354828L,
                FranchiseId = 596L,
                Game = games[354828L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 354828L,
                FranchiseId = 1820L,
                Game = games[354828L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 354829L,
                FranchiseId = 596L,
                Game = games[354829L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 354829L,
                FranchiseId = 1820L,
                Game = games[354829L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 360934L,
                FranchiseId = 596L,
                Game = games[360934L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 360934L,
                FranchiseId = 763L,
                Game = games[360934L],
                Franchise = franchises[763L],
            },
            new()
            {
                GameId = 360934L,
                FranchiseId = 845L,
                Game = games[360934L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 360934L,
                FranchiseId = 1820L,
                Game = games[360934L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 360934L,
                FranchiseId = 5396L,
                Game = games[360934L],
                Franchise = franchises[5396L],
            },
            new()
            {
                GameId = 360935L,
                FranchiseId = 60L,
                Game = games[360935L],
                Franchise = franchises[60L],
            },
            new()
            {
                GameId = 360935L,
                FranchiseId = 596L,
                Game = games[360935L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 360935L,
                FranchiseId = 789L,
                Game = games[360935L],
                Franchise = franchises[789L],
            },
            new()
            {
                GameId = 360935L,
                FranchiseId = 845L,
                Game = games[360935L],
                Franchise = franchises[845L],
            },
            new()
            {
                GameId = 364442L,
                FranchiseId = 596L,
                Game = games[364442L],
                Franchise = franchises[596L],
            },
            new()
            {
                GameId = 364442L,
                FranchiseId = 1820L,
                Game = games[364442L],
                Franchise = franchises[1820L],
            },
            new()
            {
                GameId = 377555L,
                FranchiseId = 2000L,
                Game = games[377555L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 388464L,
                FranchiseId = 2000L,
                Game = games[388464L],
                Franchise = franchises[2000L],
            },
            new()
            {
                GameId = 405460L,
                FranchiseId = 596L,
                Game = games[405460L],
                Franchise = franchises[596L],
            },
        ];
    }
}
