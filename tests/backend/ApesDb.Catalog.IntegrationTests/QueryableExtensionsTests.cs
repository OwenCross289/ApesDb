using System.ComponentModel;
using ApesDb.Api.Features.Games.ListGames;
using ApesDb.Api.Features.Games.Types;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed partial class CatalogStageRunnerTests
{
    [Fact]
    public async Task SortBy_OrdersDatabaseQueriesByOneTwoOrThreePropertiesInEitherDirection()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        var firstDate = SynchronizedAt.AddDays(-1);
        var secondDate = SynchronizedAt;
        dbContext.GameTypes.AddRange(
            CreateGameType(1, "B", firstDate),
            CreateGameType(2, "A", firstDate),
            CreateGameType(3, "A", secondDate),
            CreateGameType(4, "A", firstDate)
        );
        await dbContext.SaveChangesAsync();

        Assert.Equal(
            new long[] { 1, 2, 3, 4 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Ascending, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 4, 3, 2, 1 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Descending, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 2, 3, 4, 1 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Ascending, value => value.Name, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 1, 4, 3, 2 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Descending, value => value.Name, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 2, 4, 1, 3 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Ascending, value => value.CreatedAt, value => value.Name, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 3, 1, 4, 2 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Descending, value => value.CreatedAt, value => value.Name, value => value.Id)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 3, 4 },
            await dbContext
                .GameTypes.AsNoTracking()
                .SortBy(ListSortDirection.Ascending, value => value.Id)
                .Page(2, 2)
                .Select(value => value.Id)
                .ToArrayAsync()
        );
    }

    [Fact]
    public async Task ConditionalWhereHelpers_FilterDatabaseQueriesAndIgnoreMissingValues()
    {
        await _database.ResetAsync();
        await using var dbContext = _database.CreateDbContext();
        dbContext.GameTypes.AddRange(
            CreateGameType(1, "First", SynchronizedAt),
            CreateGameType(2, "Second", SynchronizedAt)
        );
        var alpha = CreateStoredGame(101, "Alpha");
        alpha.GameTypeId = 1;
        var beta = CreateStoredGame(102, "Beta");
        beta.GameTypeId = 2;
        var gamma = CreateStoredGame(103, "Gamma");
        dbContext.Games.AddRange(alpha, beta, gamma);
        await dbContext.SaveChangesAsync();

        Assert.Equal(
            new long[] { 101 },
            await dbContext
                .Games.AsNoTracking()
                .WhereContains([1L], game => game.GameTypeId)
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 101, 102 },
            await dbContext
                .Games.AsNoTracking()
                .WhereContains([1L, 2L], game => game.GameTypeId)
                .SortBy(ListSortDirection.Ascending, game => game.Id)
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 103 },
            await dbContext
                .Games.AsNoTracking()
                .WhereEqual(false, game => game.GameTypeId.HasValue)
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 101 },
            await dbContext
                .Games.AsNoTracking()
                .WhereEqual(1L, game => game.GameTypeId)
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 101 },
            await dbContext
                .Games.AsNoTracking()
                .WhereEqual("Alpha", game => game.Name)
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 103 },
            await dbContext
                .Games.AsNoTracking()
                .WherePredicate("amm", value => game => EF.Functions.ILike(game.Name, $"%{value}%"))
                .Select(game => game.Id)
                .ToArrayAsync()
        );
        Assert.Equal(
            new long[] { 102 },
            await dbContext
                .Games.AsNoTracking()
                .WherePredicate((long?)102, id => game => game.Id == id)
                .Select(game => game.Id)
                .ToArrayAsync()
        );

        long[]? missingIds = null;
        bool? missingEquality = null;
        string? missingPredicate = null;
        Assert.Equal(3, await dbContext.Games.WhereContains(missingIds, game => game.GameTypeId).CountAsync());
        Assert.Equal(3, await dbContext.Games.WhereContains(Array.Empty<long>(), game => game.GameTypeId).CountAsync());
        Assert.Equal(
            3,
            await dbContext.Games.WhereEqual(missingEquality, game => game.GameTypeId.HasValue).CountAsync()
        );
        Assert.Equal(
            3,
            await dbContext
                .Games.WherePredicate(missingPredicate, value => game => EF.Functions.ILike(game.Name, $"%{value}%"))
                .CountAsync()
        );
    }

    private static GameType CreateGameType(long id, string name, DateTime createdAt)
    {
        return new GameType
        {
            Id = id,
            Name = name,
            CreatedAt = createdAt,
            UpdatedAt = SynchronizedAt,
            LastSyncedAt = SynchronizedAt,
        };
    }
}

