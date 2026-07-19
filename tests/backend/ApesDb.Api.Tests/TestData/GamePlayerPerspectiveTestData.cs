using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GamePlayerPerspectiveTestData
{
    public static GamePlayerPerspective[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, PlayerPerspective> playerPerspectives
    )
    {
        return
        [
            new()
            {
                GameId = 492L,
                PlayerPerspectiveId = 2L,
                Game = games[492L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 534L,
                PlayerPerspectiveId = 2L,
                Game = games[534L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 547L,
                PlayerPerspectiveId = 2L,
                Game = games[547L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 548L,
                PlayerPerspectiveId = 2L,
                Game = games[548L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1022L,
                PlayerPerspectiveId = 3L,
                Game = games[1022L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1025L,
                PlayerPerspectiveId = 3L,
                Game = games[1025L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1025L,
                PlayerPerspectiveId = 4L,
                Game = games[1025L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 1026L,
                PlayerPerspectiveId = 3L,
                Game = games[1026L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1027L,
                PlayerPerspectiveId = 3L,
                Game = games[1027L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1027L,
                PlayerPerspectiveId = 4L,
                Game = games[1027L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 1028L,
                PlayerPerspectiveId = 3L,
                Game = games[1028L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1028L,
                PlayerPerspectiveId = 4L,
                Game = games[1028L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 1029L,
                PlayerPerspectiveId = 2L,
                Game = games[1029L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1030L,
                PlayerPerspectiveId = 2L,
                Game = games[1030L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1032L,
                PlayerPerspectiveId = 3L,
                Game = games[1032L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1033L,
                PlayerPerspectiveId = 2L,
                Game = games[1033L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1034L,
                PlayerPerspectiveId = 2L,
                Game = games[1034L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1034L,
                PlayerPerspectiveId = 3L,
                Game = games[1034L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1035L,
                PlayerPerspectiveId = 3L,
                Game = games[1035L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1036L,
                PlayerPerspectiveId = 2L,
                Game = games[1036L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1037L,
                PlayerPerspectiveId = 3L,
                Game = games[1037L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1038L,
                PlayerPerspectiveId = 3L,
                Game = games[1038L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1039L,
                PlayerPerspectiveId = 2L,
                Game = games[1039L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1041L,
                PlayerPerspectiveId = 3L,
                Game = games[1041L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 1319L,
                PlayerPerspectiveId = 2L,
                Game = games[1319L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 1626L,
                PlayerPerspectiveId = 4L,
                Game = games[1626L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 1627L,
                PlayerPerspectiveId = 4L,
                Game = games[1627L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 1628L,
                PlayerPerspectiveId = 4L,
                Game = games[1628L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 2172L,
                PlayerPerspectiveId = 1L,
                Game = games[2172L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 2172L,
                PlayerPerspectiveId = 2L,
                Game = games[2172L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 2172L,
                PlayerPerspectiveId = 3L,
                Game = games[2172L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 2172L,
                PlayerPerspectiveId = 4L,
                Game = games[2172L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 2276L,
                PlayerPerspectiveId = 2L,
                Game = games[2276L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 2350L,
                PlayerPerspectiveId = 2L,
                Game = games[2350L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 2909L,
                PlayerPerspectiveId = 2L,
                Game = games[2909L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 2909L,
                PlayerPerspectiveId = 3L,
                Game = games[2909L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 4746L,
                PlayerPerspectiveId = 2L,
                Game = games[4746L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 4973L,
                PlayerPerspectiveId = 1L,
                Game = games[4973L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 5314L,
                PlayerPerspectiveId = 2L,
                Game = games[5314L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 6401L,
                PlayerPerspectiveId = 2L,
                Game = games[6401L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 6401L,
                PlayerPerspectiveId = 3L,
                Game = games[6401L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 6401L,
                PlayerPerspectiveId = 4L,
                Game = games[6401L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 6402L,
                PlayerPerspectiveId = 2L,
                Game = games[6402L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 6402L,
                PlayerPerspectiveId = 4L,
                Game = games[6402L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 7346L,
                PlayerPerspectiveId = 2L,
                Game = games[7346L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 8532L,
                PlayerPerspectiveId = 4L,
                Game = games[8532L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 8533L,
                PlayerPerspectiveId = 4L,
                Game = games[8533L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 8534L,
                PlayerPerspectiveId = 3L,
                Game = games[8534L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 8593L,
                PlayerPerspectiveId = 2L,
                Game = games[8593L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 8593L,
                PlayerPerspectiveId = 3L,
                Game = games[8593L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 9602L,
                PlayerPerspectiveId = 4L,
                Game = games[9602L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 9621L,
                PlayerPerspectiveId = 4L,
                Game = games[9621L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 11156L,
                PlayerPerspectiveId = 2L,
                Game = games[11156L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 11186L,
                PlayerPerspectiveId = 2L,
                Game = games[11186L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 11193L,
                PlayerPerspectiveId = 2L,
                Game = games[11193L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 11194L,
                PlayerPerspectiveId = 3L,
                Game = games[11194L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 11607L,
                PlayerPerspectiveId = 3L,
                Game = games[11607L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 18017L,
                PlayerPerspectiveId = 2L,
                Game = games[18017L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 18066L,
                PlayerPerspectiveId = 3L,
                Game = games[18066L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 20054L,
                PlayerPerspectiveId = 4L,
                Game = games[20054L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 22141L,
                PlayerPerspectiveId = 2L,
                Game = games[22141L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 23825L,
                PlayerPerspectiveId = 2L,
                Game = games[23825L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 25840L,
                PlayerPerspectiveId = 4L,
                Game = games[25840L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 26764L,
                PlayerPerspectiveId = 2L,
                Game = games[26764L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 37083L,
                PlayerPerspectiveId = 2L,
                Game = games[37083L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 38319L,
                PlayerPerspectiveId = 3L,
                Game = games[38319L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 41825L,
                PlayerPerspectiveId = 2L,
                Game = games[41825L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 41826L,
                PlayerPerspectiveId = 2L,
                Game = games[41826L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 41829L,
                PlayerPerspectiveId = 2L,
                Game = games[41829L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 42308L,
                PlayerPerspectiveId = 3L,
                Game = games[42308L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 42920L,
                PlayerPerspectiveId = 1L,
                Game = games[42920L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 42920L,
                PlayerPerspectiveId = 2L,
                Game = games[42920L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 44030L,
                PlayerPerspectiveId = 1L,
                Game = games[44030L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 44030L,
                PlayerPerspectiveId = 2L,
                Game = games[44030L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45133L,
                PlayerPerspectiveId = 1L,
                Game = games[45133L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 45133L,
                PlayerPerspectiveId = 2L,
                Game = games[45133L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45135L,
                PlayerPerspectiveId = 1L,
                Game = games[45135L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 45135L,
                PlayerPerspectiveId = 2L,
                Game = games[45135L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45136L,
                PlayerPerspectiveId = 2L,
                Game = games[45136L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45139L,
                PlayerPerspectiveId = 2L,
                Game = games[45139L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45139L,
                PlayerPerspectiveId = 3L,
                Game = games[45139L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 45139L,
                PlayerPerspectiveId = 4L,
                Game = games[45139L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 45142L,
                PlayerPerspectiveId = 2L,
                Game = games[45142L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 45143L,
                PlayerPerspectiveId = 3L,
                Game = games[45143L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 47604L,
                PlayerPerspectiveId = 2L,
                Game = games[47604L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 47828L,
                PlayerPerspectiveId = 4L,
                Game = games[47828L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 72870L,
                PlayerPerspectiveId = 2L,
                Game = games[72870L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 77336L,
                PlayerPerspectiveId = 3L,
                Game = games[77336L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 81147L,
                PlayerPerspectiveId = 2L,
                Game = games[81147L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 89904L,
                PlayerPerspectiveId = 2L,
                Game = games[89904L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 90101L,
                PlayerPerspectiveId = 4L,
                Game = games[90101L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 91680L,
                PlayerPerspectiveId = 3L,
                Game = games[91680L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 95040L,
                PlayerPerspectiveId = 2L,
                Game = games[95040L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95041L,
                PlayerPerspectiveId = 2L,
                Game = games[95041L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95042L,
                PlayerPerspectiveId = 2L,
                Game = games[95042L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95043L,
                PlayerPerspectiveId = 2L,
                Game = games[95043L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95044L,
                PlayerPerspectiveId = 2L,
                Game = games[95044L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95045L,
                PlayerPerspectiveId = 2L,
                Game = games[95045L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95046L,
                PlayerPerspectiveId = 2L,
                Game = games[95046L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95047L,
                PlayerPerspectiveId = 2L,
                Game = games[95047L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 95048L,
                PlayerPerspectiveId = 2L,
                Game = games[95048L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 103291L,
                PlayerPerspectiveId = 3L,
                Game = games[103291L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 103292L,
                PlayerPerspectiveId = 2L,
                Game = games[103292L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 112874L,
                PlayerPerspectiveId = 2L,
                Game = games[112874L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 115284L,
                PlayerPerspectiveId = 3L,
                Game = games[115284L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 115284L,
                PlayerPerspectiveId = 4L,
                Game = games[115284L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 116419L,
                PlayerPerspectiveId = 3L,
                Game = games[116419L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 119388L,
                PlayerPerspectiveId = 2L,
                Game = games[119388L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 133935L,
                PlayerPerspectiveId = 2L,
                Game = games[133935L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 134014L,
                PlayerPerspectiveId = 2L,
                Game = games[134014L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 134500L,
                PlayerPerspectiveId = 3L,
                Game = games[134500L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 135912L,
                PlayerPerspectiveId = 3L,
                Game = games[135912L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 136150L,
                PlayerPerspectiveId = 2L,
                Game = games[136150L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 136162L,
                PlayerPerspectiveId = 2L,
                Game = games[136162L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 136280L,
                PlayerPerspectiveId = 3L,
                Game = games[136280L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 136280L,
                PlayerPerspectiveId = 4L,
                Game = games[136280L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 136841L,
                PlayerPerspectiveId = 2L,
                Game = games[136841L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 138343L,
                PlayerPerspectiveId = 2L,
                Game = games[138343L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 141589L,
                PlayerPerspectiveId = 1L,
                Game = games[141589L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 141589L,
                PlayerPerspectiveId = 2L,
                Game = games[141589L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 141706L,
                PlayerPerspectiveId = 3L,
                Game = games[141706L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 141742L,
                PlayerPerspectiveId = 3L,
                Game = games[141742L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 143614L,
                PlayerPerspectiveId = 2L,
                Game = games[143614L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 143618L,
                PlayerPerspectiveId = 2L,
                Game = games[143618L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 152361L,
                PlayerPerspectiveId = 3L,
                Game = games[152361L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 152361L,
                PlayerPerspectiveId = 4L,
                Game = games[152361L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 152362L,
                PlayerPerspectiveId = 4L,
                Game = games[152362L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 163572L,
                PlayerPerspectiveId = 3L,
                Game = games[163572L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 168557L,
                PlayerPerspectiveId = 2L,
                Game = games[168557L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 170034L,
                PlayerPerspectiveId = 2L,
                Game = games[170034L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 171276L,
                PlayerPerspectiveId = 2L,
                Game = games[171276L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 172501L,
                PlayerPerspectiveId = 3L,
                Game = games[172501L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 176087L,
                PlayerPerspectiveId = 2L,
                Game = games[176087L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 178049L,
                PlayerPerspectiveId = 3L,
                Game = games[178049L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178050L,
                PlayerPerspectiveId = 3L,
                Game = games[178050L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178051L,
                PlayerPerspectiveId = 3L,
                Game = games[178051L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178053L,
                PlayerPerspectiveId = 3L,
                Game = games[178053L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178054L,
                PlayerPerspectiveId = 3L,
                Game = games[178054L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178056L,
                PlayerPerspectiveId = 3L,
                Game = games[178056L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178058L,
                PlayerPerspectiveId = 3L,
                Game = games[178058L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178059L,
                PlayerPerspectiveId = 3L,
                Game = games[178059L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178061L,
                PlayerPerspectiveId = 3L,
                Game = games[178061L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178062L,
                PlayerPerspectiveId = 3L,
                Game = games[178062L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178063L,
                PlayerPerspectiveId = 3L,
                Game = games[178063L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 178064L,
                PlayerPerspectiveId = 3L,
                Game = games[178064L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 184509L,
                PlayerPerspectiveId = 2L,
                Game = games[184509L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 184510L,
                PlayerPerspectiveId = 4L,
                Game = games[184510L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 186309L,
                PlayerPerspectiveId = 7L,
                Game = games[186309L],
                PlayerPerspective = playerPerspectives[7L],
            },
            new()
            {
                GameId = 191440L,
                PlayerPerspectiveId = 2L,
                Game = games[191440L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 199417L,
                PlayerPerspectiveId = 3L,
                Game = games[199417L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 206370L,
                PlayerPerspectiveId = 4L,
                Game = games[206370L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 206539L,
                PlayerPerspectiveId = 3L,
                Game = games[206539L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 208493L,
                PlayerPerspectiveId = 3L,
                Game = games[208493L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 213363L,
                PlayerPerspectiveId = 3L,
                Game = games[213363L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 213363L,
                PlayerPerspectiveId = 4L,
                Game = games[213363L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 213594L,
                PlayerPerspectiveId = 3L,
                Game = games[213594L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 213594L,
                PlayerPerspectiveId = 4L,
                Game = games[213594L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 216236L,
                PlayerPerspectiveId = 2L,
                Game = games[216236L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 227890L,
                PlayerPerspectiveId = 2L,
                Game = games[227890L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 227987L,
                PlayerPerspectiveId = 2L,
                Game = games[227987L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 228533L,
                PlayerPerspectiveId = 2L,
                Game = games[228533L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 228549L,
                PlayerPerspectiveId = 1L,
                Game = games[228549L],
                PlayerPerspective = playerPerspectives[1L],
            },
            new()
            {
                GameId = 229416L,
                PlayerPerspectiveId = 3L,
                Game = games[229416L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 232696L,
                PlayerPerspectiveId = 4L,
                Game = games[232696L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 235330L,
                PlayerPerspectiveId = 3L,
                Game = games[235330L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 237289L,
                PlayerPerspectiveId = 2L,
                Game = games[237289L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 239007L,
                PlayerPerspectiveId = 2L,
                Game = games[239007L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 268842L,
                PlayerPerspectiveId = 2L,
                Game = games[268842L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 293728L,
                PlayerPerspectiveId = 2L,
                Game = games[293728L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 298870L,
                PlayerPerspectiveId = 3L,
                Game = games[298870L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 299862L,
                PlayerPerspectiveId = 4L,
                Game = games[299862L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 305003L,
                PlayerPerspectiveId = 3L,
                Game = games[305003L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 306149L,
                PlayerPerspectiveId = 3L,
                Game = games[306149L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 306149L,
                PlayerPerspectiveId = 4L,
                Game = games[306149L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 313619L,
                PlayerPerspectiveId = 3L,
                Game = games[313619L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 313619L,
                PlayerPerspectiveId = 4L,
                Game = games[313619L],
                PlayerPerspective = playerPerspectives[4L],
            },
            new()
            {
                GameId = 317103L,
                PlayerPerspectiveId = 2L,
                Game = games[317103L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 318793L,
                PlayerPerspectiveId = 2L,
                Game = games[318793L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 320682L,
                PlayerPerspectiveId = 3L,
                Game = games[320682L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 321589L,
                PlayerPerspectiveId = 3L,
                Game = games[321589L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 322256L,
                PlayerPerspectiveId = 3L,
                Game = games[322256L],
                PlayerPerspective = playerPerspectives[3L],
            },
            new()
            {
                GameId = 338072L,
                PlayerPerspectiveId = 2L,
                Game = games[338072L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 338073L,
                PlayerPerspectiveId = 2L,
                Game = games[338073L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 338085L,
                PlayerPerspectiveId = 2L,
                Game = games[338085L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 342721L,
                PlayerPerspectiveId = 2L,
                Game = games[342721L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 360934L,
                PlayerPerspectiveId = 5L,
                Game = games[360934L],
                PlayerPerspective = playerPerspectives[5L],
            },
            new()
            {
                GameId = 360935L,
                PlayerPerspectiveId = 5L,
                Game = games[360935L],
                PlayerPerspective = playerPerspectives[5L],
            },
            new()
            {
                GameId = 377555L,
                PlayerPerspectiveId = 2L,
                Game = games[377555L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 388464L,
                PlayerPerspectiveId = 2L,
                Game = games[388464L],
                PlayerPerspective = playerPerspectives[2L],
            },
            new()
            {
                GameId = 405460L,
                PlayerPerspectiveId = 2L,
                Game = games[405460L],
                PlayerPerspective = playerPerspectives[2L],
            },
        ];
    }
}
