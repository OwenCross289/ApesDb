using ApesDb.Domain;
using ApesDb.Domain.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Features.Games.ListGames;

public sealed class ListGamesEndpoint : Endpoint<ListGamesRequest, ListGamesResponse>
{
    private const string CoopGameModeSlug = "co-operative";
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
        var query = ApplyFilters(_dbContext.Games.AsNoTracking(), request);
        var totalCount = await query.CountAsync(ct);
        var skip = (long)(request.Page - 1) * request.PageSize;
        var games =
            skip >= totalCount
                ? []
                : await query
                    .OrderBy(game => game.Name.ToLower())
                    .ThenBy(game => game.Name)
                    .ThenBy(game => game.Id)
                    .Skip((int)skip)
                    .Take(request.PageSize)
                    .Select(game => new GameProjection(
                        game.Id,
                        game.Id,
                        game.CoverSmallUrl,
                        game.Name,
                        _dbContext.GameGameModes.Any(link =>
                            link.GameId == game.Id && link.GameMode.Slug == CoopGameModeSlug
                        ),
                        _dbContext.ExternalGames.Any(identifier =>
                            identifier.GameId == game.Id && identifier.ExternalGameSourceId == SteamSourceId
                        ),
                        _dbContext.GameRelations.Any(relation =>
                            relation.RelatedGameId == game.Id
                            && relation.RelationType == GameRelationType.StandaloneExpansion
                        ),
                        _dbContext.GameRelations.Any(relation =>
                            relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.Expansion
                        ),
                        _dbContext.GameRelations.Any(relation =>
                            relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.Dlc
                        )
                    ))
                    .ToArrayAsync(ct);

        var gameIds = games.Select(game => game.InternalId).ToArray();
        var companies =
            gameIds.Length == 0
                ? []
                : await _dbContext
                    .GameCompanies.AsNoTracking()
                    .Where(value => gameIds.Contains(value.GameId) && (value.Developer || value.Publisher))
                    .Select(value => new GameCompanyProjection(
                        value.GameId,
                        value.Company.Name,
                        value.Developer,
                        value.Publisher
                    ))
                    .ToArrayAsync(ct);

        var response = new ListGamesResponse(
            games
                .Select(game => new ListGameResponse(
                    game.Id,
                    game.CoverSmallUrl,
                    game.Name,
                    ListCompanyNames(companies, game.InternalId, value => value.Developer),
                    ListCompanyNames(companies, game.InternalId, value => value.Publisher),
                    game.IsCoop,
                    game.IsSteam,
                    GetKind(game)
                ))
                .ToArray(),
            request.Page,
            request.PageSize,
            totalCount
        );

