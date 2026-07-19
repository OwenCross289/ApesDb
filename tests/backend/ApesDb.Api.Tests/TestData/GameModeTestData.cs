using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameModeTestData
{
    public static Dictionary<long, GameMode> Create()
    {
        return new Dictionary<long, GameMode>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Single player",
                Slug = "single-player",
                IgdbUrl = "https://www.igdb.com/game_modes/single-player",
                Checksum = Guid.Parse("1cc07088-c5fb-3cb2-9e68-af6620c18836"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
            [2L] = new()
            {
                Id = 2L,
                Name = "Multiplayer",
                Slug = "multiplayer",
                IgdbUrl = "https://www.igdb.com/game_modes/multiplayer",
                Checksum = Guid.Parse("3ffef62b-e19f-6bab-d510-98385c06d902"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
            [3L] = new()
            {
                Id = 3L,
                Name = "Co-operative",
                Slug = "co-operative",
                IgdbUrl = "https://www.igdb.com/game_modes/co-operative",
                Checksum = Guid.Parse("d154173b-5bdf-5fda-5969-e4c87e135d1a"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
            [4L] = new()
            {
                Id = 4L,
                Name = "Split screen",
                Slug = "split-screen",
                IgdbUrl = "https://www.igdb.com/game_modes/split-screen",
                Checksum = Guid.Parse("a165517f-f166-79cb-1290-d8c8d4f157a8"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
            [5L] = new()
            {
                Id = 5L,
                Name = "Massively Multiplayer Online (MMO)",
                Slug = "massively-multiplayer-online-mmo",
                IgdbUrl = "https://www.igdb.com/game_modes/massively-multiplayer-online-mmo",
                Checksum = Guid.Parse("631ae92b-e28d-8089-dd44-48cd2febf4ea"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170442746860L, DateTimeKind.Utc),
            },
        };
    }
}
