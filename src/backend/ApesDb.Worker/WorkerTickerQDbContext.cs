using Microsoft.EntityFrameworkCore;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.Utilities.Entities;

namespace ApesDb.Worker;

public sealed class WorkerTickerQDbContext : TickerQDbContext<TimeTickerEntity, CronTickerEntity>
{
    public const string Schema = "worker";

    public WorkerTickerQDbContext(DbContextOptions<WorkerTickerQDbContext> options)
        : base(options) { }
}