        await Send.OkAsync(response, ct);
    }

    private IQueryable<Game> ApplyFilters(IQueryable<Game> query, ListGamesRequest request)
    {
        if (NormalizeIds(request.GameTypeIds) is { Length: > 0 } gameTypeIds)
        {
            query = query.Where(game => game.GameType != null && gameTypeIds.Contains(game.GameType.Id));
        }

        if (NormalizeIds(request.GameStatusIds) is { Length: > 0 } gameStatusIds)
        {
            var includesReleased = gameStatusIds.Contains(0);
            query = query.Where(game =>
                (includesReleased && game.GameStatusId == null)
                || (game.GameStatus != null && gameStatusIds.Contains(game.GameStatus.Id))
            );
        }

        if (NormalizeIds(request.GenreIds) is { Length: > 0 } genreIds)
        {
            query = query.Where(game =>
                _dbContext.GameGenres.Any(link => link.GameId == game.Id && genreIds.Contains(link.Genre.Id))
            );
        }

        if (NormalizeIds(request.ThemeIds) is { Length: > 0 } themeIds)
        {
            query = query.Where(game =>
                _dbContext.GameThemes.Any(link => link.GameId == game.Id && themeIds.Contains(link.Theme.Id))
            );
        }

        if (NormalizeIds(request.GameModeIds) is { Length: > 0 } gameModeIds)
        {
            query = query.Where(game =>
                _dbContext.GameGameModes.Any(link => link.GameId == game.Id && gameModeIds.Contains(link.GameMode.Id))
            );
        }

        if (NormalizeIds(request.PlayerPerspectiveIds) is { Length: > 0 } perspectiveIds)
        {
            query = query.Where(game =>
                _dbContext.GamePlayerPerspectives.Any(link =>
                    link.GameId == game.Id && perspectiveIds.Contains(link.PlayerPerspective.Id)
                )
            );
        }

        if (NormalizeIds(request.PlatformIds) is { Length: > 0 } platformIds)
        {
            query = query.Where(game =>
                _dbContext.GamePlatforms.Any(link => link.GameId == game.Id && platformIds.Contains(link.Platform.Id))
            );
        }

        if (BuildContainsPattern(request.Developer) is { } developerPattern)
        {
            query = query.Where(game =>
                _dbContext.GameCompanies.Any(link =>
                    link.GameId == game.Id
                    && link.Developer
                    && EF.Functions.ILike(link.Company.Name, developerPattern, "\\")
                )
            );
        }

        if (BuildContainsPattern(request.Publisher) is { } publisherPattern)
        {
            query = query.Where(game =>
                _dbContext.GameCompanies.Any(link =>
                    link.GameId == game.Id
                    && link.Publisher
                    && EF.Functions.ILike(link.Company.Name, publisherPattern, "\\")
                )
            );
        }

        if (BuildContainsPattern(request.Collection) is { } collectionPattern)
        {
            query = query.Where(game =>
                _dbContext.GameCollections.Any(link =>
                    link.GameId == game.Id && EF.Functions.ILike(link.Collection.Name, collectionPattern, "\\")
                )
            );
        }

        if (BuildContainsPattern(request.Franchise) is { } franchisePattern)
        {
            query = query.Where(game =>
                _dbContext.GameFranchises.Any(link =>
                    link.GameId == game.Id && EF.Functions.ILike(link.Franchise.Name, franchisePattern, "\\")
                )
            );
        }

        if (BuildContainsPattern(request.Search) is { } searchPattern)
        {
            query = query.Where(game => EF.Functions.ILike(game.Name, searchPattern, "\\"));
        }

        if (request.IsCoop is { } isCoop)
        {
            query = query.Where(game =>
                _dbContext.GameGameModes.Any(link => link.GameId == game.Id && link.GameMode.Slug == CoopGameModeSlug)
                == isCoop
            );
        }

        if (request.IsSteam is { } isSteam)
        {
            query = query.Where(game =>
                _dbContext.ExternalGames.Any(identifier =>
                    identifier.GameId == game.Id && identifier.ExternalGameSourceId == SteamSourceId
                ) == isSteam
            );
        }

        return ApplyGameKindFilter(query, request.GameKinds);
    }

    private IQueryable<Game> ApplyGameKindFilter(IQueryable<Game> query, IEnumerable<string>? values)
    {
        var kinds = (values ?? [])
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => Enum.Parse<GameKind>(value, true))
            .ToHashSet();
        if (kinds.Count == 0)
        {
            return query;
        }

        var includeMain = kinds.Contains(GameKind.Main);
        var includeDlc = kinds.Contains(GameKind.Dlc);
        var includeExpansion = kinds.Contains(GameKind.Expansion);
        var includeStandaloneExpansion = kinds.Contains(GameKind.StandaloneExpansion);

        return query.Where(game =>
            (
                includeStandaloneExpansion
                && _dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.StandaloneExpansion
                )
            )
            || (
                includeExpansion
                && !_dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.StandaloneExpansion
                )
                && _dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.Expansion
                )
            )
            || (
                includeDlc
                && !_dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id
                    && (
                        relation.RelationType == GameRelationType.StandaloneExpansion
                        || relation.RelationType == GameRelationType.Expansion
                    )
                )
                && _dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id && relation.RelationType == GameRelationType.Dlc
                )
            )
            || (
                includeMain
                && !_dbContext.GameRelations.Any(relation =>
                    relation.RelatedGameId == game.Id
                    && (
                        relation.RelationType == GameRelationType.StandaloneExpansion
                        || relation.RelationType == GameRelationType.Expansion
                        || relation.RelationType == GameRelationType.Dlc
                    )
                )
            )
        );
    }

    private static long[] NormalizeIds(IEnumerable<long>? values)
    {
        return (values ?? []).Distinct().ToArray();
    }

    private static string? BuildContainsPattern(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var escaped = value.Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
        return $"%{escaped}%";
    }

    private static string[] ListCompanyNames(
        IEnumerable<GameCompanyProjection> companies,
        long gameId,
        Func<GameCompanyProjection, bool> roleSelector
    )
    {
        return companies
            .Where(value => value.GameId == gameId && roleSelector(value))
            .Select(value => value.Name)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ThenBy(value => value, StringComparer.Ordinal)
            .ToArray();
    }

    private static GameKind GetKind(GameProjection game)
    {
        if (game.IsStandaloneExpansion)
        {
            return GameKind.StandaloneExpansion;
        }

        if (game.IsExpansion)
        {
            return GameKind.Expansion;
        }

        return game.IsDlc ? GameKind.Dlc : GameKind.Main;
    }

    private sealed record GameProjection(
        long InternalId,
        long Id,
        string? CoverSmallUrl,
        string Name,
        bool IsCoop,
        bool IsSteam,
        bool IsStandaloneExpansion,
        bool IsExpansion,
        bool IsDlc
    );

    private sealed record GameCompanyProjection(long GameId, string Name, bool Developer, bool Publisher);
}
