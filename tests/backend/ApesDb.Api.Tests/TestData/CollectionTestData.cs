using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class CollectionTestData
{
    public static Dictionary<long, Collection> Create()
    {
        return new Dictionary<long, Collection>
        {
            [66L] = new()
            {
                Id = 66L,
                Name = "Gears of War",
                Slug = "gears-of-war",
                IgdbUrl = "https://www.igdb.com/collections/gears-of-war",
                Checksum = Guid.Parse("3d7e5259-4e87-cf97-af82-922c2f53e7a4"),
                IgdbUpdatedAt = new DateTime(639113741710000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [106L] = new()
            {
                Id = 106L,
                Name = "The Legend of Zelda",
                Slug = "the-legend-of-zelda",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda",
                Checksum = Guid.Parse("18b09880-eb12-9d9b-8abd-75f202472a69"),
                IgdbUpdatedAt = new DateTime(639196566110000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [226L] = new()
            {
                Id = 226L,
                Name = "SoulCalibur",
                Slug = "soulcalibur",
                IgdbUrl = "https://www.igdb.com/collections/soulcalibur",
                Checksum = Guid.Parse("5f0edd03-2e96-ed53-c4de-76bcd8d204f9"),
                IgdbUpdatedAt = new DateTime(639033918260000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [325L] = new()
            {
                Id = 325L,
                Name = "Super Smash Bros.",
                Slug = "super-smash-bros",
                IgdbUrl = "https://www.igdb.com/collections/super-smash-bros",
                Checksum = Guid.Parse("61a5aec7-0748-c760-d211-73cdd0c9f2aa"),
                IgdbUpdatedAt = new DateTime(638801979680000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [449L] = new()
            {
                Id = 449L,
                Name = "Mario Kart",
                Slug = "mario-kart",
                IgdbUrl = "https://www.igdb.com/collections/mario-kart",
                Checksum = Guid.Parse("b772c07d-0308-4e53-a0c6-1b1c4b2e51ca"),
                IgdbUpdatedAt = new DateTime(639095840250000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [550L] = new()
            {
                Id = 550L,
                Name = "Dynasty Warriors",
                Slug = "dynasty-warriors",
                IgdbUrl = "https://www.igdb.com/collections/dynasty-warriors",
                Checksum = Guid.Parse("b4ed1fd5-c567-a3ee-797e-70a581f25f71"),
                IgdbUpdatedAt = new DateTime(639187067180000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171855500720L, DateTimeKind.Utc),
            },
            [902L] = new()
            {
                Id = 902L,
                Name = "NES Remix",
                Slug = "nes-remix",
                IgdbUrl = "https://www.igdb.com/collections/nes-remix",
                Checksum = Guid.Parse("b3c3afd5-9ad1-af4d-4520-b2555e31f4a7"),
                IgdbUpdatedAt = new DateTime(638578954020000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171862620580L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171862620580L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171862620580L, DateTimeKind.Utc),
            },
            [2156L] = new()
            {
                Id = 2156L,
                Name = "Sonic the Hedgehog",
                Slug = "sonic-the-hedgehog",
                IgdbUrl = "https://www.igdb.com/collections/sonic-the-hedgehog",
                Checksum = Guid.Parse("69d31084-abff-3440-70a6-6fa28aa95203"),
                IgdbUpdatedAt = new DateTime(639137772430000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
            },
            [2513L] = new()
            {
                Id = 2513L,
                Name = "Tingle",
                Slug = "tingle",
                IgdbUrl = "https://www.igdb.com/collections/tingle",
                Checksum = Guid.Parse("325eb9ac-95d0-29bf-fef5-04c2150f6ad5"),
                IgdbUpdatedAt = new DateTime(638562305970000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171883240660L, DateTimeKind.Utc),
            },
            [3438L] = new()
            {
                Id = 3438L,
                Name = "Balloon Fight",
                Slug = "balloon-fight",
                IgdbUrl = "https://www.igdb.com/collections/balloon-fight",
                Checksum = Guid.Parse("27ce6eac-6cad-48ab-d279-5af181dadd18"),
                IgdbUpdatedAt = new DateTime(638820713100000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171903085330L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171903085330L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171903085330L, DateTimeKind.Utc),
            },
            [5205L] = new()
            {
                Id = 5205L,
                Name = "Game \u0026 Watch",
                Slug = "game-and-watch",
                IgdbUrl = "https://www.igdb.com/collections/game-and-watch",
                Checksum = Guid.Parse("bc3044a3-a8d4-72c6-50fd-40e390c1bee8"),
                IgdbUpdatedAt = new DateTime(638852252350000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171925725520L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171925725520L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171925725520L, DateTimeKind.Utc),
            },
            [5604L] = new()
            {
                Id = 5604L,
                Name = "Crypt of the Necrodancer",
                Slug = "crypt-of-the-necrodancer",
                IgdbUrl = "https://www.igdb.com/collections/crypt-of-the-necrodancer",
                Checksum = Guid.Parse("bb48625b-afd4-0df5-902e-a9c2fd9792d3"),
                IgdbUpdatedAt = new DateTime(638977606880000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171935756180L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171935756180L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171935756180L, DateTimeKind.Utc),
            },
            [6304L] = new()
            {
                Id = 6304L,
                Name = "Horizon",
                Slug = "horizon",
                IgdbUrl = "https://www.igdb.com/collections/horizon",
                Checksum = Guid.Parse("81e85500-5d6c-4b1a-ddc5-9fccf78cf1ff"),
                IgdbUpdatedAt = new DateTime(639059023720000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
            },
            [6463L] = new()
            {
                Id = 6463L,
                Name = "Hyrule Warriors",
                Slug = "hyrule-warriors",
                IgdbUrl = "https://www.igdb.com/collections/hyrule-warriors",
                Checksum = Guid.Parse("1d06a16b-b24b-5ca6-3411-82f363fc5b17"),
                IgdbUpdatedAt = new DateTime(639187067180000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171945815400L, DateTimeKind.Utc),
            },
            [7023L] = new()
            {
                Id = 7023L,
                Name = "Picross NP",
                Slug = "picross-np",
                IgdbUrl = "https://www.igdb.com/collections/picross-np",
                Checksum = Guid.Parse("6defb98e-8da6-4916-d443-4dd531909039"),
                IgdbUpdatedAt = new DateTime(638447124690000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
            },
            [7110L] = new()
            {
                Id = 7110L,
                Name = "Game \u0026 Watch: Multi Screen",
                Slug = "game-and-watch-multi-screen--1",
                IgdbUrl = "https://www.igdb.com/collections/game-and-watch-multi-screen--1",
                Checksum = Guid.Parse("308d1824-cb92-32cf-350b-071af589ef54"),
                IgdbUpdatedAt = new DateTime(638590346730000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
            },
            [7117L] = new()
            {
                Id = 7117L,
                Name = "Game \u0026 Watch: Color Screen",
                Slug = "game-and-watch-color-screen",
                IgdbUrl = "https://www.igdb.com/collections/game-and-watch-color-screen",
                Checksum = Guid.Parse("20b9942b-f83c-9384-2ac5-e4d9708fb64e"),
                IgdbUpdatedAt = new DateTime(638852252350000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
            },
            [7313L] = new()
            {
                Id = 7313L,
                Name = "Classic NES Series",
                Slug = "classic-nes-series",
                IgdbUrl = "https://www.igdb.com/collections/classic-nes-series",
                Checksum = Guid.Parse("06b7c0ce-4711-5013-f0ff-65d5d78a6d5d"),
                IgdbUpdatedAt = new DateTime(639077815810000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
            },
            [7338L] = new()
            {
                Id = 7338L,
                Name = "Game Watch",
                Slug = "game-watch",
                IgdbUrl = "https://www.igdb.com/collections/game-watch",
                Checksum = Guid.Parse("601f0227-dc26-b337-42c1-d7b559eda2e9"),
                IgdbUpdatedAt = new DateTime(638494066100000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171956089170L, DateTimeKind.Utc),
            },
            [8218L] = new()
            {
                Id = 8218L,
                Name = "Nintendo Classic Mini",
                Slug = "nintendo-classic-mini",
                IgdbUrl = "https://www.igdb.com/collections/nintendo-classic-mini",
                Checksum = Guid.Parse("ef9bb607-528a-74cd-a354-4ebc7e7f60fa"),
                IgdbUpdatedAt = new DateTime(638539207340000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171976457540L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171976457540L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171976457540L, DateTimeKind.Utc),
            },
            [8988L] = new()
            {
                Id = 8988L,
                Name = "The Legend of Zelda: Breath of the Wild",
                Slug = "the-legend-of-zelda-breath-of-the-wild",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda-breath-of-the-wild",
                Checksum = Guid.Parse("6749904a-26d8-2c97-dc14-2e9eeb7253f7"),
                IgdbUpdatedAt = new DateTime(639196566120000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
            },
            [8993L] = new()
            {
                Id = 8993L,
                Name = "The Legend of Zelda: Ocarina of Time",
                Slug = "the-legend-of-zelda-ocarina-of-time",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda-ocarina-of-time",
                Checksum = Guid.Parse("0470fd63-2873-90a0-0216-0d191e8176a5"),
                IgdbUpdatedAt = new DateTime(639166213870000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
            },
            [8997L] = new()
            {
                Id = 8997L,
                Name = "BS Zelda no Densetsu",
                Slug = "bs-zelda-no-densetsu",
                IgdbUrl = "https://www.igdb.com/collections/bs-zelda-no-densetsu",
                Checksum = Guid.Parse("08f89b37-3ad9-24f3-d988-51c17472552e"),
                IgdbUpdatedAt = new DateTime(638590216480000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
            },
            [9026L] = new()
            {
                Id = 9026L,
                Name = "The Legend of Zelda: The Wind Waker",
                Slug = "the-legend-of-zelda-the-wind-waker",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda-the-wind-waker",
                Checksum = Guid.Parse("bea1b282-8c84-2c25-e375-08ffd26edbf1"),
                IgdbUpdatedAt = new DateTime(638576229990000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171986600690L, DateTimeKind.Utc),
            },
            [9821L] = new()
            {
                Id = 9821L,
                Name = "The Legend of Zelda: Four Swords",
                Slug = "the-legend-of-zelda-four-swords",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda-four-swords",
                Checksum = Guid.Parse("2a0bd892-a5c6-27a8-b0f6-960d600ece48"),
                IgdbUpdatedAt = new DateTime(638582112650000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
            },
            [9849L] = new()
            {
                Id = 9849L,
                Name = "The Legend of Zelda: A Link to the Past",
                Slug = "the-legend-of-zelda-a-link-to-the-past",
                IgdbUrl = "https://www.igdb.com/collections/the-legend-of-zelda-a-link-to-the-past",
                Checksum = Guid.Parse("96ecb159-651b-a217-4576-63f87baaeb4c"),
                IgdbUpdatedAt = new DateTime(638578976140000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
            },
            [10066L] = new()
            {
                Id = 10066L,
                Name = "Nintendo World Championships",
                Slug = "nintendo-world-championships",
                IgdbUrl = "https://www.igdb.com/collections/nintendo-world-championships",
                Checksum = Guid.Parse("21cb3dc9-9030-038f-ce54-ced8522c0851"),
                IgdbUpdatedAt = new DateTime(638618143570000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199171996712890L, DateTimeKind.Utc),
            },
        };
    }
}
