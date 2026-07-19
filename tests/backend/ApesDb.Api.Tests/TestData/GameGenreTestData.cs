using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameGenreTestData
{
    public static GameGenre[] Create(IReadOnlyDictionary<long, Game> games, IReadOnlyDictionary<long, Genre> genres)
    {
        return
        [
            new()
            {
                GameId = 492L,
                GenreId = 5L,
                Game = games[492L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 534L,
                GenreId = 9L,
                Game = games[534L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 534L,
                GenreId = 31L,
                Game = games[534L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 547L,
                GenreId = 5L,
                Game = games[547L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 548L,
                GenreId = 5L,
                Game = games[548L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 1022L,
                GenreId = 31L,
                Game = games[1022L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1025L,
                GenreId = 8L,
                Game = games[1025L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 1025L,
                GenreId = 12L,
                Game = games[1025L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 1025L,
                GenreId = 31L,
                Game = games[1025L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1026L,
                GenreId = 9L,
                Game = games[1026L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1026L,
                GenreId = 31L,
                Game = games[1026L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1027L,
                GenreId = 9L,
                Game = games[1027L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1027L,
                GenreId = 31L,
                Game = games[1027L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1028L,
                GenreId = 9L,
                Game = games[1028L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1028L,
                GenreId = 31L,
                Game = games[1028L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1029L,
                GenreId = 9L,
                Game = games[1029L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1029L,
                GenreId = 31L,
                Game = games[1029L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1030L,
                GenreId = 9L,
                Game = games[1030L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1030L,
                GenreId = 31L,
                Game = games[1030L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1032L,
                GenreId = 9L,
                Game = games[1032L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1032L,
                GenreId = 31L,
                Game = games[1032L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1033L,
                GenreId = 9L,
                Game = games[1033L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1033L,
                GenreId = 31L,
                Game = games[1033L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1034L,
                GenreId = 9L,
                Game = games[1034L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1034L,
                GenreId = 31L,
                Game = games[1034L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1035L,
                GenreId = 9L,
                Game = games[1035L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1035L,
                GenreId = 31L,
                Game = games[1035L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1036L,
                GenreId = 9L,
                Game = games[1036L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1036L,
                GenreId = 31L,
                Game = games[1036L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1037L,
                GenreId = 9L,
                Game = games[1037L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1037L,
                GenreId = 31L,
                Game = games[1037L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1038L,
                GenreId = 9L,
                Game = games[1038L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1038L,
                GenreId = 31L,
                Game = games[1038L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1039L,
                GenreId = 9L,
                Game = games[1039L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1039L,
                GenreId = 31L,
                Game = games[1039L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1041L,
                GenreId = 9L,
                Game = games[1041L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 1041L,
                GenreId = 31L,
                Game = games[1041L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 1319L,
                GenreId = 5L,
                Game = games[1319L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 1626L,
                GenreId = 4L,
                Game = games[1626L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 1626L,
                GenreId = 8L,
                Game = games[1626L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 1627L,
                GenreId = 4L,
                Game = games[1627L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 1627L,
                GenreId = 8L,
                Game = games[1627L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 1628L,
                GenreId = 4L,
                Game = games[1628L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 1628L,
                GenreId = 8L,
                Game = games[1628L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 5L,
                Game = games[2172L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 8L,
                Game = games[2172L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 9L,
                Game = games[2172L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 10L,
                Game = games[2172L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 31L,
                Game = games[2172L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 2172L,
                GenreId = 33L,
                Game = games[2172L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 2276L,
                GenreId = 9L,
                Game = games[2276L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 2276L,
                GenreId = 31L,
                Game = games[2276L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 2350L,
                GenreId = 10L,
                Game = games[2350L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 2350L,
                GenreId = 33L,
                Game = games[2350L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 2909L,
                GenreId = 9L,
                Game = games[2909L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 2909L,
                GenreId = 31L,
                Game = games[2909L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 4746L,
                GenreId = 31L,
                Game = games[4746L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 4973L,
                GenreId = 5L,
                Game = games[4973L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 5314L,
                GenreId = 15L,
                Game = games[5314L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 5314L,
                GenreId = 25L,
                Game = games[5314L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 8L,
                Game = games[6401L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 9L,
                Game = games[6401L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 10L,
                Game = games[6401L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 14L,
                Game = games[6401L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 31L,
                Game = games[6401L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 6401L,
                GenreId = 33L,
                Game = games[6401L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 4L,
                Game = games[6402L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 7L,
                Game = games[6402L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 8L,
                Game = games[6402L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 10L,
                Game = games[6402L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 14L,
                Game = games[6402L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 31L,
                Game = games[6402L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 6402L,
                GenreId = 33L,
                Game = games[6402L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 7346L,
                GenreId = 9L,
                Game = games[7346L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 7346L,
                GenreId = 31L,
                Game = games[7346L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 8532L,
                GenreId = 31L,
                Game = games[8532L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 8533L,
                GenreId = 9L,
                Game = games[8533L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 8533L,
                GenreId = 31L,
                Game = games[8533L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 8534L,
                GenreId = 31L,
                Game = games[8534L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 8593L,
                GenreId = 9L,
                Game = games[8593L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 8593L,
                GenreId = 31L,
                Game = games[8593L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 9602L,
                GenreId = 4L,
                Game = games[9602L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 9602L,
                GenreId = 8L,
                Game = games[9602L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 9621L,
                GenreId = 4L,
                Game = games[9621L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 9621L,
                GenreId = 8L,
                Game = games[9621L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 11156L,
                GenreId = 5L,
                Game = games[11156L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 11156L,
                GenreId = 12L,
                Game = games[11156L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 11156L,
                GenreId = 31L,
                Game = games[11156L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 11186L,
                GenreId = 5L,
                Game = games[11186L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 11193L,
                GenreId = 15L,
                Game = games[11193L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 11193L,
                GenreId = 25L,
                Game = games[11193L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 11194L,
                GenreId = 9L,
                Game = games[11194L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 11194L,
                GenreId = 31L,
                Game = games[11194L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 11607L,
                GenreId = 31L,
                Game = games[11607L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 18017L,
                GenreId = 9L,
                Game = games[18017L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 18017L,
                GenreId = 31L,
                Game = games[18017L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 18066L,
                GenreId = 31L,
                Game = games[18066L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 20054L,
                GenreId = 8L,
                Game = games[20054L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 20054L,
                GenreId = 33L,
                Game = games[20054L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 22141L,
                GenreId = 10L,
                Game = games[22141L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 23825L,
                GenreId = 15L,
                Game = games[23825L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 23825L,
                GenreId = 25L,
                Game = games[23825L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 23825L,
                GenreId = 31L,
                Game = games[23825L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 25840L,
                GenreId = 2L,
                Game = games[25840L],
                Genre = genres[2L],
            },
            new()
            {
                GameId = 25840L,
                GenreId = 31L,
                Game = games[25840L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 26764L,
                GenreId = 10L,
                Game = games[26764L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 26764L,
                GenreId = 33L,
                Game = games[26764L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 37083L,
                GenreId = 12L,
                Game = games[37083L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 37083L,
                GenreId = 31L,
                Game = games[37083L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 38319L,
                GenreId = 9L,
                Game = games[38319L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 38319L,
                GenreId = 31L,
                Game = games[38319L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 41825L,
                GenreId = 9L,
                Game = games[41825L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 41825L,
                GenreId = 31L,
                Game = games[41825L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 41826L,
                GenreId = 9L,
                Game = games[41826L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 41826L,
                GenreId = 31L,
                Game = games[41826L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 41829L,
                GenreId = 12L,
                Game = games[41829L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 41829L,
                GenreId = 31L,
                Game = games[41829L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 42308L,
                GenreId = 9L,
                Game = games[42308L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 42308L,
                GenreId = 31L,
                Game = games[42308L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 42920L,
                GenreId = 5L,
                Game = games[42920L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 42920L,
                GenreId = 12L,
                Game = games[42920L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 44030L,
                GenreId = 9L,
                Game = games[44030L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 44030L,
                GenreId = 12L,
                Game = games[44030L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 44030L,
                GenreId = 31L,
                Game = games[44030L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45133L,
                GenreId = 31L,
                Game = games[45133L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45135L,
                GenreId = 31L,
                Game = games[45135L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45136L,
                GenreId = 31L,
                Game = games[45136L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45137L,
                GenreId = 12L,
                Game = games[45137L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 45137L,
                GenreId = 14L,
                Game = games[45137L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 45137L,
                GenreId = 31L,
                Game = games[45137L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45139L,
                GenreId = 8L,
                Game = games[45139L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 45139L,
                GenreId = 9L,
                Game = games[45139L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 45139L,
                GenreId = 12L,
                Game = games[45139L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 45139L,
                GenreId = 31L,
                Game = games[45139L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45142L,
                GenreId = 9L,
                Game = games[45142L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 45142L,
                GenreId = 31L,
                Game = games[45142L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 45143L,
                GenreId = 9L,
                Game = games[45143L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 45143L,
                GenreId = 31L,
                Game = games[45143L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 47384L,
                GenreId = 5L,
                Game = games[47384L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 47456L,
                GenreId = 5L,
                Game = games[47456L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 47604L,
                GenreId = 15L,
                Game = games[47604L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 47604L,
                GenreId = 25L,
                Game = games[47604L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 47604L,
                GenreId = 31L,
                Game = games[47604L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 47828L,
                GenreId = 8L,
                Game = games[47828L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 47828L,
                GenreId = 33L,
                Game = games[47828L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 50598L,
                GenreId = 12L,
                Game = games[50598L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 50598L,
                GenreId = 14L,
                Game = games[50598L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 50598L,
                GenreId = 31L,
                Game = games[50598L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 58782L,
                GenreId = 9L,
                Game = games[58782L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 72870L,
                GenreId = 5L,
                Game = games[72870L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 72870L,
                GenreId = 12L,
                Game = games[72870L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 72870L,
                GenreId = 31L,
                Game = games[72870L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 75295L,
                GenreId = 12L,
                Game = games[75295L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 75295L,
                GenreId = 14L,
                Game = games[75295L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 75295L,
                GenreId = 31L,
                Game = games[75295L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 77336L,
                GenreId = 9L,
                Game = games[77336L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 77336L,
                GenreId = 31L,
                Game = games[77336L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 81147L,
                GenreId = 15L,
                Game = games[81147L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 81147L,
                GenreId = 25L,
                Game = games[81147L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 89904L,
                GenreId = 12L,
                Game = games[89904L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 89904L,
                GenreId = 31L,
                Game = games[89904L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 90101L,
                GenreId = 4L,
                Game = games[90101L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 90101L,
                GenreId = 8L,
                Game = games[90101L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 91680L,
                GenreId = 31L,
                Game = games[91680L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 91680L,
                GenreId = 33L,
                Game = games[91680L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 95040L,
                GenreId = 15L,
                Game = games[95040L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95040L,
                GenreId = 25L,
                Game = games[95040L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95040L,
                GenreId = 31L,
                Game = games[95040L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95041L,
                GenreId = 15L,
                Game = games[95041L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95041L,
                GenreId = 25L,
                Game = games[95041L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95041L,
                GenreId = 31L,
                Game = games[95041L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95042L,
                GenreId = 15L,
                Game = games[95042L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95042L,
                GenreId = 25L,
                Game = games[95042L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95042L,
                GenreId = 31L,
                Game = games[95042L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95043L,
                GenreId = 15L,
                Game = games[95043L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95043L,
                GenreId = 25L,
                Game = games[95043L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95043L,
                GenreId = 31L,
                Game = games[95043L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95044L,
                GenreId = 15L,
                Game = games[95044L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95044L,
                GenreId = 25L,
                Game = games[95044L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95044L,
                GenreId = 31L,
                Game = games[95044L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95045L,
                GenreId = 15L,
                Game = games[95045L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95045L,
                GenreId = 25L,
                Game = games[95045L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95045L,
                GenreId = 31L,
                Game = games[95045L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95046L,
                GenreId = 15L,
                Game = games[95046L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95046L,
                GenreId = 25L,
                Game = games[95046L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95046L,
                GenreId = 31L,
                Game = games[95046L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95047L,
                GenreId = 15L,
                Game = games[95047L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95047L,
                GenreId = 25L,
                Game = games[95047L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95047L,
                GenreId = 31L,
                Game = games[95047L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 95048L,
                GenreId = 15L,
                Game = games[95048L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 95048L,
                GenreId = 25L,
                Game = games[95048L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 95048L,
                GenreId = 31L,
                Game = games[95048L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 103291L,
                GenreId = 15L,
                Game = games[103291L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 103291L,
                GenreId = 16L,
                Game = games[103291L],
                Genre = genres[16L],
            },
            new()
            {
                GameId = 103291L,
                GenreId = 24L,
                Game = games[103291L],
                Genre = genres[24L],
            },
            new()
            {
                GameId = 103292L,
                GenreId = 5L,
                Game = games[103292L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 103292L,
                GenreId = 31L,
                Game = games[103292L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 112874L,
                GenreId = 12L,
                Game = games[112874L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 112874L,
                GenreId = 31L,
                Game = games[112874L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 115284L,
                GenreId = 9L,
                Game = games[115284L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 115284L,
                GenreId = 31L,
                Game = games[115284L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 116419L,
                GenreId = 7L,
                Game = games[116419L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 116419L,
                GenreId = 31L,
                Game = games[116419L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 119388L,
                GenreId = 9L,
                Game = games[119388L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 119388L,
                GenreId = 31L,
                Game = games[119388L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 133935L,
                GenreId = 8L,
                Game = games[133935L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 133935L,
                GenreId = 31L,
                Game = games[133935L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 134014L,
                GenreId = 9L,
                Game = games[134014L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 134014L,
                GenreId = 31L,
                Game = games[134014L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 134500L,
                GenreId = 12L,
                Game = games[134500L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 134500L,
                GenreId = 31L,
                Game = games[134500L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 135912L,
                GenreId = 7L,
                Game = games[135912L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 135912L,
                GenreId = 31L,
                Game = games[135912L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 136150L,
                GenreId = 5L,
                Game = games[136150L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 136150L,
                GenreId = 12L,
                Game = games[136150L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 136162L,
                GenreId = 31L,
                Game = games[136162L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 136280L,
                GenreId = 12L,
                Game = games[136280L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 136280L,
                GenreId = 31L,
                Game = games[136280L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 136841L,
                GenreId = 9L,
                Game = games[136841L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 136841L,
                GenreId = 12L,
                Game = games[136841L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 136841L,
                GenreId = 31L,
                Game = games[136841L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 138343L,
                GenreId = 15L,
                Game = games[138343L],
                Genre = genres[15L],
            },
            new()
            {
                GameId = 138343L,
                GenreId = 25L,
                Game = games[138343L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 141589L,
                GenreId = 31L,
                Game = games[141589L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 141706L,
                GenreId = 7L,
                Game = games[141706L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 141706L,
                GenreId = 31L,
                Game = games[141706L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 141742L,
                GenreId = 7L,
                Game = games[141742L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 141742L,
                GenreId = 31L,
                Game = games[141742L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 143614L,
                GenreId = 9L,
                Game = games[143614L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 143614L,
                GenreId = 31L,
                Game = games[143614L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 143618L,
                GenreId = 4L,
                Game = games[143618L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 143618L,
                GenreId = 25L,
                Game = games[143618L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 143618L,
                GenreId = 31L,
                Game = games[143618L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 148431L,
                GenreId = 9L,
                Game = games[148431L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 152361L,
                GenreId = 31L,
                Game = games[152361L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 152361L,
                GenreId = 33L,
                Game = games[152361L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 152362L,
                GenreId = 31L,
                Game = games[152362L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 163572L,
                GenreId = 9L,
                Game = games[163572L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 163572L,
                GenreId = 31L,
                Game = games[163572L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 168557L,
                GenreId = 12L,
                Game = games[168557L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 168557L,
                GenreId = 31L,
                Game = games[168557L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 170034L,
                GenreId = 12L,
                Game = games[170034L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 170034L,
                GenreId = 31L,
                Game = games[170034L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 171276L,
                GenreId = 12L,
                Game = games[171276L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 171276L,
                GenreId = 31L,
                Game = games[171276L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 172501L,
                GenreId = 31L,
                Game = games[172501L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 172501L,
                GenreId = 33L,
                Game = games[172501L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 176087L,
                GenreId = 9L,
                Game = games[176087L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 176087L,
                GenreId = 31L,
                Game = games[176087L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178049L,
                GenreId = 31L,
                Game = games[178049L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178050L,
                GenreId = 31L,
                Game = games[178050L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178051L,
                GenreId = 31L,
                Game = games[178051L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178053L,
                GenreId = 31L,
                Game = games[178053L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178054L,
                GenreId = 12L,
                Game = games[178054L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 178054L,
                GenreId = 31L,
                Game = games[178054L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178056L,
                GenreId = 12L,
                Game = games[178056L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 178056L,
                GenreId = 31L,
                Game = games[178056L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178058L,
                GenreId = 12L,
                Game = games[178058L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 178058L,
                GenreId = 31L,
                Game = games[178058L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178059L,
                GenreId = 12L,
                Game = games[178059L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 178059L,
                GenreId = 31L,
                Game = games[178059L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178061L,
                GenreId = 31L,
                Game = games[178061L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178062L,
                GenreId = 31L,
                Game = games[178062L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178063L,
                GenreId = 31L,
                Game = games[178063L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 178064L,
                GenreId = 31L,
                Game = games[178064L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 184509L,
                GenreId = 25L,
                Game = games[184509L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 184509L,
                GenreId = 31L,
                Game = games[184509L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 184510L,
                GenreId = 25L,
                Game = games[184510L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 184510L,
                GenreId = 31L,
                Game = games[184510L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 186309L,
                GenreId = 31L,
                Game = games[186309L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 191440L,
                GenreId = 31L,
                Game = games[191440L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 199417L,
                GenreId = 31L,
                Game = games[199417L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 206370L,
                GenreId = 31L,
                Game = games[206370L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 206370L,
                GenreId = 33L,
                Game = games[206370L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 208493L,
                GenreId = 12L,
                Game = games[208493L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 208493L,
                GenreId = 31L,
                Game = games[208493L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 4L,
                Game = games[213363L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 8L,
                Game = games[213363L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 9L,
                Game = games[213363L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 10L,
                Game = games[213363L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 12L,
                Game = games[213363L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 14L,
                Game = games[213363L],
                Genre = genres[14L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 31L,
                Game = games[213363L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 213363L,
                GenreId = 33L,
                Game = games[213363L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 4L,
                Game = games[213594L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 5L,
                Game = games[213594L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 8L,
                Game = games[213594L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 9L,
                Game = games[213594L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 10L,
                Game = games[213594L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 213594L,
                GenreId = 12L,
                Game = games[213594L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 215237L,
                GenreId = 31L,
                Game = games[215237L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 216236L,
                GenreId = 12L,
                Game = games[216236L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 216236L,
                GenreId = 31L,
                Game = games[216236L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 227890L,
                GenreId = 12L,
                Game = games[227890L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 227890L,
                GenreId = 31L,
                Game = games[227890L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 227987L,
                GenreId = 4L,
                Game = games[227987L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 228533L,
                GenreId = 12L,
                Game = games[228533L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 228533L,
                GenreId = 31L,
                Game = games[228533L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 229416L,
                GenreId = 9L,
                Game = games[229416L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 229416L,
                GenreId = 31L,
                Game = games[229416L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 232696L,
                GenreId = 33L,
                Game = games[232696L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 235330L,
                GenreId = 7L,
                Game = games[235330L],
                Genre = genres[7L],
            },
            new()
            {
                GameId = 235330L,
                GenreId = 31L,
                Game = games[235330L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 237289L,
                GenreId = 12L,
                Game = games[237289L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 237289L,
                GenreId = 31L,
                Game = games[237289L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 239007L,
                GenreId = 9L,
                Game = games[239007L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 239007L,
                GenreId = 12L,
                Game = games[239007L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 239007L,
                GenreId = 31L,
                Game = games[239007L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 257514L,
                GenreId = 31L,
                Game = games[257514L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 268842L,
                GenreId = 12L,
                Game = games[268842L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 268842L,
                GenreId = 31L,
                Game = games[268842L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 293728L,
                GenreId = 25L,
                Game = games[293728L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 298870L,
                GenreId = 4L,
                Game = games[298870L],
                Genre = genres[4L],
            },
            new()
            {
                GameId = 298870L,
                GenreId = 31L,
                Game = games[298870L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 298870L,
                GenreId = 33L,
                Game = games[298870L],
                Genre = genres[33L],
            },
            new()
            {
                GameId = 299862L,
                GenreId = 8L,
                Game = games[299862L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 299862L,
                GenreId = 10L,
                Game = games[299862L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 305003L,
                GenreId = 9L,
                Game = games[305003L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 305003L,
                GenreId = 31L,
                Game = games[305003L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 305160L,
                GenreId = 5L,
                Game = games[305160L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 305160L,
                GenreId = 31L,
                Game = games[305160L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 306149L,
                GenreId = 9L,
                Game = games[306149L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 306149L,
                GenreId = 31L,
                Game = games[306149L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 313619L,
                GenreId = 8L,
                Game = games[313619L],
                Genre = genres[8L],
            },
            new()
            {
                GameId = 313619L,
                GenreId = 10L,
                Game = games[313619L],
                Genre = genres[10L],
            },
            new()
            {
                GameId = 317103L,
                GenreId = 5L,
                Game = games[317103L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 317103L,
                GenreId = 12L,
                Game = games[317103L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 317103L,
                GenreId = 31L,
                Game = games[317103L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 320682L,
                GenreId = 9L,
                Game = games[320682L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 320682L,
                GenreId = 31L,
                Game = games[320682L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 321589L,
                GenreId = 9L,
                Game = games[321589L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 321589L,
                GenreId = 31L,
                Game = games[321589L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 322256L,
                GenreId = 9L,
                Game = games[322256L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 322256L,
                GenreId = 31L,
                Game = games[322256L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 328591L,
                GenreId = 13L,
                Game = games[328591L],
                Genre = genres[13L],
            },
            new()
            {
                GameId = 328666L,
                GenreId = 9L,
                Game = games[328666L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 328949L,
                GenreId = 35L,
                Game = games[328949L],
                Genre = genres[35L],
            },
            new()
            {
                GameId = 338072L,
                GenreId = 9L,
                Game = games[338072L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 338072L,
                GenreId = 31L,
                Game = games[338072L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 338073L,
                GenreId = 9L,
                Game = games[338073L],
                Genre = genres[9L],
            },
            new()
            {
                GameId = 338073L,
                GenreId = 31L,
                Game = games[338073L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 338085L,
                GenreId = 25L,
                Game = games[338085L],
                Genre = genres[25L],
            },
            new()
            {
                GameId = 338085L,
                GenreId = 31L,
                Game = games[338085L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 342721L,
                GenreId = 5L,
                Game = games[342721L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 360934L,
                GenreId = 26L,
                Game = games[360934L],
                Genre = genres[26L],
            },
            new()
            {
                GameId = 360935L,
                GenreId = 26L,
                Game = games[360935L],
                Genre = genres[26L],
            },
            new()
            {
                GameId = 377555L,
                GenreId = 12L,
                Game = games[377555L],
                Genre = genres[12L],
            },
            new()
            {
                GameId = 377555L,
                GenreId = 31L,
                Game = games[377555L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 388464L,
                GenreId = 5L,
                Game = games[388464L],
                Genre = genres[5L],
            },
            new()
            {
                GameId = 388464L,
                GenreId = 24L,
                Game = games[388464L],
                Genre = genres[24L],
            },
            new()
            {
                GameId = 388464L,
                GenreId = 31L,
                Game = games[388464L],
                Genre = genres[31L],
            },
            new()
            {
                GameId = 405460L,
                GenreId = 31L,
                Game = games[405460L],
                Genre = genres[31L],
            },
        ];
    }
}
