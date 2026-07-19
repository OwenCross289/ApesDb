using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class ThemeTestData
{
    public static Dictionary<long, Theme> Create()
    {
        return new Dictionary<long, Theme>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Action",
                Slug = "action",
                IgdbUrl = "https://www.igdb.com/themes/action",
                Checksum = Guid.Parse("cee4e3c1-6b2d-6dcc-a707-e00ca4de6ecc"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [17L] = new()
            {
                Id = 17L,
                Name = "Fantasy",
                Slug = "fantasy",
                IgdbUrl = "https://www.igdb.com/themes/fantasy",
                Checksum = Guid.Parse("e24dc205-20ff-01fc-00eb-d7a460ad600b"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [18L] = new()
            {
                Id = 18L,
                Name = "Science fiction",
                Slug = "science-fiction",
                IgdbUrl = "https://www.igdb.com/themes/science-fiction",
                Checksum = Guid.Parse("6aebdd69-6e6e-61f3-4721-3eb222712062"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [19L] = new()
            {
                Id = 19L,
                Name = "Horror",
                Slug = "horror",
                IgdbUrl = "https://www.igdb.com/themes/horror",
                Checksum = Guid.Parse("4775a860-da15-e363-9bbd-b944f7a90ab9"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [21L] = new()
            {
                Id = 21L,
                Name = "Survival",
                Slug = "survival",
                IgdbUrl = "https://www.igdb.com/themes/survival",
                Checksum = Guid.Parse("9dda17f7-c26b-bf97-40d1-4fbff9f1fa4b"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [22L] = new()
            {
                Id = 22L,
                Name = "Historical",
                Slug = "historical",
                IgdbUrl = "https://www.igdb.com/themes/historical",
                Checksum = Guid.Parse("055d8eb9-e32a-35b5-22b6-05f33f883ae3"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [23L] = new()
            {
                Id = 23L,
                Name = "Stealth",
                Slug = "stealth",
                IgdbUrl = "https://www.igdb.com/themes/stealth",
                Checksum = Guid.Parse("71cc79cc-2055-baa3-0d08-b6136ae5c3fa"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [27L] = new()
            {
                Id = 27L,
                Name = "Comedy",
                Slug = "comedy",
                IgdbUrl = "https://www.igdb.com/themes/comedy",
                Checksum = Guid.Parse("81df3ee7-c23d-b87f-36b5-e2a3279d45de"),
                IgdbUpdatedAt = new DateTime(634588860160000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [31L] = new()
            {
                Id = 31L,
                Name = "Drama",
                Slug = "drama",
                IgdbUrl = "https://www.igdb.com/themes/drama",
                Checksum = Guid.Parse("a10308c4-a660-7016-742b-ec956e9e9675"),
                IgdbUpdatedAt = new DateTime(634592069970000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [33L] = new()
            {
                Id = 33L,
                Name = "Sandbox",
                Slug = "sandbox",
                IgdbUrl = "https://www.igdb.com/themes/sandbox",
                Checksum = Guid.Parse("e2f2c5b2-16ea-6e44-e2bb-af500d0b3f97"),
                IgdbUpdatedAt = new DateTime(634621418590000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [35L] = new()
            {
                Id = 35L,
                Name = "Kids",
                Slug = "kids",
                IgdbUrl = "https://www.igdb.com/themes/kids",
                Checksum = Guid.Parse("95416d52-fb76-9995-f035-ee86bafbe5c4"),
                IgdbUpdatedAt = new DateTime(634624267400000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [38L] = new()
            {
                Id = 38L,
                Name = "Open world",
                Slug = "open-world",
                IgdbUrl = "https://www.igdb.com/themes/open-world",
                Checksum = Guid.Parse("3417947f-3f76-24e3-fc7b-ce2a804d448a"),
                IgdbUpdatedAt = new DateTime(634810832260000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [39L] = new()
            {
                Id = 39L,
                Name = "Warfare",
                Slug = "warfare",
                IgdbUrl = "https://www.igdb.com/themes/warfare",
                Checksum = Guid.Parse("361067f4-c398-2b0b-6c51-8b3132372658"),
                IgdbUpdatedAt = new DateTime(634810899340000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [40L] = new()
            {
                Id = 40L,
                Name = "Party",
                Slug = "party",
                IgdbUrl = "https://www.igdb.com/themes/party",
                Checksum = Guid.Parse("8eb5e994-f11e-210a-f2f3-9eb70bc72dc9"),
                IgdbUpdatedAt = new DateTime(634924862010000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
            [44L] = new()
            {
                Id = 44L,
                Name = "Romance",
                Slug = "romance",
                IgdbUrl = "https://www.igdb.com/themes/romance",
                Checksum = Guid.Parse("8c40b73c-c673-3e26-6d53-69d01476f473"),
                IgdbUpdatedAt = new DateTime(637244520940000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170436919700L, DateTimeKind.Utc),
            },
        };
    }
}
