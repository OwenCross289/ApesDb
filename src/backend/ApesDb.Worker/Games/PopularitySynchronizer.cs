using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Games;
using ApesDb.Domain.Entities.IgdbSync;
using ApesDb.Igdb.Sdk;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Worker.Games;

public sealed class PopularitySynchronizer : IPopularitySynchronizer
{
    private const int TargetCount = 1000;
    private const int PageSize = 500;
    private const string VisitsPopularityTypeName = "Visits";

    private readonly ApplicationDbContext _dbContext;
    private readonly IIgdbService _igdbService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<PopularitySynchronizer> _logger;

    public PopularitySynchronizer(
        ApplicationDbContext dbContext,
        IIgdbService igdbService,
        IDateTimeProvider dateTimeProvider,
        ILogger<PopularitySynchronizer> logger
    )
    {
        _dbContext = dbContext;
        _igdbService = igdbService;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task RefreshAsync(bool allowDuringCatalogRun, CancellationToken cancellationToken = default)
    {
        if (
            !allowDuringCatalogRun
            && await _dbContext.IgdbSyncRuns.AnyAsync(
                run => run.Status != IgdbSyncRunStatus.Succeeded,
                cancellationToken
            )
        )
        {
            _logger.LogInformation("Skipping the hourly popularity refresh while a catalog sync is active.");
            return;
        }

        var popularityType = await _dbContext
            .PopularityTypes.AsNoTracking()
            .Where(value => value.Name == VisitsPopularityTypeName)
            .OrderBy(value => value.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (popularityType is null)
        {
            if (allowDuringCatalogRun)
            {
                throw new InvalidDataException("IGDB did not provide the Visits popularity type.");
            }

            _logger.LogInformation(
                "Skipping popularity refresh because the Visits popularity type is not synchronized."
            );
            return;
        }

        var synchronizedAt = _dateTimeProvider.UtcNow;
        var popularity = new List<PopularGame>(TargetCount);
        var seenGames = new HashSet<long>();
        var offset = 0;
        while (popularity.Count < TargetCount)
        {
            var page = await _igdbService.FetchPopularityPrimitivesPageAsync(
                popularityType.Id,
                offset,
                cancellationToken
            );
            if (page.Count == 0)
            {
                break;
            }

            var candidateGameIds = page.Select(value => value.GameId).Distinct().ToArray();
            var storedGameIds = await _dbContext
                .Games.AsNoTracking()
                .Where(value => candidateGameIds.Contains(value.Id))
                .Select(value => value.Id)
                .ToHashSetAsync(cancellationToken);

            for (var index = 0; index < page.Count && popularity.Count < TargetCount; index++)
            {
                var primitive = page[index];
                if (!storedGameIds.Contains(primitive.GameId) || !seenGames.Add(primitive.GameId))
                {
                    continue;
                }

                popularity.Add(
                    new PopularGame
                    {
                        Id = primitive.Id,
                        GameId = primitive.GameId,
                        Rank = popularity.Count + 1,
                        SourceRank = offset + index + 1,
                        Score = primitive.Value,
                        PopularityTypeId = primitive.PopularityTypeId,
                        CalculatedAt = primitive.CalculatedAt?.UtcDateTime ?? synchronizedAt,
                        IgdbUpdatedAt = primitive.UpdatedAt?.UtcDateTime,
                        Checksum = primitive.Checksum,
                        SyncedAt = synchronizedAt,
                    }
                );
            }

            offset += page.Count;
            if (page.Count < PageSize)
            {
                break;
            }
        }

        if (popularity.Count == 0)
        {
            throw new InvalidDataException("IGDB returned no popularity rows for synchronized games.");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        await _dbContext.PopularGames.ExecuteDeleteAsync(cancellationToken);
        await _dbContext.BulkInsertAsync(popularity, cancellationToken: cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        _logger.LogInformation("Refreshed {Count} IGDB popularity rows.", popularity.Count);
    }
}
