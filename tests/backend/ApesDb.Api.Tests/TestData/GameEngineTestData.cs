using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameEngineTestData
{
    public static Dictionary<long, GameEngine> Create()
    {
        return new Dictionary<long, GameEngine>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Decima",
                Checksum = Guid.Parse("bb90de1a-d2dc-4c57-b880-603d7155fd24"),
                IgdbUpdatedAt = new DateTime(638779819570000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
            [2L] = new()
            {
                Id = 2L,
                Name = "Havok",
                Checksum = Guid.Parse("2afe653b-39a1-4710-9436-866eff15b9db"),
                IgdbUpdatedAt = new DateTime(638779819570000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
        };
    }
}