public sealed class QueryableExtensionsTranslationTests
{
    [Fact]
    public void QueryHelpers_CreateTranslatablePostgreSqlExpressions()
    {
        using var dbContext = CreateDbContext();

        var sql = dbContext
            .Games.WhereContains([1L], game => game.GameTypeId)
            .WhereEqual(1L, game => game.GameTypeId)
            .WhereEqual(true, game => game.GameTypeId.HasValue)
            .WherePredicate("amm", value => game => EF.Functions.ILike(game.Name, $"%{value}%"))
            .SortBy(ListSortDirection.Ascending, game => game.Name.ToLower(), game => game.Name, game => game.Id)
            .Page(2, 10)
            .ToQueryString();

        Assert.Contains("ILIKE", sql);
        Assert.Contains("ORDER BY", sql);
        Assert.Contains("OFFSET", sql);

        var containsSql = dbContext
            .Games.WhereContains(
                [1L],
                game => dbContext.GameGenres.Where(link => link.GameId == game.Id).Select(link => link.GenreId)
            )
            .WhereContains(
                @"%_\*",
                game =>
                    dbContext
                        .GameCompanies.Where(link => link.GameId == game.Id && link.Developer)
                        .Select(link => link.Company.Name)
            )
            .WhereContains(@"%_\*", game => game.Name)
            .ToQueryString();

        Assert.Contains("EXISTS", containsSql);
        Assert.Contains("lower", containsSql);

        var projectionSql = dbContext
            .Games.SortBy(ListSortDirection.Ascending, game => game.Name.ToLower(), game => game.Name, game => game.Id)
            .Page(2, 10)
            .Select(game => new ListGameResponse(
                game.Id,
                game.CoverSmallUrl,
                game.Name,
                dbContext
                    .GameCompanies.Where(link => link.GameId == game.Id && link.Developer)
                    .Select(link => link.Company.Name)
                    .Distinct()
                    .OrderBy(name => name.ToLower())
                    .ThenBy(name => name)
                    .ToArray(),
                dbContext
                    .GameCompanies.Where(link => link.GameId == game.Id && link.Publisher)
                    .Select(link => link.Company.Name)
                    .Distinct()
                    .OrderBy(name => name.ToLower())
                    .ThenBy(name => name)
                    .ToArray(),
                dbContext.GameGameModes.Any(link => link.GameId == game.Id),
                dbContext.ExternalGames.Any(link => link.GameId == game.Id),
                dbContext
                    .GameTypes.Where(gameType => gameType.Id == game.GameTypeId)
                    .Select(gameType => new GameTypeResponse(gameType.Id, gameType.Name))
                    .FirstOrDefault()
            ))
            .ToQueryString();

        Assert.Contains("LEFT JOIN", projectionSql);
        Assert.Contains("OFFSET", projectionSql);
    }

    [Fact]
    public void SortBy_RejectsUnsupportedDirections()
    {
        using var dbContext = CreateDbContext();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            dbContext.GameTypes.SortBy((ListSortDirection)int.MaxValue, value => value.Id)
        );
        Assert.Throws<ArgumentOutOfRangeException>(() => dbContext.GameTypes.Page(0, 10));
        Assert.Throws<ArgumentOutOfRangeException>(() => dbContext.GameTypes.Page(1, 0));
    }

    [Fact]
    public void WhereContains_IgnoresEmptyCollectionsAndWhitespace()
    {
        using var dbContext = CreateDbContext();
        var query = dbContext.Games.AsQueryable();

        Assert.Same(
            query,
            query.WhereContains(
                Array.Empty<long>(),
                game => dbContext.GameGenres.Where(link => link.GameId == game.Id).Select(link => link.GenreId)
            )
        );
        Assert.Same(query, query.WhereContains("  ", game => game.Name));
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Host=localhost;Database=apesdb;Username=apesdb;Password=apesdb")
            .Options;
        return new ApplicationDbContext(options);
    }
}
