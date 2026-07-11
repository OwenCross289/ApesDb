namespace ApesDb.Worker.Games;

public interface IPopularitySynchronizer
{
    Task RefreshAsync(bool allowDuringCatalogRun, CancellationToken cancellationToken = default);
}
