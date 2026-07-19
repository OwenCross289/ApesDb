using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class PlatformTypeTestData
{
    public static Dictionary<long, PlatformType> Create()
    {
        return new Dictionary<long, PlatformType>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Console",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176001"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
            },
            [3L] = new()
            {
                Id = 3L,
                Name = "Platform",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176003"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
            },
            [4L] = new()
            {
                Id = 4L,
                Name = "Operating_system",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176004"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
            },
            [5L] = new()
            {
                Id = 5L,
                Name = "Portable_console",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614176005"),
                IgdbUpdatedAt = new DateTime(638741376000000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170454377370L, DateTimeKind.Utc),
            },
        };
    }
}
