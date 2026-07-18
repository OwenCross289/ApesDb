using ApesDb.Domain.Entities.IgdbSync;

namespace ApesDb.Worker.Games;

public interface ICatalogStageRunner
{
    Task RunAsync(
        Guid runId,
        IgdbSyncStageKind stageKind,
        int retryCount,
        CancellationToken cancellationToken = default
    );
}
