using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class ExternalGameSourceTestData
{
    public static Dictionary<long, ExternalGameSource> Create()
    {
        return new Dictionary<long, ExternalGameSource>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Steam",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                IgdbUpdatedAt = new DateTime(638308041410000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
            },
            [31L] = new()
            {
                Id = 31L,
                Name = "Xbox Marketplace",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614174030"),
                IgdbUpdatedAt = new DateTime(638309710800000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
            },
            [36L] = new()
            {
                Id = 36L,
                Name = "Playstation Store US",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614174035"),
                IgdbUpdatedAt = new DateTime(638308038740000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
            },
            [54L] = new()
            {
                Id = 54L,
                Name = "Xbox Game Pass Ultimate Cloud",
                Checksum = Guid.Parse("123e4567-e89b-12d3-a456-426614174041"),
                IgdbUpdatedAt = new DateTime(638308038070000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170471603340L, DateTimeKind.Utc),
            },
        };
    }
}
