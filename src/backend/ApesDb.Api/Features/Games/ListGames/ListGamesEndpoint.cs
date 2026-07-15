using System.ComponentModel;
using ApesDb.Api.Features.Games.Types;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Games.ListGames;

public sealed class ListGamesEndpoint : Endpoint<ListGamesRequest, Pagable<ListGameResponse>>
{
    private const string CoopGameModeSlug = "co-operative";
    private const long ReleasedStatusId = 0;
    private const long SteamSourceId = 1;

    private readonly ApplicationDbContext _dbContext;

    public ListGamesEndpoint(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Games.List);
        Summary(summary =>
        {
            summary.Summary = "Lists and filters synchronized games.";
            summary.Description = "Returns an alphabetized, paginated view of the synchronized game catalog.";
        });
    }

    public override async Task HandleAsync(ListGamesRequest request, CancellationToken ct)
    {
        var baseQuery = _dbContext.Games.AsNoTracking().Where(game => game.VersionParentId == null);
        var query = baseQuery
            .WhereContains(request.GameTypeIds, game => game.GameTypeId)
            .WhereContains(request.GameStatusIds, game => game.GameStatusId ?? ReleasedStatusId)
            .WhereContains(
                request.GenreIds,
                game => _dbContext.GameGenres.Where(link => link.GameId == game.Id).Select(link => link.GenreId)
            )
            .WhereContains(
                request.ThemeIds,
                game => _dbContext.GameThemes.Where(link => link.GameId == game.Id).Select(link => link.ThemeId)
            )
            .WhereContains(
                request.GameModeIds,
                game => _dbContext.GameGameModes.Where(link => link.GameId == game.Id).Select(link => link.GameModeId)
            )
            .WhereContains(
                request.PlayerPerspectiveIds,
                game =>
                    _dbContext
                        .GamePlayerPerspectives.Where(link => link.GameId == game.Id)
                        .Select(link => link.PlayerPerspectiveId)
            )
            .WhereContains(
                request.PlatformIds,
                game => _dbContext.GamePlatforms.Where(link => link.GameId == game.Id).Select(link => link.PlatformId)
            )
            .WhereContains(
                request.Developer,
                game =>
                    _dbContext
                        .GameCompanies.Where(link => link.GameId == game.Id && link.Developer)
                        .Select(link => link.Company.Name)
            )
            .WhereContains(
                request.Publisher,
                game =>
                    _dbContext
                        .GameCompanies.Where(link => link.GameId == game.Id && link.Publisher)
                        .Select(link => link.Company.Name)
            )
            .WhereContains(
                request.Collection,
                game =>
                    _dbContext
                        .GameCollections.Where(link => link.GameId == game.Id)
                        .Select(link => link.Collection.Name)
            )
            .WhereContains(
                request.Franchise,
                game =>
                    _dbContext.GameFranchises.Where(link => link.GameId == game.Id).Select(link => link.Franchise.Name)
            )
            .WhereContains(request.Search, game => game.Name)
            .WhereEqual(
                request.IsCoop,
                game =>
                    _dbContext.GameGameModes.Any(link =>
                        link.GameId == game.Id && link.GameMode.Slug == CoopGameModeSlug
                    )
            )
            .WhereEqual(
                request.IsSteam,
                game =>
                    _dbContext.ExternalGames.Any(identifier =>
                        identifier.GameId == game.Id && identifier.ExternalGameSourceId == SteamSourceId
                    )
            );

        var total = await Total(baseQuery, ct);
        var filteredTotal = await FilteredTotal(query, ct);
        var items = await Query(query, request, ct);
        await Send.OkAsync(
            new Pagable<ListGameResponse>(items, total, filteredTotal, request.Page, request.PageSize),
            ct
        );
    }

    private static Task<int> Total(IQueryable<Game> query, CancellationToken ct)
    {
        return query.CountAsync(ct);
    }

    private static Task<int> FilteredTotal(IQueryable<Game> query, CancellationToken ct)
    {
        return query.CountAsync(ct);
    }

    private Task<ListGameResponse[]> Query(IQueryable<Game> query, ListGamesRequest request, CancellationToken ct)
    {
        return query
            .SortBy(ListSortDirection.Ascending, game => game.Name.ToLower(), game => game.Name, game => game.Id)
            .Page(request.Page, request.PageSize)
            .Select(game => new ListGameResponse(
                game.Id,
                game.CoverSmallUrl,
                game.Name,
                _dbContext
                    .GameCompanies.Where(link => link.GameId == game.Id && link.Developer)
                    .Select(link => link.Company.Name)
                    .Distinct()
                    .OrderBy(name => name.ToLower())
                    .ThenBy(name => name)
                    .ToArray(),
                _dbContext
                    .GameCompanies.Where(link => link.GameId == game.Id && link.Publisher)
                    .Select(link => link.Company.Name)
                    .Distinct()
                    .OrderBy(name => name.ToLower())
                    .ThenBy(name => name)
                    .ToArray(),
                _dbContext.GameGameModes.Any(link => link.GameId == game.Id && link.GameMode.Slug == CoopGameModeSlug),
                _dbContext.ExternalGames.Any(identifier =>
                    identifier.GameId == game.Id && identifier.ExternalGameSourceId == SteamSourceId
                ),
                _dbContext
                    .GameTypes.Where(gameType => gameType.Id == game.GameTypeId)
                    .Select(gameType => new GameTypeResponse(gameType.Id, gameType.Name))
                    .FirstOrDefault()
            ))
            .ToArrayAsync(ct);
    }
}
