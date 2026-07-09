using ApesDb.Common;
using ApesDb.Domain;
using Microsoft.EntityFrameworkCore;
using TickerQ.Utilities.Entities;
using TickerQ.Utilities.Interfaces.Managers;

namespace ApesDb.Worker.Games;

public sealed class InitialPopularGamesSyncScheduler : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDbContextFactory<WorkerTickerQDbContext> _tickerDbContextFactory;
    private readonly ITimeTickerManager<TimeTickerEntity> _timeTickerManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<InitialPopularGamesSyncScheduler> _logger;

    public InitialPopularGamesSyncScheduler(
        IServiceScopeFactory scopeFactory,
        IDbContextFactory<WorkerTickerQDbContext> tickerDbContextFactory,
        ITimeTickerManager<TimeTickerEntity> timeTickerManager,
        IDateTimeProvider dateTimeProvider,
        ILogger<InitialPopularGamesSyncScheduler> logger
    )
    {
        _scopeFactory = scopeFactory;
        _tickerDbContextFactory = tickerDbContextFactory;
        _timeTickerManager = timeTickerManager;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await applicationDbContext.PopularGames.AsNoTracking().AnyAsync(cancellationToken))
        {
            return;
        }

        await using var tickerDbContext = await _tickerDbContextFactory.CreateDbContextAsync(cancellationToken);
        var recentCutoff = _dateTimeProvider.UtcNow.AddHours(-1);
        var alreadyScheduled = await tickerDbContext
            .Set<TimeTickerEntity>()
            .AsNoTracking()
            .AnyAsync(
                ticker => ticker.Function == PopularGamesSyncJob.FunctionName && ticker.CreatedAt >= recentCutoff,
                cancellationToken
            );

        if (alreadyScheduled)
        {
            _logger.LogInformation("An initial popular-games import is already scheduled.");
            return;
        }

        var result = await _timeTickerManager.AddAsync(
            new TimeTickerEntity
            {
                Function = PopularGamesSyncJob.FunctionName,
                Description = "Initial IGDB popular-games catalog import",
                ExecutionTime = _dateTimeProvider.UtcNow,
                Retries = 3,
                RetryIntervals = [30, 120, 600],
            },
            cancellationToken
        );

        if (!result.IsSucceeded)
        {
            throw new InvalidOperationException(
                "Could not schedule the initial popular-games import.",
                result.Exception
            );
        }

        _logger.LogInformation("Scheduled an immediate popular-games import because the catalog is empty.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
