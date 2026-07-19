using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameStatusTestData
{
    public static Dictionary<long, GameStatus> Create()
    {
        return new Dictionary<long, GameStatus>
        {
            [6L] = new()
            {
                Id = 6L,
                Name = "Cancelled",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176006"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170423586400L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170423502860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170423502860L, DateTimeKind.Utc),
            },
            [8L] = new()
            {
                Id = 8L,
                Name = "Delisted",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176008"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170423586400L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170423502860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170423502860L, DateTimeKind.Utc),
            },
        };
    }
}
