using System.Reflection;
using ApesDb.Worker;
using Microsoft.EntityFrameworkCore;
using TickerQ.Utilities.Entities;
using TickerQ.Utilities.Interfaces;
using TickerQ.Utilities.Interfaces.Managers;
using TickerQ.Utilities.Models;

namespace ApesDb.Catalog.IntegrationTests;

internal sealed class FakeTimeTickerManager : ITimeTickerManager<TimeTickerEntity>
{
    private readonly Func<WorkerTickerQDbContext> _createDbContext;

    public FakeTimeTickerManager(Func<WorkerTickerQDbContext> createDbContext)
    {
        _createDbContext = createDbContext;
    }

    public List<TimeTickerEntity> Added { get; } = [];

    public List<Guid> Deleted { get; } = [];

    public Func<TimeTickerEntity, CancellationToken, Task<Exception?>>? AddInterceptor { get; set; }

    public async Task<TickerResult<TimeTickerEntity>> AddAsync(
        TimeTickerEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        Added.Add(entity);
        if (AddInterceptor is not null)
        {
            var interceptedFailure = await AddInterceptor(entity, cancellationToken);
            if (interceptedFailure is not null)
            {
                return Failure<TimeTickerEntity>(interceptedFailure);
            }
        }

        try
        {
            await using var dbContext = _createDbContext();
            dbContext.Set<TimeTickerEntity>().Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Success(entity);
        }
        catch (Exception exception)
        {
            return Failure<TimeTickerEntity>(exception);
        }
    }

    public Task<TickerResult<TimeTickerEntity>> UpdateAsync(
        TimeTickerEntity timeTicker,
        CancellationToken cancellationToken = default
    ) => Task.FromResult(Success(timeTicker));

    public async Task<TickerResult<TimeTickerEntity>> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        Deleted.Add(id);
        await using var dbContext = _createDbContext();
        await dbContext.Set<TimeTickerEntity>().Where(value => value.Id == id).ExecuteDeleteAsync(cancellationToken);
        return Success(new TimeTickerEntity { Id = id });
    }

    public Task<TickerResult<List<TimeTickerEntity>>> AddBatchAsync(
        List<TimeTickerEntity> entities,
        CancellationToken cancellationToken = default
    ) => Task.FromResult(Success(entities));

    public Task<TickerResult<List<TimeTickerEntity>>> UpdateBatchAsync(
        List<TimeTickerEntity> timeTickers,
        CancellationToken cancellationToken = default
    ) => Task.FromResult(Success(timeTickers));

    public Task<TickerResult<TimeTickerEntity>> DeleteBatchAsync(
        List<Guid> ids,
        CancellationToken cancellationToken = default
    ) => Task.FromResult(Success(new TimeTickerEntity()));

    public Task<TickerResult<TimeTickerEntity>> AddAsync<TFunction>(
        DateTime? executionTime = null,
        CancellationToken cancellationToken = default
    )
        where TFunction : class, ITickerFunction => throw new NotSupportedException();

    public Task<TickerResult<TimeTickerEntity>> AddAsync<TFunction, TRequest>(
        DateTime? executionTime,
        TRequest request,
        CancellationToken cancellationToken = default
    )
        where TFunction : class, ITickerFunction<TRequest> => throw new NotSupportedException();

    private static TickerResult<TEntity> Success<TEntity>(TEntity entity)
        where TEntity : class
    {
        var constructor = typeof(TickerResult<TEntity>).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            [typeof(TEntity)],
            modifiers: null
        );
        return (TickerResult<TEntity>)constructor!.Invoke([entity]);
    }

    private static TickerResult<TEntity> Failure<TEntity>(Exception exception)
        where TEntity : class
    {
        var constructor = typeof(TickerResult<TEntity>).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            [typeof(Exception)],
            modifiers: null
        );
        return (TickerResult<TEntity>)constructor!.Invoke([exception]);
    }
}

internal sealed class TestTickerDbContextFactory : IDbContextFactory<WorkerTickerQDbContext>
{
    private readonly Func<WorkerTickerQDbContext> _createDbContext;

    public TestTickerDbContextFactory(Func<WorkerTickerQDbContext> createDbContext)
    {
        _createDbContext = createDbContext;
    }

    public WorkerTickerQDbContext CreateDbContext() => _createDbContext();

    public Task<WorkerTickerQDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(_createDbContext());
}
