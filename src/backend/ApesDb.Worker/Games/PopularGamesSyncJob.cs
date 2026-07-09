using ApesDb.Igdb.Sdk;
using TickerQ.Utilities.Base;

namespace ApesDb.Worker.Games;

public sealed class PopularGamesSyncJob
{
    public const string FunctionName = "sync-popular-games";
    public const int CatalogSize = 1000;

    private static readonly SemaphoreSlim ExecutionGate = new(1, 1);

    private readonly IIgdbService _igdbService;
    private readonly IPopularGamesCatalogImporter _importer;
    private readonly ILogger<PopularGamesSyncJob> _logger;

    public PopularGamesSyncJob(
        IIgdbService igdbService,
        IPopularGamesCatalogImporter importer,
        ILogger<PopularGamesSyncJob> logger
    )
    {
        _igdbService = igdbService;
        _importer = importer;
        _logger = logger;
    }

    [TickerFunction(FunctionName, cronExpression: "0 * * * *", maxConcurrency: 1)]
    public async Task SyncAsync(TickerFunctionContext context, CancellationToken cancellationToken)
    {
        context.CronOccurrenceOperations?.SkipIfAlreadyRunning();

        if (!await ExecutionGate.WaitAsync(0, cancellationToken))
        {
            _logger.LogInformation("Skipping the popular-games import because an import is already running.");
            return;
        }

        try
        {
            _logger.LogInformation("Starting the IGDB popular-games import for {CatalogSize} games.", CatalogSize);

            var catalog = await _igdbService.FetchPopularCatalogAsync(CatalogSize, cancellationToken);
            if (catalog.PopularGames.Count != CatalogSize)
            {
                throw new InvalidDataException(
                    $"IGDB returned {catalog.PopularGames.Count} popular games; expected {CatalogSize}."
                );
            }

            await _importer.ImportAsync(catalog, cancellationToken);

            _logger.LogInformation(
                "Completed the IGDB popular-games import with {PopularGameCount} ranked games and {GameCount} total games.",
                catalog.PopularGames.Count,
                catalog.Games.Count
            );
        }
        finally
        {
            ExecutionGate.Release();
        }
    }
}
