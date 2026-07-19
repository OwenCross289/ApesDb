using ApesDb.Domain.Entities.Games;

namespace ApesDb.Api.Tests.TestData;

internal static class PopularityTypeTestData
{
    public static Dictionary<long, PopularityType> Create()
    {
        return new Dictionary<long, PopularityType>
        {
            [1L] = new()
            {
                Id = 1L,
                Name = "Visits",
                ExternalPopularitySourceId = 121L,
                Checksum = Guid.Parse("68293106-b76f-b9e1-14be-6c1becde5dd0"),
                IgdbUpdatedAt = new DateTime(638755205720000000L, DateTimeKind.Utc),
                CreatedAt = new DateTime(639199170465893970L, DateTimeKind.Utc),
                UpdatedAt = new DateTime(639199170465893970L, DateTimeKind.Utc),
                LastSyncedAt = new DateTime(639199170465893970L, DateTimeKind.Utc),
            },
        };
    }
}
