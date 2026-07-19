using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GenreTestData
{
    public static Dictionary<long, Genre> Create()
    {
        return new Dictionary<long, Genre>
        {
            [2L] = new()
            {
                Id = 2L,
                Name = "Point-and-click",
                Slug = "point-and-click",
                IgdbUrl = "https://www.igdb.com/genres/point-and-click",
                Checksum = Guid.Parse("b295f28a-5f68-fc3e-5de2-f3195e10d160"),
                IgdbUpdatedAt = new DateTime(634589788860000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [4L] = new()
            {
                Id = 4L,
                Name = "Fighting",
                Slug = "fighting",
                IgdbUrl = "https://www.igdb.com/genres/fighting",
                Checksum = Guid.Parse("d23da988-5bb7-011e-34dc-0b712765e470"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [5L] = new()
            {
                Id = 5L,
                Name = "Shooter",
                Slug = "shooter",
                IgdbUrl = "https://www.igdb.com/genres/shooter",
                Checksum = Guid.Parse("20c255bc-ec2d-c219-34bd-47995b0a79f1"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [7L] = new()
            {
                Id = 7L,
                Name = "Music",
                Slug = "music",
                IgdbUrl = "https://www.igdb.com/genres/music",
                Checksum = Guid.Parse("cbf12469-a5a8-6974-086c-ec9c6aeb5791"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [8L] = new()
            {
                Id = 8L,
                Name = "Platform",
                Slug = "platform",
                IgdbUrl = "https://www.igdb.com/genres/platform",
                Checksum = Guid.Parse("06c188b9-1058-933f-dfee-4ec658117ad2"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [9L] = new()
            {
                Id = 9L,
                Name = "Puzzle",
                Slug = "puzzle",
                IgdbUrl = "https://www.igdb.com/genres/puzzle",
                Checksum = Guid.Parse("f4afdd14-b8c5-e938-58a6-ff0a56514f8b"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [10L] = new()
            {
                Id = 10L,
                Name = "Racing",
                Slug = "racing",
                IgdbUrl = "https://www.igdb.com/genres/racing",
                Checksum = Guid.Parse("41227287-0a1a-0f14-90f2-05655314e8b4"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [12L] = new()
            {
                Id = 12L,
                Name = "Role-playing (RPG)",
                Slug = "role-playing-rpg",
                IgdbUrl = "https://www.igdb.com/genres/role-playing-rpg",
                Checksum = Guid.Parse("5a7bd431-149a-3083-532d-2176f4daca24"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [13L] = new()
            {
                Id = 13L,
                Name = "Simulator",
                Slug = "simulator",
                IgdbUrl = "https://www.igdb.com/genres/simulator",
                Checksum = Guid.Parse("00cd9e3e-ef6b-c1c1-387e-cdfcfdc05dd0"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [14L] = new()
            {
                Id = 14L,
                Name = "Sport",
                Slug = "sport",
                IgdbUrl = "https://www.igdb.com/genres/sport",
                Checksum = Guid.Parse("ae972fd1-328e-08f9-1ce2-340badc1024e"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [15L] = new()
            {
                Id = 15L,
                Name = "Strategy",
                Slug = "strategy",
                IgdbUrl = "https://www.igdb.com/genres/strategy",
                Checksum = Guid.Parse("a722bf83-aa16-da39-6cbf-709bcfd5c0be"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [16L] = new()
            {
                Id = 16L,
                Name = "Turn-based strategy (TBS)",
                Slug = "turn-based-strategy-tbs",
                IgdbUrl = "https://www.igdb.com/genres/turn-based-strategy-tbs",
                Checksum = Guid.Parse("34acc979-404f-f6b2-dc55-3a6f06e6dad6"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [24L] = new()
            {
                Id = 24L,
                Name = "Tactical",
                Slug = "tactical",
                IgdbUrl = "https://www.igdb.com/genres/tactical",
                Checksum = Guid.Parse("f13e2375-3258-bc0c-931b-d7f41f6ca81d"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [25L] = new()
            {
                Id = 25L,
                Name = "Hack and slash/Beat \u0027em up",
                Slug = "hack-and-slash-beat-em-up",
                IgdbUrl = "https://www.igdb.com/genres/hack-and-slash-beat-em-up",
                Checksum = Guid.Parse("6c84d75f-abcb-cf79-dea9-853eb738d178"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [26L] = new()
            {
                Id = 26L,
                Name = "Quiz/Trivia",
                Slug = "quiz-trivia",
                IgdbUrl = "https://www.igdb.com/genres/quiz-trivia",
                Checksum = Guid.Parse("e51ba5a9-8862-fdb6-480f-59e9e8b813df"),
                IgdbUpdatedAt = new DateTime(634588860150000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [31L] = new()
            {
                Id = 31L,
                Name = "Adventure",
                Slug = "adventure",
                IgdbUrl = "https://www.igdb.com/genres/adventure",
                Checksum = Guid.Parse("a93a77df-3927-13fa-1d50-5d443cbb695f"),
                IgdbUpdatedAt = new DateTime(634591648410000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [33L] = new()
            {
                Id = 33L,
                Name = "Arcade",
                Slug = "arcade",
                IgdbUrl = "https://www.igdb.com/genres/arcade",
                Checksum = Guid.Parse("cd4431bf-5482-b058-a863-7eb596a438dd"),
                IgdbUpdatedAt = new DateTime(635165682490000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
            [35L] = new()
            {
                Id = 35L,
                Name = "Card \u0026 Board Game",
                Slug = "card-and-board-game",
                IgdbUrl = "https://www.igdb.com/genres/card-and-board-game",
                Checksum = Guid.Parse("c26cae4e-ecb3-06a9-f46f-245c1da067c8"),
                IgdbUpdatedAt = new DateTime(637244518920000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170429359550L, DateTimeKind.Utc),
            },
        };
    }
}
