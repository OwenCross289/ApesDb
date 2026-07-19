using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameThemeTestData
{
    public static GameTheme[] Create(IReadOnlyDictionary<long, Game> games, IReadOnlyDictionary<long, Theme> themes)
    {
        return
        [
            new()
            {
                GameId = 492L,
                ThemeId = 1L,
                Game = games[492L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 492L,
                ThemeId = 18L,
                Game = games[492L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 492L,
                ThemeId = 19L,
                Game = games[492L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 534L,
                ThemeId = 1L,
                Game = games[534L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 534L,
                ThemeId = 17L,
                Game = games[534L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 534L,
                ThemeId = 22L,
                Game = games[534L],
                Theme = themes[22L],
            },
            new()
            {
                GameId = 534L,
                ThemeId = 38L,
                Game = games[534L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 547L,
                ThemeId = 1L,
                Game = games[547L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 547L,
                ThemeId = 18L,
                Game = games[547L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 547L,
                ThemeId = 19L,
                Game = games[547L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 548L,
                ThemeId = 1L,
                Game = games[548L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 548L,
                ThemeId = 18L,
                Game = games[548L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 1022L,
                ThemeId = 1L,
                Game = games[1022L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1022L,
                ThemeId = 17L,
                Game = games[1022L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1022L,
                ThemeId = 38L,
                Game = games[1022L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 1025L,
                ThemeId = 1L,
                Game = games[1025L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1025L,
                ThemeId = 17L,
                Game = games[1025L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1026L,
                ThemeId = 1L,
                Game = games[1026L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1026L,
                ThemeId = 17L,
                Game = games[1026L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1027L,
                ThemeId = 1L,
                Game = games[1027L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1027L,
                ThemeId = 17L,
                Game = games[1027L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1028L,
                ThemeId = 1L,
                Game = games[1028L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1028L,
                ThemeId = 17L,
                Game = games[1028L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1029L,
                ThemeId = 1L,
                Game = games[1029L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1029L,
                ThemeId = 17L,
                Game = games[1029L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1029L,
                ThemeId = 33L,
                Game = games[1029L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 1029L,
                ThemeId = 38L,
                Game = games[1029L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 1030L,
                ThemeId = 1L,
                Game = games[1030L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1030L,
                ThemeId = 17L,
                Game = games[1030L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1030L,
                ThemeId = 19L,
                Game = games[1030L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 1030L,
                ThemeId = 38L,
                Game = games[1030L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 1032L,
                ThemeId = 1L,
                Game = games[1032L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1032L,
                ThemeId = 17L,
                Game = games[1032L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1033L,
                ThemeId = 1L,
                Game = games[1033L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1033L,
                ThemeId = 17L,
                Game = games[1033L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1034L,
                ThemeId = 1L,
                Game = games[1034L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1034L,
                ThemeId = 17L,
                Game = games[1034L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1035L,
                ThemeId = 1L,
                Game = games[1035L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1035L,
                ThemeId = 17L,
                Game = games[1035L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1036L,
                ThemeId = 1L,
                Game = games[1036L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1036L,
                ThemeId = 17L,
                Game = games[1036L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1036L,
                ThemeId = 38L,
                Game = games[1036L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 1037L,
                ThemeId = 1L,
                Game = games[1037L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1037L,
                ThemeId = 17L,
                Game = games[1037L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1038L,
                ThemeId = 1L,
                Game = games[1038L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1038L,
                ThemeId = 17L,
                Game = games[1038L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1039L,
                ThemeId = 1L,
                Game = games[1039L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1039L,
                ThemeId = 17L,
                Game = games[1039L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 1041L,
                ThemeId = 1L,
                Game = games[1041L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1319L,
                ThemeId = 1L,
                Game = games[1319L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1319L,
                ThemeId = 18L,
                Game = games[1319L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 1626L,
                ThemeId = 1L,
                Game = games[1626L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1626L,
                ThemeId = 40L,
                Game = games[1626L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 1627L,
                ThemeId = 1L,
                Game = games[1627L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 1628L,
                ThemeId = 1L,
                Game = games[1628L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 2172L,
                ThemeId = 1L,
                Game = games[2172L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 2172L,
                ThemeId = 17L,
                Game = games[2172L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 2172L,
                ThemeId = 18L,
                Game = games[2172L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 2172L,
                ThemeId = 40L,
                Game = games[2172L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 2276L,
                ThemeId = 1L,
                Game = games[2276L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 2276L,
                ThemeId = 17L,
                Game = games[2276L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 2276L,
                ThemeId = 35L,
                Game = games[2276L],
                Theme = themes[35L],
            },
            new()
            {
                GameId = 2276L,
                ThemeId = 38L,
                Game = games[2276L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 2350L,
                ThemeId = 1L,
                Game = games[2350L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 2350L,
                ThemeId = 17L,
                Game = games[2350L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 2350L,
                ThemeId = 35L,
                Game = games[2350L],
                Theme = themes[35L],
            },
            new()
            {
                GameId = 2350L,
                ThemeId = 40L,
                Game = games[2350L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 2909L,
                ThemeId = 1L,
                Game = games[2909L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 2909L,
                ThemeId = 17L,
                Game = games[2909L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 2909L,
                ThemeId = 22L,
                Game = games[2909L],
                Theme = themes[22L],
            },
            new()
            {
                GameId = 2909L,
                ThemeId = 33L,
                Game = games[2909L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 2909L,
                ThemeId = 38L,
                Game = games[2909L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 4746L,
                ThemeId = 1L,
                Game = games[4746L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 4746L,
                ThemeId = 17L,
                Game = games[4746L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 4746L,
                ThemeId = 27L,
                Game = games[4746L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 4973L,
                ThemeId = 1L,
                Game = games[4973L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 4973L,
                ThemeId = 17L,
                Game = games[4973L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 5314L,
                ThemeId = 1L,
                Game = games[5314L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 6401L,
                ThemeId = 1L,
                Game = games[6401L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 6402L,
                ThemeId = 1L,
                Game = games[6402L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 6402L,
                ThemeId = 40L,
                Game = games[6402L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 7346L,
                ThemeId = 1L,
                Game = games[7346L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 7346L,
                ThemeId = 17L,
                Game = games[7346L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 7346L,
                ThemeId = 18L,
                Game = games[7346L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 7346L,
                ThemeId = 33L,
                Game = games[7346L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 7346L,
                ThemeId = 38L,
                Game = games[7346L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 8532L,
                ThemeId = 1L,
                Game = games[8532L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 8533L,
                ThemeId = 1L,
                Game = games[8533L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 8534L,
                ThemeId = 1L,
                Game = games[8534L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 8534L,
                ThemeId = 17L,
                Game = games[8534L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 8593L,
                ThemeId = 1L,
                Game = games[8593L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 8593L,
                ThemeId = 17L,
                Game = games[8593L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 9602L,
                ThemeId = 1L,
                Game = games[9602L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 9602L,
                ThemeId = 17L,
                Game = games[9602L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 9602L,
                ThemeId = 18L,
                Game = games[9602L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 9602L,
                ThemeId = 40L,
                Game = games[9602L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 9621L,
                ThemeId = 1L,
                Game = games[9621L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 9621L,
                ThemeId = 17L,
                Game = games[9621L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 9621L,
                ThemeId = 18L,
                Game = games[9621L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 9621L,
                ThemeId = 40L,
                Game = games[9621L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 11156L,
                ThemeId = 1L,
                Game = games[11156L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 11156L,
                ThemeId = 18L,
                Game = games[11156L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 11156L,
                ThemeId = 23L,
                Game = games[11156L],
                Theme = themes[23L],
            },
            new()
            {
                GameId = 11156L,
                ThemeId = 31L,
                Game = games[11156L],
                Theme = themes[31L],
            },
            new()
            {
                GameId = 11156L,
                ThemeId = 38L,
                Game = games[11156L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 11186L,
                ThemeId = 1L,
                Game = games[11186L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 11186L,
                ThemeId = 18L,
                Game = games[11186L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 11186L,
                ThemeId = 19L,
                Game = games[11186L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 11193L,
                ThemeId = 1L,
                Game = games[11193L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 11194L,
                ThemeId = 1L,
                Game = games[11194L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 11194L,
                ThemeId = 17L,
                Game = games[11194L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 11607L,
                ThemeId = 17L,
                Game = games[11607L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 11607L,
                ThemeId = 27L,
                Game = games[11607L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 18017L,
                ThemeId = 1L,
                Game = games[18017L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 18017L,
                ThemeId = 17L,
                Game = games[18017L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 18066L,
                ThemeId = 1L,
                Game = games[18066L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 18066L,
                ThemeId = 17L,
                Game = games[18066L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 20054L,
                ThemeId = 1L,
                Game = games[20054L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 20054L,
                ThemeId = 17L,
                Game = games[20054L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 20054L,
                ThemeId = 18L,
                Game = games[20054L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 22141L,
                ThemeId = 1L,
                Game = games[22141L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 23825L,
                ThemeId = 1L,
                Game = games[23825L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 23825L,
                ThemeId = 17L,
                Game = games[23825L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 25840L,
                ThemeId = 17L,
                Game = games[25840L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 25840L,
                ThemeId = 27L,
                Game = games[25840L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 25840L,
                ThemeId = 44L,
                Game = games[25840L],
                Theme = themes[44L],
            },
            new()
            {
                GameId = 26764L,
                ThemeId = 1L,
                Game = games[26764L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 26764L,
                ThemeId = 17L,
                Game = games[26764L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 26764L,
                ThemeId = 35L,
                Game = games[26764L],
                Theme = themes[35L],
            },
            new()
            {
                GameId = 26764L,
                ThemeId = 40L,
                Game = games[26764L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 37083L,
                ThemeId = 1L,
                Game = games[37083L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 37083L,
                ThemeId = 18L,
                Game = games[37083L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 37083L,
                ThemeId = 38L,
                Game = games[37083L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 38319L,
                ThemeId = 1L,
                Game = games[38319L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 38319L,
                ThemeId = 17L,
                Game = games[38319L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 41825L,
                ThemeId = 1L,
                Game = games[41825L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 41825L,
                ThemeId = 17L,
                Game = games[41825L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 41825L,
                ThemeId = 18L,
                Game = games[41825L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 41825L,
                ThemeId = 33L,
                Game = games[41825L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 41825L,
                ThemeId = 38L,
                Game = games[41825L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 41826L,
                ThemeId = 1L,
                Game = games[41826L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 41826L,
                ThemeId = 17L,
                Game = games[41826L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 41826L,
                ThemeId = 18L,
                Game = games[41826L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 41826L,
                ThemeId = 33L,
                Game = games[41826L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 41826L,
                ThemeId = 38L,
                Game = games[41826L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 41829L,
                ThemeId = 1L,
                Game = games[41829L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 41829L,
                ThemeId = 17L,
                Game = games[41829L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 42308L,
                ThemeId = 1L,
                Game = games[42308L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 42308L,
                ThemeId = 17L,
                Game = games[42308L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 42920L,
                ThemeId = 1L,
                Game = games[42920L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 42920L,
                ThemeId = 18L,
                Game = games[42920L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 42920L,
                ThemeId = 38L,
                Game = games[42920L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 44030L,
                ThemeId = 1L,
                Game = games[44030L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45133L,
                ThemeId = 1L,
                Game = games[45133L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45135L,
                ThemeId = 1L,
                Game = games[45135L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45136L,
                ThemeId = 1L,
                Game = games[45136L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45136L,
                ThemeId = 17L,
                Game = games[45136L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 45137L,
                ThemeId = 1L,
                Game = games[45137L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45137L,
                ThemeId = 17L,
                Game = games[45137L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 45137L,
                ThemeId = 38L,
                Game = games[45137L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 45139L,
                ThemeId = 1L,
                Game = games[45139L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45139L,
                ThemeId = 17L,
                Game = games[45139L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 45142L,
                ThemeId = 1L,
                Game = games[45142L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 45142L,
                ThemeId = 17L,
                Game = games[45142L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 45143L,
                ThemeId = 1L,
                Game = games[45143L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 47384L,
                ThemeId = 1L,
                Game = games[47384L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 47384L,
                ThemeId = 18L,
                Game = games[47384L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 47384L,
                ThemeId = 19L,
                Game = games[47384L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 47456L,
                ThemeId = 1L,
                Game = games[47456L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 47456L,
                ThemeId = 18L,
                Game = games[47456L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 47456L,
                ThemeId = 19L,
                Game = games[47456L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 47604L,
                ThemeId = 1L,
                Game = games[47604L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 47828L,
                ThemeId = 1L,
                Game = games[47828L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 50598L,
                ThemeId = 1L,
                Game = games[50598L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 50598L,
                ThemeId = 17L,
                Game = games[50598L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 50598L,
                ThemeId = 38L,
                Game = games[50598L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 60431L,
                ThemeId = 1L,
                Game = games[60431L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 72870L,
                ThemeId = 1L,
                Game = games[72870L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 72870L,
                ThemeId = 18L,
                Game = games[72870L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 72870L,
                ThemeId = 38L,
                Game = games[72870L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 75295L,
                ThemeId = 1L,
                Game = games[75295L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 75295L,
                ThemeId = 17L,
                Game = games[75295L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 75295L,
                ThemeId = 38L,
                Game = games[75295L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 77336L,
                ThemeId = 1L,
                Game = games[77336L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 77336L,
                ThemeId = 17L,
                Game = games[77336L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 81147L,
                ThemeId = 1L,
                Game = games[81147L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 81147L,
                ThemeId = 17L,
                Game = games[81147L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 89904L,
                ThemeId = 1L,
                Game = games[89904L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 89904L,
                ThemeId = 17L,
                Game = games[89904L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 90101L,
                ThemeId = 1L,
                Game = games[90101L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 90101L,
                ThemeId = 17L,
                Game = games[90101L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 90101L,
                ThemeId = 18L,
                Game = games[90101L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 90101L,
                ThemeId = 40L,
                Game = games[90101L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 91680L,
                ThemeId = 1L,
                Game = games[91680L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 91680L,
                ThemeId = 17L,
                Game = games[91680L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95040L,
                ThemeId = 1L,
                Game = games[95040L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95041L,
                ThemeId = 1L,
                Game = games[95041L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95041L,
                ThemeId = 17L,
                Game = games[95041L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95042L,
                ThemeId = 1L,
                Game = games[95042L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95042L,
                ThemeId = 17L,
                Game = games[95042L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95043L,
                ThemeId = 1L,
                Game = games[95043L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95043L,
                ThemeId = 17L,
                Game = games[95043L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95044L,
                ThemeId = 1L,
                Game = games[95044L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95044L,
                ThemeId = 17L,
                Game = games[95044L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95045L,
                ThemeId = 1L,
                Game = games[95045L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95045L,
                ThemeId = 17L,
                Game = games[95045L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95046L,
                ThemeId = 1L,
                Game = games[95046L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95046L,
                ThemeId = 17L,
                Game = games[95046L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95047L,
                ThemeId = 1L,
                Game = games[95047L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95047L,
                ThemeId = 17L,
                Game = games[95047L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 95048L,
                ThemeId = 1L,
                Game = games[95048L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 95048L,
                ThemeId = 17L,
                Game = games[95048L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 103291L,
                ThemeId = 1L,
                Game = games[103291L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 103291L,
                ThemeId = 39L,
                Game = games[103291L],
                Theme = themes[39L],
            },
            new()
            {
                GameId = 103292L,
                ThemeId = 1L,
                Game = games[103292L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 103292L,
                ThemeId = 18L,
                Game = games[103292L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 112874L,
                ThemeId = 1L,
                Game = games[112874L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 112874L,
                ThemeId = 18L,
                Game = games[112874L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 112874L,
                ThemeId = 38L,
                Game = games[112874L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 115284L,
                ThemeId = 1L,
                Game = games[115284L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 115284L,
                ThemeId = 17L,
                Game = games[115284L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 116419L,
                ThemeId = 1L,
                Game = games[116419L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 116419L,
                ThemeId = 17L,
                Game = games[116419L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 119388L,
                ThemeId = 1L,
                Game = games[119388L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 119388L,
                ThemeId = 17L,
                Game = games[119388L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 119388L,
                ThemeId = 18L,
                Game = games[119388L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 119388L,
                ThemeId = 33L,
                Game = games[119388L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 119388L,
                ThemeId = 38L,
                Game = games[119388L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 133935L,
                ThemeId = 1L,
                Game = games[133935L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 134014L,
                ThemeId = 1L,
                Game = games[134014L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 134014L,
                ThemeId = 17L,
                Game = games[134014L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 134500L,
                ThemeId = 1L,
                Game = games[134500L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 134500L,
                ThemeId = 17L,
                Game = games[134500L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 135912L,
                ThemeId = 1L,
                Game = games[135912L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 135912L,
                ThemeId = 17L,
                Game = games[135912L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 136150L,
                ThemeId = 1L,
                Game = games[136150L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 136150L,
                ThemeId = 18L,
                Game = games[136150L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 136150L,
                ThemeId = 23L,
                Game = games[136150L],
                Theme = themes[23L],
            },
            new()
            {
                GameId = 136150L,
                ThemeId = 38L,
                Game = games[136150L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 136162L,
                ThemeId = 1L,
                Game = games[136162L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 136162L,
                ThemeId = 17L,
                Game = games[136162L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 136280L,
                ThemeId = 1L,
                Game = games[136280L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 136280L,
                ThemeId = 17L,
                Game = games[136280L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 136841L,
                ThemeId = 1L,
                Game = games[136841L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 136841L,
                ThemeId = 17L,
                Game = games[136841L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 136841L,
                ThemeId = 38L,
                Game = games[136841L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 138343L,
                ThemeId = 1L,
                Game = games[138343L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 138343L,
                ThemeId = 17L,
                Game = games[138343L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 141589L,
                ThemeId = 17L,
                Game = games[141589L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 141706L,
                ThemeId = 1L,
                Game = games[141706L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 141706L,
                ThemeId = 17L,
                Game = games[141706L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 141742L,
                ThemeId = 1L,
                Game = games[141742L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 141742L,
                ThemeId = 17L,
                Game = games[141742L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 143614L,
                ThemeId = 1L,
                Game = games[143614L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 143614L,
                ThemeId = 17L,
                Game = games[143614L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 143618L,
                ThemeId = 1L,
                Game = games[143618L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 143618L,
                ThemeId = 17L,
                Game = games[143618L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 152361L,
                ThemeId = 17L,
                Game = games[152361L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 152362L,
                ThemeId = 17L,
                Game = games[152362L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 163572L,
                ThemeId = 1L,
                Game = games[163572L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 163572L,
                ThemeId = 17L,
                Game = games[163572L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 168557L,
                ThemeId = 1L,
                Game = games[168557L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 168557L,
                ThemeId = 18L,
                Game = games[168557L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 168557L,
                ThemeId = 38L,
                Game = games[168557L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 170034L,
                ThemeId = 1L,
                Game = games[170034L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 170034L,
                ThemeId = 18L,
                Game = games[170034L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 170034L,
                ThemeId = 38L,
                Game = games[170034L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 171276L,
                ThemeId = 1L,
                Game = games[171276L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 171276L,
                ThemeId = 18L,
                Game = games[171276L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 171276L,
                ThemeId = 38L,
                Game = games[171276L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 172501L,
                ThemeId = 1L,
                Game = games[172501L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 172501L,
                ThemeId = 17L,
                Game = games[172501L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 176087L,
                ThemeId = 1L,
                Game = games[176087L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 176087L,
                ThemeId = 17L,
                Game = games[176087L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178049L,
                ThemeId = 1L,
                Game = games[178049L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178049L,
                ThemeId = 17L,
                Game = games[178049L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178050L,
                ThemeId = 1L,
                Game = games[178050L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178050L,
                ThemeId = 17L,
                Game = games[178050L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178051L,
                ThemeId = 1L,
                Game = games[178051L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178051L,
                ThemeId = 17L,
                Game = games[178051L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178053L,
                ThemeId = 1L,
                Game = games[178053L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178053L,
                ThemeId = 17L,
                Game = games[178053L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178054L,
                ThemeId = 1L,
                Game = games[178054L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178054L,
                ThemeId = 17L,
                Game = games[178054L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178056L,
                ThemeId = 1L,
                Game = games[178056L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178056L,
                ThemeId = 17L,
                Game = games[178056L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178058L,
                ThemeId = 1L,
                Game = games[178058L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178058L,
                ThemeId = 17L,
                Game = games[178058L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178059L,
                ThemeId = 1L,
                Game = games[178059L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178059L,
                ThemeId = 17L,
                Game = games[178059L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178061L,
                ThemeId = 1L,
                Game = games[178061L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178061L,
                ThemeId = 17L,
                Game = games[178061L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178062L,
                ThemeId = 1L,
                Game = games[178062L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178062L,
                ThemeId = 17L,
                Game = games[178062L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178063L,
                ThemeId = 1L,
                Game = games[178063L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178063L,
                ThemeId = 17L,
                Game = games[178063L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 178064L,
                ThemeId = 1L,
                Game = games[178064L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 178064L,
                ThemeId = 17L,
                Game = games[178064L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 186309L,
                ThemeId = 1L,
                Game = games[186309L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 186309L,
                ThemeId = 18L,
                Game = games[186309L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 191440L,
                ThemeId = 1L,
                Game = games[191440L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 191440L,
                ThemeId = 17L,
                Game = games[191440L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 199417L,
                ThemeId = 1L,
                Game = games[199417L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 199417L,
                ThemeId = 17L,
                Game = games[199417L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 206370L,
                ThemeId = 1L,
                Game = games[206370L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 206370L,
                ThemeId = 17L,
                Game = games[206370L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 208493L,
                ThemeId = 1L,
                Game = games[208493L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 213363L,
                ThemeId = 1L,
                Game = games[213363L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 213363L,
                ThemeId = 17L,
                Game = games[213363L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 213363L,
                ThemeId = 18L,
                Game = games[213363L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 213594L,
                ThemeId = 1L,
                Game = games[213594L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 213594L,
                ThemeId = 17L,
                Game = games[213594L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 213594L,
                ThemeId = 18L,
                Game = games[213594L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 1L,
                Game = games[216236L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 17L,
                Game = games[216236L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 21L,
                Game = games[216236L],
                Theme = themes[21L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 31L,
                Game = games[216236L],
                Theme = themes[31L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 33L,
                Game = games[216236L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 216236L,
                ThemeId = 38L,
                Game = games[216236L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 227890L,
                ThemeId = 1L,
                Game = games[227890L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 227890L,
                ThemeId = 18L,
                Game = games[227890L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 227890L,
                ThemeId = 38L,
                Game = games[227890L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 227987L,
                ThemeId = 1L,
                Game = games[227987L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 227987L,
                ThemeId = 17L,
                Game = games[227987L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 228533L,
                ThemeId = 1L,
                Game = games[228533L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 228533L,
                ThemeId = 18L,
                Game = games[228533L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 228533L,
                ThemeId = 38L,
                Game = games[228533L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 229416L,
                ThemeId = 1L,
                Game = games[229416L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 229416L,
                ThemeId = 17L,
                Game = games[229416L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 235330L,
                ThemeId = 1L,
                Game = games[235330L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 235330L,
                ThemeId = 17L,
                Game = games[235330L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 237289L,
                ThemeId = 1L,
                Game = games[237289L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 237289L,
                ThemeId = 17L,
                Game = games[237289L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 237289L,
                ThemeId = 38L,
                Game = games[237289L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 239007L,
                ThemeId = 1L,
                Game = games[239007L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 239007L,
                ThemeId = 17L,
                Game = games[239007L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 257514L,
                ThemeId = 1L,
                Game = games[257514L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 257514L,
                ThemeId = 17L,
                Game = games[257514L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 268842L,
                ThemeId = 1L,
                Game = games[268842L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 268842L,
                ThemeId = 18L,
                Game = games[268842L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 268842L,
                ThemeId = 38L,
                Game = games[268842L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 298870L,
                ThemeId = 1L,
                Game = games[298870L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 298870L,
                ThemeId = 17L,
                Game = games[298870L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 299862L,
                ThemeId = 1L,
                Game = games[299862L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 299862L,
                ThemeId = 40L,
                Game = games[299862L],
                Theme = themes[40L],
            },
            new()
            {
                GameId = 305003L,
                ThemeId = 1L,
                Game = games[305003L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 305003L,
                ThemeId = 18L,
                Game = games[305003L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 305003L,
                ThemeId = 27L,
                Game = games[305003L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 305160L,
                ThemeId = 1L,
                Game = games[305160L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 306149L,
                ThemeId = 1L,
                Game = games[306149L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 306149L,
                ThemeId = 17L,
                Game = games[306149L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 317103L,
                ThemeId = 1L,
                Game = games[317103L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 317103L,
                ThemeId = 18L,
                Game = games[317103L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 317103L,
                ThemeId = 38L,
                Game = games[317103L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 320682L,
                ThemeId = 1L,
                Game = games[320682L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 320682L,
                ThemeId = 18L,
                Game = games[320682L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 320682L,
                ThemeId = 27L,
                Game = games[320682L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 321589L,
                ThemeId = 1L,
                Game = games[321589L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 321589L,
                ThemeId = 18L,
                Game = games[321589L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 321589L,
                ThemeId = 27L,
                Game = games[321589L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 322256L,
                ThemeId = 1L,
                Game = games[322256L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 322256L,
                ThemeId = 18L,
                Game = games[322256L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 322256L,
                ThemeId = 27L,
                Game = games[322256L],
                Theme = themes[27L],
            },
            new()
            {
                GameId = 328949L,
                ThemeId = 17L,
                Game = games[328949L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 338072L,
                ThemeId = 1L,
                Game = games[338072L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 338072L,
                ThemeId = 17L,
                Game = games[338072L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 338072L,
                ThemeId = 33L,
                Game = games[338072L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 338072L,
                ThemeId = 38L,
                Game = games[338072L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 338073L,
                ThemeId = 1L,
                Game = games[338073L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 338073L,
                ThemeId = 17L,
                Game = games[338073L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 338073L,
                ThemeId = 18L,
                Game = games[338073L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 338073L,
                ThemeId = 33L,
                Game = games[338073L],
                Theme = themes[33L],
            },
            new()
            {
                GameId = 338073L,
                ThemeId = 38L,
                Game = games[338073L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 338085L,
                ThemeId = 1L,
                Game = games[338085L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 338085L,
                ThemeId = 17L,
                Game = games[338085L],
                Theme = themes[17L],
            },
            new()
            {
                GameId = 342721L,
                ThemeId = 1L,
                Game = games[342721L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 342721L,
                ThemeId = 18L,
                Game = games[342721L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 342721L,
                ThemeId = 19L,
                Game = games[342721L],
                Theme = themes[19L],
            },
            new()
            {
                GameId = 377555L,
                ThemeId = 1L,
                Game = games[377555L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 377555L,
                ThemeId = 18L,
                Game = games[377555L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 377555L,
                ThemeId = 38L,
                Game = games[377555L],
                Theme = themes[38L],
            },
            new()
            {
                GameId = 388464L,
                ThemeId = 1L,
                Game = games[388464L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 388464L,
                ThemeId = 18L,
                Game = games[388464L],
                Theme = themes[18L],
            },
            new()
            {
                GameId = 405460L,
                ThemeId = 1L,
                Game = games[405460L],
                Theme = themes[1L],
            },
            new()
            {
                GameId = 405460L,
                ThemeId = 17L,
                Game = games[405460L],
                Theme = themes[17L],
            },
        ];
    }
}
