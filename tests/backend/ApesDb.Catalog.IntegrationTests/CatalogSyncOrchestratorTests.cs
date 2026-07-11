using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using ApesDb.Worker.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TickerQ.Utilities.Entities;
using TickerQ.Utilities.Enums;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed class CatalogSyncOrchestratorTests : IClassFixture<CatalogDatabaseFixture>
{
    private const string GenresFunction = "sync-igdb-genres";

    private readonly CatalogDatabaseFixture _database;

    public CatalogSyncOrchestratorTests(CatalogDatabaseFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task CompletedBootstrap_StartsAContiguousCatchUpAndLaterWindowsDoNotOverlap()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, 987, DateTimeKind.Utc));
        var manager = new FakeTimeTickerManager(_database.CreateTickerDbContext);
        var orchestrator = CreateOrchestrator(dbContext, manager, clock);

        await orchestrator.EnsureBootstrapAsync();

        var bootstrap = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync();
        Assert.Equal(IgdbSyncRunMode.Bootstrap, bootstrap.Mode);
        Assert.Null(bootstrap.From);
        Assert.Equal(new DateTime(2026, 7, 9, 11, 55, 0, DateTimeKind.Utc), bootstrap.Through);
        Assert.Equal(21, await dbContext.IgdbSyncStages.CountAsync(value => value.RunId == bootstrap.Id));
        Assert.Single(manager.Added);

        clock.UtcNow = new DateTime(2026, 7, 9, 12, 5, 10, 456, DateTimeKind.Utc);
        await MarkReadyToCompleteAsync(dbContext, bootstrap.Id, clock.UtcNow);
        await orchestrator.CompleteAsync(bootstrap.Id, retryCount: 0);

        dbContext.ChangeTracker.Clear();
        var catchUp = await dbContext
            .IgdbSyncRuns.AsNoTracking()
            .SingleAsync(value => value.Mode == IgdbSyncRunMode.Incremental);
        Assert.Equal(bootstrap.Through, catchUp.From);
        Assert.Equal(CaptureExpectedThrough(clock.UtcNow), catchUp.Through);
        Assert.True(catchUp.From < catchUp.Through);
        Assert.Equal(20, await dbContext.IgdbSyncStages.CountAsync(value => value.RunId == catchUp.Id));
        Assert.Equal(2, manager.Added.Count);

        clock.UtcNow = new DateTime(2026, 7, 9, 13, 0, 0, 999, DateTimeKind.Utc);
        await orchestrator.EnsureBootstrapAsync();
        await orchestrator.EnsureIncrementalAsync();
        Assert.Equal(2, await dbContext.IgdbSyncRuns.CountAsync());
        Assert.Equal(2, manager.Added.Count);

        await MarkReadyToCompleteAsync(dbContext, catchUp.Id, clock.UtcNow);
        await orchestrator.CompleteAsync(catchUp.Id, retryCount: 0);
        clock.UtcNow = new DateTime(2026, 7, 10, 2, 0, 0, 750, DateTimeKind.Utc);
        await orchestrator.EnsureIncrementalAsync();

        dbContext.ChangeTracker.Clear();
        var incrementals = await dbContext
            .IgdbSyncRuns.AsNoTracking()
            .Where(value => value.Mode == IgdbSyncRunMode.Incremental)
            .OrderBy(value => value.Through)
            .ToArrayAsync();
        Assert.Equal(2, incrementals.Length);
        var daily = incrementals[1];
        Assert.Equal(catchUp.Through, daily.From);
        Assert.Equal(CaptureExpectedThrough(clock.UtcNow), daily.Through);
        Assert.Equal(1, await dbContext.IgdbSyncRuns.CountAsync(value => value.Status != IgdbSyncRunStatus.Succeeded));
    }

    [Fact]
    public async Task ConcurrentBootstrapChecks_LeaveOneUnfinishedRunAndOneStageTicker()
    {
        await _database.ResetAsync();
        var now = new DateTime(2026, 7, 9, 12, 0, 0, 500, DateTimeKind.Utc);
        var clock = new MutableDateTimeProvider(now);
        await using var firstDbContext = _database.CreateDbContext();
        await using var secondDbContext = _database.CreateDbContext();
        var firstManager = new FakeTimeTickerManager(_database.CreateTickerDbContext);
        var secondManager = new FakeTimeTickerManager(_database.CreateTickerDbContext);
        var first = CreateOrchestrator(firstDbContext, firstManager, clock);
        var second = CreateOrchestrator(secondDbContext, secondManager, clock);

        await Task.WhenAll(first.EnsureBootstrapAsync(), second.EnsureBootstrapAsync());

        await using var verification = _database.CreateDbContext();
        var run = await verification.IgdbSyncRuns.AsNoTracking().SingleAsync();
        Assert.Equal(IgdbSyncRunStatus.Pending, run.Status);
        Assert.Equal(CaptureExpectedThrough(now), run.Through);
        Assert.Equal(
            1,
            await verification.IgdbSyncRuns.CountAsync(value => value.Status != IgdbSyncRunStatus.Succeeded)
        );
        var firstStage = await verification
            .IgdbSyncStages.AsNoTracking()
            .Where(value => value.RunId == run.Id)
            .OrderBy(value => value.Order)
            .FirstAsync();
        await using var tickerDbContext = _database.CreateTickerDbContext();
        var ticker = await tickerDbContext.Set<TimeTickerEntity>().AsNoTracking().SingleAsync();
        Assert.Equal(firstStage.Id, ticker.Id);
        Assert.Equal(TickerStatus.Idle, ticker.Status);
    }

    [Fact]
    public async Task ConcurrentRecoveryOfTheSameTerminalTicker_LeavesOneActiveTickerAndPreservesProgress()
    {
        await _database.ResetAsync();
        Guid runId;
        Guid stageId;
        await using (var seedDbContext = _database.CreateDbContext())
        {
            (runId, stageId) = await CreateUnfinishedRunAsync(
                seedDbContext,
                IgdbSyncRunStatus.Running,
                IgdbSyncStageStatus.Running
            );
        }

        await InsertTickerAsync(stageId, TickerStatus.Failed);
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc));
        await using var firstDbContext = _database.CreateDbContext();
        await using var secondDbContext = _database.CreateDbContext();
        var firstManager = new FakeTimeTickerManager(_database.CreateTickerDbContext);
        var secondManager = new FakeTimeTickerManager(_database.CreateTickerDbContext);
        var first = CreateOrchestrator(firstDbContext, firstManager, clock);
        var second = CreateOrchestrator(secondDbContext, secondManager, clock);

        await Task.WhenAll(first.EnsureIncrementalAsync(), second.EnsureIncrementalAsync());

        await using var verification = _database.CreateDbContext();
        await AssertRecoveredAsync(verification, runId, stageId);
        Assert.Equal(1, firstManager.Deleted.Count + secondManager.Deleted.Count);
        Assert.Equal(1, firstManager.Added.Count + secondManager.Added.Count);
        await AssertActiveTickerAsync(stageId);
        await using var tickerDbContext = _database.CreateTickerDbContext();
        Assert.Equal(1, await tickerDbContext.Set<TimeTickerEntity>().CountAsync());
    }

    [Fact]
    public async Task FailedAddAfterConcurrentStageCompletion_DoesNotRegressSucceededState()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc));
        var (runId, stageId) = await CreateUnfinishedRunAsync(
            dbContext,
            IgdbSyncRunStatus.Running,
            IgdbSyncStageStatus.Running
        );
        var addStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var releaseAdd = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var manager = new FakeTimeTickerManager(_database.CreateTickerDbContext)
        {
            AddInterceptor = async (_, cancellationToken) =>
            {
                addStarted.TrySetResult(true);
                await releaseAdd.Task.WaitAsync(cancellationToken);
                return new InvalidOperationException("Simulated scheduling failure.");
            },
        };
        var recovery = CreateOrchestrator(dbContext, manager, clock).EnsureIncrementalAsync();

        await addStarted.Task.WaitAsync(TimeSpan.FromSeconds(10));
        var completedAt = clock.UtcNow.AddSeconds(1);
        try
        {
            await using var concurrentDbContext = _database.CreateDbContext();
            var run = await concurrentDbContext.IgdbSyncRuns.SingleAsync(value => value.Id == runId);
            var stage = await concurrentDbContext.IgdbSyncStages.SingleAsync(value => value.Id == stageId);
            run.Status = IgdbSyncRunStatus.Succeeded;
            run.StartedAt ??= completedAt;
            run.CompletedAt = completedAt;
            run.UpdatedAt = completedAt;
            run.Error = null;
            stage.Status = IgdbSyncStageStatus.Succeeded;
            stage.StartedAt ??= completedAt;
            stage.CompletedAt = completedAt;
            stage.UpdatedAt = completedAt;
            stage.Error = null;
            await concurrentDbContext.SaveChangesAsync();
        }
        finally
        {
            releaseAdd.TrySetResult(true);
        }

        await recovery;

        dbContext.ChangeTracker.Clear();
        var succeededRun = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        var succeededStage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.Id == stageId);
        Assert.Equal(IgdbSyncRunStatus.Succeeded, succeededRun.Status);
        Assert.Null(succeededRun.Error);
        Assert.Equal(completedAt, succeededRun.CompletedAt);
        Assert.Equal(IgdbSyncStageStatus.Succeeded, succeededStage.Status);
        Assert.Null(succeededStage.Error);
        Assert.Equal(123, succeededStage.PageCursor);
        Assert.Equal(2, succeededStage.PagesProcessed);
        Assert.Equal(1_000, succeededStage.RowsProcessed);
        Assert.Single(manager.Added);
        await using var tickerDbContext = _database.CreateTickerDbContext();
        Assert.Empty(await tickerDbContext.Set<TimeTickerEntity>().ToArrayAsync());
    }

    [Theory]
    [InlineData(IgdbSyncRunStatus.Pending, IgdbSyncStageStatus.Pending)]
    [InlineData(IgdbSyncRunStatus.Running, IgdbSyncStageStatus.Running)]
    [InlineData(IgdbSyncRunStatus.Failed, IgdbSyncStageStatus.Failed)]
    public async Task MissingTicker_RecoversEveryUnfinishedRunStateWithoutLosingProgress(
        IgdbSyncRunStatus runStatus,
        IgdbSyncStageStatus stageStatus
    )
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc));
        var (runId, stageId) = await CreateUnfinishedRunAsync(dbContext, runStatus, stageStatus);
        var manager = new FakeTimeTickerManager(_database.CreateTickerDbContext);

        await CreateOrchestrator(dbContext, manager, clock).EnsureIncrementalAsync();

        await AssertRecoveredAsync(dbContext, runId, stageId);
        Assert.Empty(manager.Deleted);
        Assert.Equal(stageId, Assert.Single(manager.Added).Id);
        await AssertActiveTickerAsync(stageId);
    }

    [Theory]
    [InlineData(IgdbSyncRunStatus.Pending, IgdbSyncStageStatus.Pending, TickerStatus.Done)]
    [InlineData(IgdbSyncRunStatus.Running, IgdbSyncStageStatus.Running, TickerStatus.Failed)]
    [InlineData(IgdbSyncRunStatus.Failed, IgdbSyncStageStatus.Failed, TickerStatus.Cancelled)]
    public async Task TerminalTicker_RecoversEveryUnfinishedRunStateWithoutLosingProgress(
        IgdbSyncRunStatus runStatus,
        IgdbSyncStageStatus stageStatus,
        TickerStatus tickerStatus
    )
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc));
        var (runId, stageId) = await CreateUnfinishedRunAsync(dbContext, runStatus, stageStatus);
        await InsertTickerAsync(stageId, tickerStatus);
        var manager = new FakeTimeTickerManager(_database.CreateTickerDbContext);

        await CreateOrchestrator(dbContext, manager, clock).EnsureIncrementalAsync();

        await AssertRecoveredAsync(dbContext, runId, stageId);
        Assert.Equal([stageId], manager.Deleted);
        Assert.Equal(stageId, Assert.Single(manager.Added).Id);
        await AssertActiveTickerAsync(stageId);
    }

    [Fact]
    public async Task ActiveTicker_IsNotReplacedOrReset()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var clock = new MutableDateTimeProvider(new DateTime(2026, 7, 9, 12, 0, 0, DateTimeKind.Utc));
        var (runId, stageId) = await CreateUnfinishedRunAsync(
            dbContext,
            IgdbSyncRunStatus.Running,
            IgdbSyncStageStatus.Running
        );
        await InsertTickerAsync(stageId, TickerStatus.InProgress);
        var manager = new FakeTimeTickerManager(_database.CreateTickerDbContext);

        await CreateOrchestrator(dbContext, manager, clock).EnsureIncrementalAsync();

        dbContext.ChangeTracker.Clear();
        var run = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        var stage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.Id == stageId);
        Assert.Equal(IgdbSyncRunStatus.Running, run.Status);
        Assert.Equal(IgdbSyncStageStatus.Running, stage.Status);
        Assert.Equal(123, stage.PageCursor);
        Assert.Empty(manager.Added);
        Assert.Empty(manager.Deleted);
    }

    private CatalogSyncOrchestrator CreateOrchestrator(
        ApplicationDbContext dbContext,
        FakeTimeTickerManager manager,
        IDateTimeProvider clock
    )
    {
        return new CatalogSyncOrchestrator(
            dbContext,
            new TestTickerDbContextFactory(_database.CreateTickerDbContext),
            manager,
            clock,
            NullLogger<CatalogSyncOrchestrator>.Instance
        );
    }

    private static async Task MarkReadyToCompleteAsync(ApplicationDbContext dbContext, Guid runId, DateTime completedAt)
    {
        var stages = await dbContext
            .IgdbSyncStages.Where(value => value.RunId == runId && value.Kind != IgdbSyncStageKind.Complete)
            .ToArrayAsync();
        foreach (var stage in stages)
        {
            stage.Status = IgdbSyncStageStatus.Succeeded;
            stage.StartedAt ??= completedAt;
            stage.CompletedAt = completedAt;
            stage.UpdatedAt = completedAt;
            stage.Error = null;
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task<(Guid RunId, Guid StageId)> CreateUnfinishedRunAsync(
        ApplicationDbContext dbContext,
        IgdbSyncRunStatus runStatus,
        IgdbSyncStageStatus stageStatus
    )
    {
        var createdAt = new DateTime(2026, 7, 9, 10, 0, 0, DateTimeKind.Utc);
        var run = new IgdbSyncRun
        {
            Mode = IgdbSyncRunMode.Incremental,
            Status = runStatus,
            From = createdAt.AddDays(-1),
            Through = createdAt,
            Error = runStatus == IgdbSyncRunStatus.Failed ? "Previous run failure" : null,
            CreatedAt = createdAt,
            StartedAt = runStatus == IgdbSyncRunStatus.Pending ? null : createdAt,
            UpdatedAt = createdAt,
        };
        var stage = new IgdbSyncStage
        {
            Run = run,
            Kind = IgdbSyncStageKind.Genres,
            Order = 1,
            Status = stageStatus,
            PageCursor = 123,
            PagesProcessed = 2,
            RowsProcessed = 1_000,
            Error = stageStatus == IgdbSyncStageStatus.Failed ? "Previous stage failure" : null,
            CreatedAt = createdAt,
            StartedAt = stageStatus == IgdbSyncStageStatus.Pending ? null : createdAt,
            UpdatedAt = createdAt,
        };
        dbContext.AddRange(run, stage);
        await dbContext.SaveChangesAsync();
        return (run.Id, stage.Id);
    }

    private async Task InsertTickerAsync(Guid stageId, TickerStatus status)
    {
        await using var tickerDbContext = _database.CreateTickerDbContext();
        var ticker = new TimeTickerEntity
        {
            Id = stageId,
            Function = GenresFunction,
            Description = "Existing ticker",
            ExecutionTime = new DateTime(2026, 7, 9, 10, 0, 0, DateTimeKind.Utc),
        };
        tickerDbContext.Add(ticker);
        tickerDbContext.Entry(ticker).Property<TickerStatus>(nameof(TimeTickerEntity.Status)).CurrentValue = status;
        await tickerDbContext.SaveChangesAsync();
    }

    private static async Task AssertRecoveredAsync(ApplicationDbContext dbContext, Guid runId, Guid stageId)
    {
        dbContext.ChangeTracker.Clear();
        var run = await dbContext.IgdbSyncRuns.AsNoTracking().SingleAsync(value => value.Id == runId);
        var stage = await dbContext.IgdbSyncStages.AsNoTracking().SingleAsync(value => value.Id == stageId);
        Assert.Equal(IgdbSyncRunStatus.Pending, run.Status);
        Assert.Null(run.Error);
        Assert.Equal(IgdbSyncStageStatus.Pending, stage.Status);
        Assert.Null(stage.Error);
        Assert.Equal(123, stage.PageCursor);
        Assert.Equal(2, stage.PagesProcessed);
        Assert.Equal(1_000, stage.RowsProcessed);
        Assert.Equal(1, await dbContext.IgdbSyncRuns.CountAsync());
    }

    private async Task AssertActiveTickerAsync(Guid stageId)
    {
        await using var tickerDbContext = _database.CreateTickerDbContext();
        var ticker = await tickerDbContext
            .Set<TimeTickerEntity>()
            .AsNoTracking()
            .SingleAsync(value => value.Id == stageId);
        Assert.Equal(TickerStatus.Idle, ticker.Status);
        Assert.Equal(GenresFunction, ticker.Function);
    }

    private static DateTime CaptureExpectedThrough(DateTime now)
    {
        var laggedNow = now.AddMinutes(-5);
        return new DateTime(laggedNow.Ticks - (laggedNow.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Utc);
    }

    private sealed class MutableDateTimeProvider : IDateTimeProvider
    {
        public MutableDateTimeProvider(DateTime utcNow)
        {
            UtcNow = utcNow;
        }

        public DateTime Now => UtcNow.ToLocalTime();

        public DateTime UtcNow { get; set; }

        public DateTimeOffset OffsetNow => new(Now);

        public DateTimeOffset OffsetUtcNow => new(UtcNow);
    }
}
