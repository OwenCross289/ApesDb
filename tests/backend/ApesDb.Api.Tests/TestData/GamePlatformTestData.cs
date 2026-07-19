using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GamePlatformTestData
{
    public static GamePlatform[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, Platform> platforms
    )
    {
        return
        [
            new()
            {
                GameId = 492L,
                PlatformId = 12L,
                Game = games[492L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 534L,
                PlatformId = 5L,
                Game = games[534L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 534L,
                PlatformId = 41L,
                Game = games[534L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 547L,
                PlatformId = 12L,
                Game = games[547L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 548L,
                PlatformId = 12L,
                Game = games[548L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 5L,
                Game = games[1022L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 18L,
                Game = games[1022L],
                Platform = platforms[18L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 37L,
                Game = games[1022L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 41L,
                Game = games[1022L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 51L,
                Game = games[1022L],
                Platform = platforms[51L],
            },
            new()
            {
                GameId = 1022L,
                PlatformId = 99L,
                Game = games[1022L],
                Platform = platforms[99L],
            },
            new()
            {
                GameId = 1025L,
                PlatformId = 5L,
                Game = games[1025L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1025L,
                PlatformId = 18L,
                Game = games[1025L],
                Platform = platforms[18L],
            },
            new()
            {
                GameId = 1025L,
                PlatformId = 37L,
                Game = games[1025L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1025L,
                PlatformId = 41L,
                Game = games[1025L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1025L,
                PlatformId = 51L,
                Game = games[1025L],
                Platform = platforms[51L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 5L,
                Game = games[1026L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 19L,
                Game = games[1026L],
                Platform = platforms[19L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 41L,
                Game = games[1026L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 58L,
                Game = games[1026L],
                Platform = platforms[58L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 137L,
                Game = games[1026L],
                Platform = platforms[137L],
            },
            new()
            {
                GameId = 1026L,
                PlatformId = 306L,
                Game = games[1026L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 1027L,
                PlatformId = 22L,
                Game = games[1027L],
                Platform = platforms[22L],
            },
            new()
            {
                GameId = 1027L,
                PlatformId = 37L,
                Game = games[1027L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1028L,
                PlatformId = 33L,
                Game = games[1028L],
                Platform = platforms[33L],
            },
            new()
            {
                GameId = 1029L,
                PlatformId = 4L,
                Game = games[1029L],
                Platform = platforms[4L],
            },
            new()
            {
                GameId = 1029L,
                PlatformId = 5L,
                Game = games[1029L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1029L,
                PlatformId = 41L,
                Game = games[1029L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1029L,
                PlatformId = 416L,
                Game = games[1029L],
                Platform = platforms[416L],
            },
            new()
            {
                GameId = 1030L,
                PlatformId = 4L,
                Game = games[1030L],
                Platform = platforms[4L],
            },
            new()
            {
                GameId = 1030L,
                PlatformId = 5L,
                Game = games[1030L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1030L,
                PlatformId = 41L,
                Game = games[1030L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1030L,
                PlatformId = 416L,
                Game = games[1030L],
                Platform = platforms[416L],
            },
            new()
            {
                GameId = 1032L,
                PlatformId = 22L,
                Game = games[1032L],
                Platform = platforms[22L],
            },
            new()
            {
                GameId = 1032L,
                PlatformId = 37L,
                Game = games[1032L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1033L,
                PlatformId = 21L,
                Game = games[1033L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 1034L,
                PlatformId = 21L,
                Game = games[1034L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 1035L,
                PlatformId = 24L,
                Game = games[1035L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 1035L,
                PlatformId = 37L,
                Game = games[1035L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1035L,
                PlatformId = 41L,
                Game = games[1035L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1036L,
                PlatformId = 21L,
                Game = games[1036L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 1037L,
                PlatformId = 20L,
                Game = games[1037L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 1037L,
                PlatformId = 41L,
                Game = games[1037L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1038L,
                PlatformId = 20L,
                Game = games[1038L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 1038L,
                PlatformId = 41L,
                Game = games[1038L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 1039L,
                PlatformId = 37L,
                Game = games[1039L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1041L,
                PlatformId = 22L,
                Game = games[1041L],
                Platform = platforms[22L],
            },
            new()
            {
                GameId = 1041L,
                PlatformId = 37L,
                Game = games[1041L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 1319L,
                PlatformId = 12L,
                Game = games[1319L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 1626L,
                PlatformId = 4L,
                Game = games[1626L],
                Platform = platforms[4L],
            },
            new()
            {
                GameId = 1626L,
                PlatformId = 5L,
                Game = games[1626L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 1627L,
                PlatformId = 21L,
                Game = games[1627L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 1628L,
                PlatformId = 5L,
                Game = games[1628L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 2172L,
                PlatformId = 41L,
                Game = games[2172L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 2276L,
                PlatformId = 41L,
                Game = games[2276L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 2350L,
                PlatformId = 41L,
                Game = games[2350L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 2350L,
                PlatformId = 130L,
                Game = games[2350L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 2909L,
                PlatformId = 37L,
                Game = games[2909L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 4746L,
                PlatformId = 5L,
                Game = games[4746L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 4973L,
                PlatformId = 5L,
                Game = games[4973L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 5314L,
                PlatformId = 41L,
                Game = games[5314L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 6401L,
                PlatformId = 41L,
                Game = games[6401L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 6402L,
                PlatformId = 41L,
                Game = games[6402L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 7346L,
                PlatformId = 41L,
                Game = games[7346L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 7346L,
                PlatformId = 130L,
                Game = games[7346L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 8532L,
                PlatformId = 117L,
                Game = games[8532L],
                Platform = platforms[117L],
            },
            new()
            {
                GameId = 8533L,
                PlatformId = 117L,
                Game = games[8533L],
                Platform = platforms[117L],
            },
            new()
            {
                GameId = 8534L,
                PlatformId = 117L,
                Game = games[8534L],
                Platform = platforms[117L],
            },
            new()
            {
                GameId = 8593L,
                PlatformId = 37L,
                Game = games[8593L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 9602L,
                PlatformId = 41L,
                Game = games[9602L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 9621L,
                PlatformId = 37L,
                Game = games[9621L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 11156L,
                PlatformId = 6L,
                Game = games[11156L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 11156L,
                PlatformId = 48L,
                Game = games[11156L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 11156L,
                PlatformId = 167L,
                Game = games[11156L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 11186L,
                PlatformId = 6L,
                Game = games[11186L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 11186L,
                PlatformId = 49L,
                Game = games[11186L],
                Platform = platforms[49L],
            },
            new()
            {
                GameId = 11193L,
                PlatformId = 37L,
                Game = games[11193L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 11194L,
                PlatformId = 37L,
                Game = games[11194L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 11607L,
                PlatformId = 20L,
                Game = games[11607L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 18017L,
                PlatformId = 41L,
                Game = games[18017L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 18066L,
                PlatformId = 24L,
                Game = games[18066L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 20054L,
                PlatformId = 37L,
                Game = games[20054L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 22141L,
                PlatformId = 41L,
                Game = games[22141L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 23825L,
                PlatformId = 41L,
                Game = games[23825L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 25840L,
                PlatformId = 20L,
                Game = games[25840L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 26764L,
                PlatformId = 130L,
                Game = games[26764L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 37083L,
                PlatformId = 6L,
                Game = games[37083L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 37083L,
                PlatformId = 48L,
                Game = games[37083L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 38319L,
                PlatformId = 306L,
                Game = games[38319L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 41825L,
                PlatformId = 41L,
                Game = games[41825L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 41825L,
                PlatformId = 130L,
                Game = games[41825L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 41826L,
                PlatformId = 41L,
                Game = games[41826L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 41826L,
                PlatformId = 130L,
                Game = games[41826L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 41826L,
                PlatformId = 508L,
                Game = games[41826L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 41829L,
                PlatformId = 41L,
                Game = games[41829L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 41829L,
                PlatformId = 130L,
                Game = games[41829L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 41829L,
                PlatformId = 508L,
                Game = games[41829L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 42308L,
                PlatformId = 306L,
                Game = games[42308L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 42920L,
                PlatformId = 48L,
                Game = games[42920L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 44030L,
                PlatformId = 37L,
                Game = games[44030L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 45133L,
                PlatformId = 41L,
                Game = games[45133L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 45135L,
                PlatformId = 41L,
                Game = games[45135L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 45136L,
                PlatformId = 5L,
                Game = games[45136L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 45137L,
                PlatformId = 130L,
                Game = games[45137L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 45139L,
                PlatformId = 21L,
                Game = games[45139L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 45142L,
                PlatformId = 21L,
                Game = games[45142L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 45142L,
                PlatformId = 416L,
                Game = games[45142L],
                Platform = platforms[416L],
            },
            new()
            {
                GameId = 45143L,
                PlatformId = 37L,
                Game = games[45143L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 45143L,
                PlatformId = 159L,
                Game = games[45143L],
                Platform = platforms[159L],
            },
            new()
            {
                GameId = 47384L,
                PlatformId = 12L,
                Game = games[47384L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 47456L,
                PlatformId = 12L,
                Game = games[47456L],
                Platform = platforms[12L],
            },
            new()
            {
                GameId = 47604L,
                PlatformId = 37L,
                Game = games[47604L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 47828L,
                PlatformId = 20L,
                Game = games[47828L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 50598L,
                PlatformId = 130L,
                Game = games[50598L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 58782L,
                PlatformId = 37L,
                Game = games[58782L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 60431L,
                PlatformId = 41L,
                Game = games[60431L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 72870L,
                PlatformId = 6L,
                Game = games[72870L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 72870L,
                PlatformId = 48L,
                Game = games[72870L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 75295L,
                PlatformId = 130L,
                Game = games[75295L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 77336L,
                PlatformId = 24L,
                Game = games[77336L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 81147L,
                PlatformId = 130L,
                Game = games[81147L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 89904L,
                PlatformId = 37L,
                Game = games[89904L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 90101L,
                PlatformId = 130L,
                Game = games[90101L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 91680L,
                PlatformId = 21L,
                Game = games[91680L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 95040L,
                PlatformId = 41L,
                Game = games[95040L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95041L,
                PlatformId = 41L,
                Game = games[95041L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95042L,
                PlatformId = 41L,
                Game = games[95042L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95043L,
                PlatformId = 41L,
                Game = games[95043L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95044L,
                PlatformId = 41L,
                Game = games[95044L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95045L,
                PlatformId = 37L,
                Game = games[95045L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 95045L,
                PlatformId = 41L,
                Game = games[95045L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95046L,
                PlatformId = 37L,
                Game = games[95046L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 95046L,
                PlatformId = 41L,
                Game = games[95046L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95047L,
                PlatformId = 37L,
                Game = games[95047L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 95047L,
                PlatformId = 41L,
                Game = games[95047L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 95048L,
                PlatformId = 37L,
                Game = games[95048L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 95048L,
                PlatformId = 41L,
                Game = games[95048L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 100169L,
                PlatformId = 159L,
                Game = games[100169L],
                Platform = platforms[159L],
            },
            new()
            {
                GameId = 103291L,
                PlatformId = 6L,
                Game = games[103291L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 103291L,
                PlatformId = 49L,
                Game = games[103291L],
                Platform = platforms[49L],
            },
            new()
            {
                GameId = 103291L,
                PlatformId = 169L,
                Game = games[103291L],
                Platform = platforms[169L],
            },
            new()
            {
                GameId = 103292L,
                PlatformId = 6L,
                Game = games[103292L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 103292L,
                PlatformId = 49L,
                Game = games[103292L],
                Platform = platforms[49L],
            },
            new()
            {
                GameId = 103292L,
                PlatformId = 169L,
                Game = games[103292L],
                Platform = platforms[169L],
            },
            new()
            {
                GameId = 112874L,
                PlatformId = 6L,
                Game = games[112874L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 112874L,
                PlatformId = 48L,
                Game = games[112874L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 112874L,
                PlatformId = 167L,
                Game = games[112874L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 115284L,
                PlatformId = 130L,
                Game = games[115284L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 115284L,
                PlatformId = 508L,
                Game = games[115284L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 116419L,
                PlatformId = 130L,
                Game = games[116419L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 119388L,
                PlatformId = 130L,
                Game = games[119388L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 133935L,
                PlatformId = 41L,
                Game = games[133935L],
                Platform = platforms[41L],
            },
            new()
            {
                GameId = 134014L,
                PlatformId = 5L,
                Game = games[134014L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 134500L,
                PlatformId = 306L,
                Game = games[134500L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 135912L,
                PlatformId = 130L,
                Game = games[135912L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 136150L,
                PlatformId = 48L,
                Game = games[136150L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 136162L,
                PlatformId = 5L,
                Game = games[136162L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 136280L,
                PlatformId = 24L,
                Game = games[136280L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 136841L,
                PlatformId = 130L,
                Game = games[136841L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 138343L,
                PlatformId = 130L,
                Game = games[138343L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 141589L,
                PlatformId = 37L,
                Game = games[141589L],
                Platform = platforms[37L],
            },
            new()
            {
                GameId = 141706L,
                PlatformId = 130L,
                Game = games[141706L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 141742L,
                PlatformId = 130L,
                Game = games[141742L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 143614L,
                PlatformId = 130L,
                Game = games[143614L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 143618L,
                PlatformId = 130L,
                Game = games[143618L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 148431L,
                PlatformId = 58L,
                Game = games[148431L],
                Platform = platforms[58L],
            },
            new()
            {
                GameId = 148431L,
                PlatformId = 137L,
                Game = games[148431L],
                Platform = platforms[137L],
            },
            new()
            {
                GameId = 152361L,
                PlatformId = 307L,
                Game = games[152361L],
                Platform = platforms[307L],
            },
            new()
            {
                GameId = 152362L,
                PlatformId = 307L,
                Game = games[152362L],
                Platform = platforms[307L],
            },
            new()
            {
                GameId = 163572L,
                PlatformId = 24L,
                Game = games[163572L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 168557L,
                PlatformId = 48L,
                Game = games[168557L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 168557L,
                PlatformId = 167L,
                Game = games[168557L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 170034L,
                PlatformId = 48L,
                Game = games[170034L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 170034L,
                PlatformId = 167L,
                Game = games[170034L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 171276L,
                PlatformId = 48L,
                Game = games[171276L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 171276L,
                PlatformId = 167L,
                Game = games[171276L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 172501L,
                PlatformId = 411L,
                Game = games[172501L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 176087L,
                PlatformId = 21L,
                Game = games[176087L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 178049L,
                PlatformId = 306L,
                Game = games[178049L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178050L,
                PlatformId = 306L,
                Game = games[178050L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178051L,
                PlatformId = 306L,
                Game = games[178051L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178053L,
                PlatformId = 306L,
                Game = games[178053L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178054L,
                PlatformId = 306L,
                Game = games[178054L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178056L,
                PlatformId = 306L,
                Game = games[178056L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178058L,
                PlatformId = 306L,
                Game = games[178058L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178059L,
                PlatformId = 306L,
                Game = games[178059L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178061L,
                PlatformId = 306L,
                Game = games[178061L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178062L,
                PlatformId = 306L,
                Game = games[178062L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178063L,
                PlatformId = 306L,
                Game = games[178063L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 178064L,
                PlatformId = 306L,
                Game = games[178064L],
                Platform = platforms[306L],
            },
            new()
            {
                GameId = 184509L,
                PlatformId = 130L,
                Game = games[184509L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 184510L,
                PlatformId = 130L,
                Game = games[184510L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 186309L,
                PlatformId = 390L,
                Game = games[186309L],
                Platform = platforms[390L],
            },
            new()
            {
                GameId = 191440L,
                PlatformId = 34L,
                Game = games[191440L],
                Platform = platforms[34L],
            },
            new()
            {
                GameId = 199417L,
                PlatformId = 18L,
                Game = games[199417L],
                Platform = platforms[18L],
            },
            new()
            {
                GameId = 206370L,
                PlatformId = 411L,
                Game = games[206370L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 206539L,
                PlatformId = 5L,
                Game = games[206539L],
                Platform = platforms[5L],
            },
            new()
            {
                GameId = 208493L,
                PlatformId = 22L,
                Game = games[208493L],
                Platform = platforms[22L],
            },
            new()
            {
                GameId = 213363L,
                PlatformId = 377L,
                Game = games[213363L],
                Platform = platforms[377L],
            },
            new()
            {
                GameId = 213594L,
                PlatformId = 377L,
                Game = games[213594L],
                Platform = platforms[377L],
            },
            new()
            {
                GameId = 215237L,
                PlatformId = 20L,
                Game = games[215237L],
                Platform = platforms[20L],
            },
            new()
            {
                GameId = 216236L,
                PlatformId = 130L,
                Game = games[216236L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 227890L,
                PlatformId = 48L,
                Game = games[227890L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 227890L,
                PlatformId = 167L,
                Game = games[227890L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 227987L,
                PlatformId = 21L,
                Game = games[227987L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 228533L,
                PlatformId = 6L,
                Game = games[228533L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 228533L,
                PlatformId = 167L,
                Game = games[228533L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 228549L,
                PlatformId = 33L,
                Game = games[228549L],
                Platform = platforms[33L],
            },
            new()
            {
                GameId = 229416L,
                PlatformId = 24L,
                Game = games[229416L],
                Platform = platforms[24L],
            },
            new()
            {
                GameId = 232696L,
                PlatformId = 307L,
                Game = games[232696L],
                Platform = platforms[307L],
            },
            new()
            {
                GameId = 235330L,
                PlatformId = 130L,
                Game = games[235330L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 237289L,
                PlatformId = 130L,
                Game = games[237289L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 239007L,
                PlatformId = 21L,
                Game = games[239007L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 257514L,
                PlatformId = 58L,
                Game = games[257514L],
                Platform = platforms[58L],
            },
            new()
            {
                GameId = 268842L,
                PlatformId = 6L,
                Game = games[268842L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 268842L,
                PlatformId = 167L,
                Game = games[268842L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 293728L,
                PlatformId = 130L,
                Game = games[293728L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 298870L,
                PlatformId = 21L,
                Game = games[298870L],
                Platform = platforms[21L],
            },
            new()
            {
                GameId = 299862L,
                PlatformId = 130L,
                Game = games[299862L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 305003L,
                PlatformId = 6L,
                Game = games[305003L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 305003L,
                PlatformId = 48L,
                Game = games[305003L],
                Platform = platforms[48L],
            },
            new()
            {
                GameId = 305003L,
                PlatformId = 130L,
                Game = games[305003L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 305003L,
                PlatformId = 167L,
                Game = games[305003L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 305160L,
                PlatformId = 6L,
                Game = games[305160L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 305160L,
                PlatformId = 167L,
                Game = games[305160L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 305160L,
                PlatformId = 169L,
                Game = games[305160L],
                Platform = platforms[169L],
            },
            new()
            {
                GameId = 306149L,
                PlatformId = 130L,
                Game = games[306149L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 306149L,
                PlatformId = 508L,
                Game = games[306149L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 313619L,
                PlatformId = 130L,
                Game = games[313619L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 317103L,
                PlatformId = 6L,
                Game = games[317103L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 317103L,
                PlatformId = 167L,
                Game = games[317103L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 318793L,
                PlatformId = 6L,
                Game = games[318793L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 320682L,
                PlatformId = 6L,
                Game = games[320682L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 320682L,
                PlatformId = 130L,
                Game = games[320682L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 320682L,
                PlatformId = 167L,
                Game = games[320682L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 321589L,
                PlatformId = 130L,
                Game = games[321589L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 321589L,
                PlatformId = 167L,
                Game = games[321589L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 321589L,
                PlatformId = 169L,
                Game = games[321589L],
                Platform = platforms[169L],
            },
            new()
            {
                GameId = 322256L,
                PlatformId = 130L,
                Game = games[322256L],
                Platform = platforms[130L],
            },
            new()
            {
                GameId = 322256L,
                PlatformId = 167L,
                Game = games[322256L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 328591L,
                PlatformId = 82L,
                Game = games[328591L],
                Platform = platforms[82L],
            },
            new()
            {
                GameId = 328666L,
                PlatformId = 82L,
                Game = games[328666L],
                Platform = platforms[82L],
            },
            new()
            {
                GameId = 328861L,
                PlatformId = 411L,
                Game = games[328861L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328862L,
                PlatformId = 411L,
                Game = games[328862L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328863L,
                PlatformId = 411L,
                Game = games[328863L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328864L,
                PlatformId = 411L,
                Game = games[328864L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328865L,
                PlatformId = 411L,
                Game = games[328865L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328866L,
                PlatformId = 411L,
                Game = games[328866L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328867L,
                PlatformId = 411L,
                Game = games[328867L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328868L,
                PlatformId = 411L,
                Game = games[328868L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328869L,
                PlatformId = 411L,
                Game = games[328869L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328870L,
                PlatformId = 411L,
                Game = games[328870L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328871L,
                PlatformId = 411L,
                Game = games[328871L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328872L,
                PlatformId = 411L,
                Game = games[328872L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328873L,
                PlatformId = 411L,
                Game = games[328873L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328874L,
                PlatformId = 411L,
                Game = games[328874L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328875L,
                PlatformId = 411L,
                Game = games[328875L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328876L,
                PlatformId = 411L,
                Game = games[328876L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328877L,
                PlatformId = 411L,
                Game = games[328877L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 328949L,
                PlatformId = 411L,
                Game = games[328949L],
                Platform = platforms[411L],
            },
            new()
            {
                GameId = 338072L,
                PlatformId = 508L,
                Game = games[338072L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 338073L,
                PlatformId = 508L,
                Game = games[338073L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 338085L,
                PlatformId = 508L,
                Game = games[338085L],
                Platform = platforms[508L],
            },
            new()
            {
                GameId = 342721L,
                PlatformId = 6L,
                Game = games[342721L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 342721L,
                PlatformId = 167L,
                Game = games[342721L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 342721L,
                PlatformId = 169L,
                Game = games[342721L],
                Platform = platforms[169L],
            },
            new()
            {
                GameId = 350670L,
                PlatformId = 4L,
                Game = games[350670L],
                Platform = platforms[4L],
            },
            new()
            {
                GameId = 354416L,
                PlatformId = 510L,
                Game = games[354416L],
                Platform = platforms[510L],
            },
            new()
            {
                GameId = 354828L,
                PlatformId = 510L,
                Game = games[354828L],
                Platform = platforms[510L],
            },
            new()
            {
                GameId = 354829L,
                PlatformId = 510L,
                Game = games[354829L],
                Platform = platforms[510L],
            },
            new()
            {
                GameId = 360934L,
                PlatformId = 82L,
                Game = games[360934L],
                Platform = platforms[82L],
            },
            new()
            {
                GameId = 360935L,
                PlatformId = 82L,
                Game = games[360935L],
                Platform = platforms[82L],
            },
            new()
            {
                GameId = 364442L,
                PlatformId = 510L,
                Game = games[364442L],
                Platform = platforms[510L],
            },
            new()
            {
                GameId = 377555L,
                PlatformId = 6L,
                Game = games[377555L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 377555L,
                PlatformId = 34L,
                Game = games[377555L],
                Platform = platforms[34L],
            },
            new()
            {
                GameId = 377555L,
                PlatformId = 39L,
                Game = games[377555L],
                Platform = platforms[39L],
            },
            new()
            {
                GameId = 388464L,
                PlatformId = 6L,
                Game = games[388464L],
                Platform = platforms[6L],
            },
            new()
            {
                GameId = 388464L,
                PlatformId = 167L,
                Game = games[388464L],
                Platform = platforms[167L],
            },
            new()
            {
                GameId = 405460L,
                PlatformId = 508L,
                Game = games[405460L],
                Platform = platforms[508L],
            },
        ];
    }
}
