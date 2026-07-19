using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameCollectionTestData
{
    public static GameCollection[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, Collection> collections
    )
    {
        return
        [
            new()
            {
                GameId = 492L,
                CollectionId = 66L,
                Game = games[492L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 534L,
                CollectionId = 106L,
                Game = games[534L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 547L,
                CollectionId = 66L,
                Game = games[547L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 548L,
                CollectionId = 66L,
                Game = games[548L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 1022L,
                CollectionId = 106L,
                Game = games[1022L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1025L,
                CollectionId = 106L,
                Game = games[1025L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1026L,
                CollectionId = 106L,
                Game = games[1026L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1026L,
                CollectionId = 9849L,
                Game = games[1026L],
                Collection = collections[9849L],
            },
            new()
            {
                GameId = 1027L,
                CollectionId = 106L,
                Game = games[1027L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1028L,
                CollectionId = 106L,
                Game = games[1028L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1029L,
                CollectionId = 106L,
                Game = games[1029L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1029L,
                CollectionId = 8993L,
                Game = games[1029L],
                Collection = collections[8993L],
            },
            new()
            {
                GameId = 1030L,
                CollectionId = 106L,
                Game = games[1030L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1030L,
                CollectionId = 8993L,
                Game = games[1030L],
                Collection = collections[8993L],
            },
            new()
            {
                GameId = 1032L,
                CollectionId = 106L,
                Game = games[1032L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1033L,
                CollectionId = 106L,
                Game = games[1033L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1033L,
                CollectionId = 9026L,
                Game = games[1033L],
                Collection = collections[9026L],
            },
            new()
            {
                GameId = 1034L,
                CollectionId = 106L,
                Game = games[1034L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1034L,
                CollectionId = 9821L,
                Game = games[1034L],
                Collection = collections[9821L],
            },
            new()
            {
                GameId = 1035L,
                CollectionId = 106L,
                Game = games[1035L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1035L,
                CollectionId = 9821L,
                Game = games[1035L],
                Collection = collections[9821L],
            },
            new()
            {
                GameId = 1036L,
                CollectionId = 106L,
                Game = games[1036L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1037L,
                CollectionId = 106L,
                Game = games[1037L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1037L,
                CollectionId = 9026L,
                Game = games[1037L],
                Collection = collections[9026L],
            },
            new()
            {
                GameId = 1038L,
                CollectionId = 106L,
                Game = games[1038L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1038L,
                CollectionId = 9026L,
                Game = games[1038L],
                Collection = collections[9026L],
            },
            new()
            {
                GameId = 1039L,
                CollectionId = 106L,
                Game = games[1039L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1039L,
                CollectionId = 8993L,
                Game = games[1039L],
                Collection = collections[8993L],
            },
            new()
            {
                GameId = 1041L,
                CollectionId = 106L,
                Game = games[1041L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 1319L,
                CollectionId = 66L,
                Game = games[1319L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 1626L,
                CollectionId = 325L,
                Game = games[1626L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 1627L,
                CollectionId = 325L,
                Game = games[1627L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 1628L,
                CollectionId = 325L,
                Game = games[1628L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 2276L,
                CollectionId = 106L,
                Game = games[2276L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 2276L,
                CollectionId = 9026L,
                Game = games[2276L],
                Collection = collections[9026L],
            },
            new()
            {
                GameId = 2350L,
                CollectionId = 449L,
                Game = games[2350L],
                Collection = collections[449L],
            },
            new()
            {
                GameId = 2909L,
                CollectionId = 106L,
                Game = games[2909L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 2909L,
                CollectionId = 9849L,
                Game = games[2909L],
                Collection = collections[9849L],
            },
            new()
            {
                GameId = 4973L,
                CollectionId = 106L,
                Game = games[4973L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 5314L,
                CollectionId = 106L,
                Game = games[5314L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 5314L,
                CollectionId = 550L,
                Game = games[5314L],
                Collection = collections[550L],
            },
            new()
            {
                GameId = 5314L,
                CollectionId = 6463L,
                Game = games[5314L],
                Collection = collections[6463L],
            },
            new()
            {
                GameId = 6401L,
                CollectionId = 902L,
                Game = games[6401L],
                Collection = collections[902L],
            },
            new()
            {
                GameId = 6402L,
                CollectionId = 902L,
                Game = games[6402L],
                Collection = collections[902L],
            },
            new()
            {
                GameId = 7346L,
                CollectionId = 106L,
                Game = games[7346L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 7346L,
                CollectionId = 8988L,
                Game = games[7346L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 8532L,
                CollectionId = 106L,
                Game = games[8532L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 8533L,
                CollectionId = 106L,
                Game = games[8533L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 8534L,
                CollectionId = 106L,
                Game = games[8534L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 8593L,
                CollectionId = 106L,
                Game = games[8593L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 8593L,
                CollectionId = 8993L,
                Game = games[8593L],
                Collection = collections[8993L],
            },
            new()
            {
                GameId = 9602L,
                CollectionId = 325L,
                Game = games[9602L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 9621L,
                CollectionId = 325L,
                Game = games[9621L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 11156L,
                CollectionId = 6304L,
                Game = games[11156L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 11186L,
                CollectionId = 66L,
                Game = games[11186L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 11193L,
                CollectionId = 6463L,
                Game = games[11193L],
                Collection = collections[6463L],
            },
            new()
            {
                GameId = 11194L,
                CollectionId = 106L,
                Game = games[11194L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 11607L,
                CollectionId = 106L,
                Game = games[11607L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 11607L,
                CollectionId = 2513L,
                Game = games[11607L],
                Collection = collections[2513L],
            },
            new()
            {
                GameId = 18017L,
                CollectionId = 106L,
                Game = games[18017L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 18066L,
                CollectionId = 106L,
                Game = games[18066L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 18066L,
                CollectionId = 7313L,
                Game = games[18066L],
                Collection = collections[7313L],
            },
            new()
            {
                GameId = 20054L,
                CollectionId = 902L,
                Game = games[20054L],
                Collection = collections[902L],
            },
            new()
            {
                GameId = 25840L,
                CollectionId = 2513L,
                Game = games[25840L],
                Collection = collections[2513L],
            },
            new()
            {
                GameId = 26764L,
                CollectionId = 449L,
                Game = games[26764L],
                Collection = collections[449L],
            },
            new()
            {
                GameId = 37083L,
                CollectionId = 6304L,
                Game = games[37083L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 38319L,
                CollectionId = 106L,
                Game = games[38319L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 38319L,
                CollectionId = 8997L,
                Game = games[38319L],
                Collection = collections[8997L],
            },
            new()
            {
                GameId = 41825L,
                CollectionId = 106L,
                Game = games[41825L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 41825L,
                CollectionId = 8988L,
                Game = games[41825L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 41826L,
                CollectionId = 106L,
                Game = games[41826L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 41826L,
                CollectionId = 8988L,
                Game = games[41826L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 42308L,
                CollectionId = 106L,
                Game = games[42308L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 42308L,
                CollectionId = 8997L,
                Game = games[42308L],
                Collection = collections[8997L],
            },
            new()
            {
                GameId = 42308L,
                CollectionId = 9849L,
                Game = games[42308L],
                Collection = collections[9849L],
            },
            new()
            {
                GameId = 45142L,
                CollectionId = 106L,
                Game = games[45142L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 45142L,
                CollectionId = 8993L,
                Game = games[45142L],
                Collection = collections[8993L],
            },
            new()
            {
                GameId = 45143L,
                CollectionId = 106L,
                Game = games[45143L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 45143L,
                CollectionId = 9821L,
                Game = games[45143L],
                Collection = collections[9821L],
            },
            new()
            {
                GameId = 47604L,
                CollectionId = 106L,
                Game = games[47604L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 47604L,
                CollectionId = 550L,
                Game = games[47604L],
                Collection = collections[550L],
            },
            new()
            {
                GameId = 47604L,
                CollectionId = 6463L,
                Game = games[47604L],
                Collection = collections[6463L],
            },
            new()
            {
                GameId = 47828L,
                CollectionId = 106L,
                Game = games[47828L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 47828L,
                CollectionId = 2513L,
                Game = games[47828L],
                Collection = collections[2513L],
            },
            new()
            {
                GameId = 47828L,
                CollectionId = 3438L,
                Game = games[47828L],
                Collection = collections[3438L],
            },
            new()
            {
                GameId = 72870L,
                CollectionId = 6304L,
                Game = games[72870L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 77336L,
                CollectionId = 106L,
                Game = games[77336L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 90101L,
                CollectionId = 325L,
                Game = games[90101L],
                Collection = collections[325L],
            },
            new()
            {
                GameId = 91680L,
                CollectionId = 106L,
                Game = games[91680L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 91680L,
                CollectionId = 9026L,
                Game = games[91680L],
                Collection = collections[9026L],
            },
            new()
            {
                GameId = 100169L,
                CollectionId = 2513L,
                Game = games[100169L],
                Collection = collections[2513L],
            },
            new()
            {
                GameId = 103291L,
                CollectionId = 66L,
                Game = games[103291L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 103292L,
                CollectionId = 66L,
                Game = games[103292L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 112874L,
                CollectionId = 6304L,
                Game = games[112874L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 115284L,
                CollectionId = 106L,
                Game = games[115284L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 116419L,
                CollectionId = 5604L,
                Game = games[116419L],
                Collection = collections[5604L],
            },
            new()
            {
                GameId = 119388L,
                CollectionId = 106L,
                Game = games[119388L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 119388L,
                CollectionId = 8988L,
                Game = games[119388L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 133935L,
                CollectionId = 2156L,
                Game = games[133935L],
                Collection = collections[2156L],
            },
            new()
            {
                GameId = 134014L,
                CollectionId = 106L,
                Game = games[134014L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 134500L,
                CollectionId = 8997L,
                Game = games[134500L],
                Collection = collections[8997L],
            },
            new()
            {
                GameId = 135912L,
                CollectionId = 5604L,
                Game = games[135912L],
                Collection = collections[5604L],
            },
            new()
            {
                GameId = 136280L,
                CollectionId = 7313L,
                Game = games[136280L],
                Collection = collections[7313L],
            },
            new()
            {
                GameId = 138343L,
                CollectionId = 6463L,
                Game = games[138343L],
                Collection = collections[6463L],
            },
            new()
            {
                GameId = 138343L,
                CollectionId = 8988L,
                Game = games[138343L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 141706L,
                CollectionId = 5604L,
                Game = games[141706L],
                Collection = collections[5604L],
            },
            new()
            {
                GameId = 141742L,
                CollectionId = 5604L,
                Game = games[141742L],
                Collection = collections[5604L],
            },
            new()
            {
                GameId = 143614L,
                CollectionId = 106L,
                Game = games[143614L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 148431L,
                CollectionId = 7023L,
                Game = games[148431L],
                Collection = collections[7023L],
            },
            new()
            {
                GameId = 152361L,
                CollectionId = 5205L,
                Game = games[152361L],
                Collection = collections[5205L],
            },
            new()
            {
                GameId = 152361L,
                CollectionId = 7117L,
                Game = games[152361L],
                Collection = collections[7117L],
            },
            new()
            {
                GameId = 152362L,
                CollectionId = 106L,
                Game = games[152362L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 152362L,
                CollectionId = 5205L,
                Game = games[152362L],
                Collection = collections[5205L],
            },
            new()
            {
                GameId = 152362L,
                CollectionId = 7110L,
                Game = games[152362L],
                Collection = collections[7110L],
            },
            new()
            {
                GameId = 163572L,
                CollectionId = 106L,
                Game = games[163572L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 163572L,
                CollectionId = 9821L,
                Game = games[163572L],
                Collection = collections[9821L],
            },
            new()
            {
                GameId = 172501L,
                CollectionId = 106L,
                Game = games[172501L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 172501L,
                CollectionId = 7338L,
                Game = games[172501L],
                Collection = collections[7338L],
            },
            new()
            {
                GameId = 186309L,
                CollectionId = 6304L,
                Game = games[186309L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 191440L,
                CollectionId = 106L,
                Game = games[191440L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 206370L,
                CollectionId = 106L,
                Game = games[206370L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 208493L,
                CollectionId = 106L,
                Game = games[208493L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 213363L,
                CollectionId = 8218L,
                Game = games[213363L],
                Collection = collections[8218L],
            },
            new()
            {
                GameId = 213594L,
                CollectionId = 8218L,
                Game = games[213594L],
                Collection = collections[8218L],
            },
            new()
            {
                GameId = 227890L,
                CollectionId = 6304L,
                Game = games[227890L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 227987L,
                CollectionId = 226L,
                Game = games[227987L],
                Collection = collections[226L],
            },
            new()
            {
                GameId = 228533L,
                CollectionId = 6304L,
                Game = games[228533L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 229416L,
                CollectionId = 106L,
                Game = games[229416L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 229416L,
                CollectionId = 9849L,
                Game = games[229416L],
                Collection = collections[9849L],
            },
            new()
            {
                GameId = 232696L,
                CollectionId = 106L,
                Game = games[232696L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 232696L,
                CollectionId = 5205L,
                Game = games[232696L],
                Collection = collections[5205L],
            },
            new()
            {
                GameId = 257514L,
                CollectionId = 106L,
                Game = games[257514L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 268842L,
                CollectionId = 6304L,
                Game = games[268842L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 299862L,
                CollectionId = 10066L,
                Game = games[299862L],
                Collection = collections[10066L],
            },
            new()
            {
                GameId = 305003L,
                CollectionId = 6304L,
                Game = games[305003L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 305160L,
                CollectionId = 66L,
                Game = games[305160L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 306149L,
                CollectionId = 106L,
                Game = games[306149L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 317103L,
                CollectionId = 6304L,
                Game = games[317103L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 318793L,
                CollectionId = 66L,
                Game = games[318793L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 338072L,
                CollectionId = 106L,
                Game = games[338072L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 338072L,
                CollectionId = 8988L,
                Game = games[338072L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 338073L,
                CollectionId = 106L,
                Game = games[338073L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 338073L,
                CollectionId = 8988L,
                Game = games[338073L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 338085L,
                CollectionId = 6463L,
                Game = games[338085L],
                Collection = collections[6463L],
            },
            new()
            {
                GameId = 338085L,
                CollectionId = 8988L,
                Game = games[338085L],
                Collection = collections[8988L],
            },
            new()
            {
                GameId = 342721L,
                CollectionId = 66L,
                Game = games[342721L],
                Collection = collections[66L],
            },
            new()
            {
                GameId = 377555L,
                CollectionId = 6304L,
                Game = games[377555L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 388464L,
                CollectionId = 6304L,
                Game = games[388464L],
                Collection = collections[6304L],
            },
            new()
            {
                GameId = 405460L,
                CollectionId = 106L,
                Game = games[405460L],
                Collection = collections[106L],
            },
            new()
            {
                GameId = 405460L,
                CollectionId = 8993L,
                Game = games[405460L],
                Collection = collections[8993L],
            },
        ];
    }
}
