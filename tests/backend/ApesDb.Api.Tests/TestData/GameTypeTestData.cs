using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class GameTypeTestData
{
    public static Dictionary<long, GameType> Create()
    {
        return new Dictionary<long, GameType>
        {
            [0L] = new()
            {
                Id = 0L,
                Name = "Main Game",
                Checksum = Guid.Parse("0d2fcd35-9e5b-aaaf-bf20-806697c0d452"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [1L] = new()
            {
                Id = 1L,
                Name = "DLC",
                Checksum = Guid.Parse("eba3c7aa-7601-9862-ecd6-4075928a49fc"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [2L] = new()
            {
                Id = 2L,
                Name = "Expansion",
                Checksum = Guid.Parse("3ece7d49-a9c0-cdc8-7ee8-b1d5e103da8d"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [3L] = new()
            {
                Id = 3L,
                Name = "Bundle",
                Checksum = Guid.Parse("7e161bed-8efc-f4fe-6dc9-7732eb2bf856"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [4L] = new()
            {
                Id = 4L,
                Name = "Standalone Expansion",
                Checksum = Guid.Parse("e6c6ef65-22f8-af76-75f8-5d18b306e84b"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [5L] = new()
            {
                Id = 5L,
                Name = "Mod",
                Checksum = Guid.Parse("792d7168-7845-45fa-00ba-d79f4f0ffe87"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [6L] = new()
            {
                Id = 6L,
                Name = "Episode",
                Checksum = Guid.Parse("fdfc28b9-e619-a842-7835-17ef550dcff7"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [8L] = new()
            {
                Id = 8L,
                Name = "Remake",
                Checksum = Guid.Parse("9dd9070c-fef0-259b-64cc-2e084db0adf1"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [9L] = new()
            {
                Id = 9L,
                Name = "Remaster",
                Checksum = Guid.Parse("2c38d408-fdc3-277f-692a-b3db85b58ef6"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [10L] = new()
            {
                Id = 10L,
                Name = "Expanded Game",
                Checksum = Guid.Parse("09566b27-1877-e2b3-007d-ae0547853d65"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [11L] = new()
            {
                Id = 11L,
                Name = "Port",
                Checksum = Guid.Parse("d9f55b26-ec60-7daa-fdb9-6539499b4319"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [13L] = new()
            {
                Id = 13L,
                Name = "Pack / Addon",
                Checksum = Guid.Parse("77418120-c6dc-b9f5-acfe-a9601f40eb8a"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
            [14L] = new()
            {
                Id = 14L,
                Name = "Update",
                Checksum = Guid.Parse("96df17cb-c665-f004-dc65-2a00166c86d2"),
                IgdbUpdatedAt = new DateTime(638791845130000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170417006680L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170416509160L, DateTimeKind.Utc),
            },
        };
    }
}
