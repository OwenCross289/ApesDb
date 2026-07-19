using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class PlayerPerspectiveTestData
{
    public static Dictionary<long, PlayerPerspective> Create()
    {
        return new Dictionary<long, PlayerPerspective>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "First person",
                Slug = "first-person",
                IgdbUrl = "https://www.igdb.com/player_perspectives/first-person",
                Checksum = Guid.Parse("d90c79c9-6e5d-b0c9-49fd-3866866b8c9f"),
                IgdbUpdatedAt = new DateTime(634588860140000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
            [2L] = new()
            {
                Id = 2L,
                Name = "Third person",
                Slug = "third-person",
                IgdbUrl = "https://www.igdb.com/player_perspectives/third-person",
                Checksum = Guid.Parse("df3218c6-d152-af20-f352-b04f70f43ae5"),
                IgdbUpdatedAt = new DateTime(634588860140000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
            [3L] = new()
            {
                Id = 3L,
                Name = "Bird view / Isometric",
                Slug = "bird-view-slash-isometric",
                IgdbUrl = "https://www.igdb.com/player_perspectives/bird-view-slash-isometric",
                Checksum = Guid.Parse("efa5333f-98c8-654b-4d2f-34f10feb2b60"),
                IgdbUpdatedAt = new DateTime(637181352430000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
            [4L] = new()
            {
                Id = 4L,
                Name = "Side view",
                Slug = "side-view",
                IgdbUrl = "https://www.igdb.com/player_perspectives/side-view",
                Checksum = Guid.Parse("54f282ed-e1d2-f821-8f29-d436138c92f2"),
                IgdbUpdatedAt = new DateTime(634588860140000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
            [5L] = new()
            {
                Id = 5L,
                Name = "Text",
                Slug = "text",
                IgdbUrl = "https://www.igdb.com/player_perspectives/text",
                Checksum = Guid.Parse("472912b7-0793-021c-fdca-90bfcdf901bd"),
                IgdbUpdatedAt = new DateTime(634588860140000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
            [7L] = new()
            {
                Id = 7L,
                Name = "Virtual Reality",
                Slug = "virtual-reality",
                IgdbUrl = "https://www.igdb.com/player_perspectives/virtual-reality",
                Checksum = Guid.Parse("2db09175-5aa1-9d30-3adc-a03e47f75b70"),
                IgdbUpdatedAt = new DateTime(635978852840000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170448518080L, DateTimeKind.Utc),
            },
        };
    }
}
