using ApesDb.Common;

namespace ApesDb.Api.Tests.Infrastructure.Time;

public static class TestClock
{
    public static readonly DateTime UtcNow = new(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);
}

public sealed class TestDateTimeProvider : IDateTimeProvider
{
    public TestDateTimeProvider(DateTime utcNow)
    {
        UtcNow = utcNow;
        Now = utcNow.ToLocalTime();
        OffsetUtcNow = new DateTimeOffset(utcNow);
        OffsetNow = OffsetUtcNow.ToLocalTime();
    }

    public DateTime Now { get; }

    public DateTime UtcNow { get; }

    public DateTimeOffset OffsetNow { get; }

    public DateTimeOffset OffsetUtcNow { get; }
}
