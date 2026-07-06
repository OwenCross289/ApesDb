using ApesDb.Common;

namespace ApesDb.Worker;

public sealed class Worker : BackgroundService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(IDateTimeProvider dateTimeProvider, ILogger<Worker> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("ApesDb worker heartbeat at {Time}", _dateTimeProvider.OffsetUtcNow);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
