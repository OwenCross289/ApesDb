using ApesDb.Common;

namespace ApesDb.Teams.IntegrationTests;

public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateTime utcNow)
    {
        UtcNow = utcNow;
    }

    public DateTime Now => UtcNow.ToLocalTime();

    public DateTime UtcNow { get; set; }

    public DateTimeOffset OffsetNow => new(Now);

    public DateTimeOffset OffsetUtcNow => new(UtcNow);
}
