namespace ApesDb.Worker.Games;

public sealed class InitialCatalogSyncScheduler : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InitialCatalogSyncScheduler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var orchestrator = scope.ServiceProvider.GetRequiredService<ICatalogSyncOrchestrator>();
        await orchestrator.EnsureBootstrapAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
