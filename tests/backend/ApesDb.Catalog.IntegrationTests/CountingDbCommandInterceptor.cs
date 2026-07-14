using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApesDb.Catalog.IntegrationTests;

internal sealed class CountingDbCommandInterceptor : DbCommandInterceptor
{
    private int _commandCount;

    public int CommandCount => _commandCount;

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result
    )
    {
        Interlocked.Increment(ref _commandCount);
        return result;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default
    )
    {
        Interlocked.Increment(ref _commandCount);
        return ValueTask.FromResult(result);
    }
}
