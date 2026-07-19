using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class PopularGameTestData
{
    public static PopularGame[] Create(
        IReadOnlyDictionary<long, Game> games,
        IReadOnlyDictionary<long, PopularityType> popularityTypes
    )
    {
        return
        [
            new()
            {
                Id = 3874919L,
                GameId = 1022L,
                Rank = 759,
                SourceRank = 760,
                Score = 0.000143203559086000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163160000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[1022L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3874921L,
                GameId = 1026L,
                Rank = 585,
                SourceRank = 585,
                Score = 0.000171844270903000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163160000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[1026L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3874924L,
                GameId = 1029L,
                Rank = 417,
                SourceRank = 417,
                Score = 0.000214805338629000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163160000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[1029L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3877690L,
                GameId = 7346L,
                Rank = 265,
                SourceRank = 265,
                Score = 0.000305500926050000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163220000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[7346L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3878875L,
                GameId = 11156L,
                Rank = 962,
                SourceRank = 963,
                Score = 0.000124109751208000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163230000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[11156L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3881827L,
                GameId = 26764L,
                Rank = 899,
                SourceRank = 900,
                Score = 0.000128883203177000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163260000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[26764L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3888197L,
                GameId = 90101L,
                Rank = 429,
                SourceRank = 429,
                Score = 0.000210031886659000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163290000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[90101L],
                PopularityType = popularityTypes[1L],
            },
            new()
            {
                Id = 3890511L,
                GameId = 119388L,
                Rank = 339,
                SourceRank = 339,
                Score = 0.000248219502415000m,
                PopularityTypeId = 1L,
                CalculatedAt = new DateTime(639200163120000000L, DateTimeKind.Utc),
                IgdbUpdatedAt = new DateTime(639200163320000000L, DateTimeKind.Utc),
                Checksum = null,
                SyncedAt = new DateTime(639200196000372350L, DateTimeKind.Utc),
                Game = games[119388L],
                PopularityType = popularityTypes[1L],
            },
        ];
    }
}
