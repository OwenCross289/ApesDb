using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameRelationTestData
{
    public static GameRelation[] Create(IReadOnlyDictionary<long, Game> games)
    {
        return
        [
            new()
            {
                GameId = 492L,
                RelatedGameId = 20656L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650055730L, DateTimeKind.Utc),
                Game = games[492L],
                RelatedGame = games[20656L],
            },
            new()
            {
                GameId = 492L,
                RelatedGameId = 21844L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650055730L, DateTimeKind.Utc),
                Game = games[492L],
                RelatedGame = games[21844L],
            },
            new()
            {
                GameId = 492L,
                RelatedGameId = 148248L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650055730L, DateTimeKind.Utc),
                Game = games[492L],
                RelatedGame = games[148248L],
            },
            new()
            {
                GameId = 548L,
                RelatedGameId = 23264L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650724520L, DateTimeKind.Utc),
                Game = games[548L],
                RelatedGame = games[23264L],
            },
            new()
            {
                GameId = 548L,
                RelatedGameId = 299996L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650724520L, DateTimeKind.Utc),
                Game = games[548L],
                RelatedGame = games[299996L],
            },
            new()
            {
                GameId = 548L,
                RelatedGameId = 299997L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650724520L, DateTimeKind.Utc),
                Game = games[548L],
                RelatedGame = games[299997L],
            },
            new()
            {
                GameId = 548L,
                RelatedGameId = 299998L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184650724520L, DateTimeKind.Utc),
                Game = games[548L],
                RelatedGame = games[299998L],
            },
            new()
            {
                GameId = 1029L,
                RelatedGameId = 45142L,
                RelationType = GameRelationType.StandaloneExpansion,
                CreatedAt = new DateTime(639199184651287510L, DateTimeKind.Utc),
                Game = games[1029L],
                RelatedGame = games[45142L],
            },
            new()
            {
                GameId = 2350L,
                RelatedGameId = 22141L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639200171970706070L, DateTimeKind.Utc),
                Game = games[2350L],
                RelatedGame = games[22141L],
            },
            new()
            {
                GameId = 2350L,
                RelatedGameId = 22146L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639200171970706070L, DateTimeKind.Utc),
                Game = games[2350L],
                RelatedGame = games[22146L],
            },
            new()
            {
                GameId = 2350L,
                RelatedGameId = 399503L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639200171970706070L, DateTimeKind.Utc),
                Game = games[2350L],
                RelatedGame = games[399503L],
            },
            new()
            {
                GameId = 7346L,
                RelatedGameId = 41825L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184657577530L, DateTimeKind.Utc),
                Game = games[7346L],
                RelatedGame = games[41825L],
            },
            new()
            {
                GameId = 7346L,
                RelatedGameId = 41826L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184657577530L, DateTimeKind.Utc),
                Game = games[7346L],
                RelatedGame = games[41826L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178309L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178309L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178311L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178311L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178313L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178313L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178315L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178315L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178318L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178318L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178320L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178320L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 178322L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[178322L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 312966L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[312966L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 312969L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[312969L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 313013L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[313013L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 313015L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[313015L],
            },
            new()
            {
                GameId = 9602L,
                RelatedGameId = 313016L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9602L],
                RelatedGame = games[313016L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178310L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178310L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178312L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178312L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178314L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178314L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178316L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178316L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178319L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178319L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178321L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178321L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 178323L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[178323L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 312965L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[312965L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 312968L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[312968L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 313014L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[313014L],
            },
            new()
            {
                GameId = 9621L,
                RelatedGameId = 313017L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184659650580L, DateTimeKind.Utc),
                Game = games[9621L],
                RelatedGame = games[313017L],
            },
            new()
            {
                GameId = 11156L,
                RelatedGameId = 37083L,
                RelationType = GameRelationType.Expansion,
                CreatedAt = new DateTime(639200171971881300L, DateTimeKind.Utc),
                Game = games[11156L],
                RelatedGame = games[37083L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 217559L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[217559L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 231440L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[231440L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 231441L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[231441L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 231442L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[231442L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 231444L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[231444L],
            },
            new()
            {
                GameId = 26764L,
                RelatedGameId = 231445L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184675362760L, DateTimeKind.Utc),
                Game = games[26764L],
                RelatedGame = games[231445L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 122260L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[122260L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 122261L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[122261L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 129489L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[129489L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 129491L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[129491L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 129492L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[129492L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133835L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133835L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133837L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133837L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133838L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133838L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133839L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133839L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133840L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133840L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 133841L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[133841L],
            },
            new()
            {
                GameId = 90101L,
                RelatedGameId = 136383L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184728563280L, DateTimeKind.Utc),
                Game = games[90101L],
                RelatedGame = games[136383L],
            },
            new()
            {
                GameId = 103292L,
                RelatedGameId = 140517L,
                RelationType = GameRelationType.Expansion,
                CreatedAt = new DateTime(639199184740842100L, DateTimeKind.Utc),
                Game = games[103292L],
                RelatedGame = games[140517L],
            },
            new()
            {
                GameId = 112874L,
                RelatedGameId = 228533L,
                RelationType = GameRelationType.Expansion,
                CreatedAt = new DateTime(639199184750202860L, DateTimeKind.Utc),
                Game = games[112874L],
                RelatedGame = games[228533L],
            },
            new()
            {
                GameId = 116419L,
                RelatedGameId = 135912L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639200171975444240L, DateTimeKind.Utc),
                Game = games[116419L],
                RelatedGame = games[135912L],
            },
            new()
            {
                GameId = 116419L,
                RelatedGameId = 235330L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639200171975444240L, DateTimeKind.Utc),
                Game = games[116419L],
                RelatedGame = games[235330L],
            },
            new()
            {
                GameId = 138343L,
                RelatedGameId = 184509L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184774324160L, DateTimeKind.Utc),
                Game = games[138343L],
                RelatedGame = games[184509L],
            },
            new()
            {
                GameId = 138343L,
                RelatedGameId = 184510L,
                RelationType = GameRelationType.Dlc,
                CreatedAt = new DateTime(639199184774324160L, DateTimeKind.Utc),
                Game = games[138343L],
                RelatedGame = games[184510L],
            },
        ];
    }
}
