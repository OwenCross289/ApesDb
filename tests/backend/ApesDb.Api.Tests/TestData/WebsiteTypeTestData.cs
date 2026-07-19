using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class WebsiteTypeTestData
{
    public static Dictionary<long, WebsiteType> Create()
    {
        return new Dictionary<long, WebsiteType>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Official Website",
                Checksum = Guid.Parse("68a07b1b-ff77-b4eb-1603-a179fab0ee5f"),
                IgdbUpdatedAt = new DateTime(638779819570000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170460105960L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170460105960L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170460105960L, DateTimeKind.Utc),
            },
        };
    }
}
