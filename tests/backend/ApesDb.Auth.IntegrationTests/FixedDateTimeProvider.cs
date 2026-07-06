using ApesDb.Common;

namespace ApesDb.Auth.IntegrationTests;

public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateTime utcNow)
    {
        UtcNow = utcNow;
        Now = utcNow.ToLocalTime();
        OffsetNow = new DateTimeOffset(Now);
        OffsetUtcNow = new DateTimeOffset(utcNow);
    }

    public DateTime Now { get; private set; }

    public DateTime UtcNow
    {
        get => _utcNow;
        set
        {
            _utcNow = value;
            Now = value.ToLocalTime();
            OffsetNow = new DateTimeOffset(Now);
            OffsetUtcNow = new DateTimeOffset(value);
        }
    }

    public DateTimeOffset OffsetNow { get; private set; }

    public DateTimeOffset OffsetUtcNow { get; private set; }

    private DateTime _utcNow;
}
