using ApesDb.Domain;
using ApesDb.Worker;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TickerQ.EntityFrameworkCore;
using TickerQ.Utilities.Entities;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed class CatalogDatabaseFixture : IAsyncLifetime
{
    private const string DatabaseName = "apesdb";
    private const string DatabaseUsername = "apesdb";
    private const string DatabasePassword = "apesdb";
    private const string PostgresNetworkAlias = "postgres";

    private readonly INetwork _network;
    private readonly PostgreSqlContainer _postgres;
    private readonly IContainer _flyway;
    private readonly ServiceProvider _tickerServices;

    public CatalogDatabaseFixture()
    {
        _network = new NetworkBuilder().Build();
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:18")
            .WithDatabase(DatabaseName)
            .WithUsername(DatabaseUsername)
            .WithPassword(DatabasePassword)
            .WithNetwork(_network)
            .WithNetworkAliases(PostgresNetworkAlias)
            .Build();
        _flyway = new ContainerBuilder()
            .WithImage("flyway/flyway:11-alpine")
            .WithCommand("migrate")
            .WithEnvironment("FLYWAY_URL", $"jdbc:postgresql://{PostgresNetworkAlias}:5432/{DatabaseName}")
            .WithEnvironment("FLYWAY_USER", DatabaseUsername)
            .WithEnvironment("FLYWAY_PASSWORD", DatabasePassword)
            .WithEnvironment("FLYWAY_CONNECT_RETRIES", "60")
            .WithEnvironment("FLYWAY_BASELINE_ON_MIGRATE", "true")
            .WithEnvironment("FLYWAY_DEFAULT_SCHEMA", "migrations")
            .WithEnvironment("FLYWAY_SCHEMAS", "migrations,public,worker")
            .WithResourceMapping(new DirectoryInfo(ResolveMigrationsDirectory()), "/flyway/sql")
            .WithNetwork(_network)
            .DependsOn(_postgres)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Successfully applied"))
            .Build();
        _tickerServices = new ServiceCollection()
            .AddEntityFrameworkNpgsql()
            .AddSingleton(
                new TickerQEfCoreOptionBuilder<TimeTickerEntity, CronTickerEntity>().SetSchema(
                    WorkerTickerQDbContext.Schema
                )
            )
            .BuildServiceProvider();
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await _flyway.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _flyway.DisposeAsync();
        await _postgres.DisposeAsync();
        await _network.DisposeAsync();
        await _tickerServices.DisposeAsync();
    }

    public ApplicationDbContext CreateDbContext(params IInterceptor[] interceptors)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(
            _postgres.GetConnectionString()
        );
        if (interceptors.Length > 0)
        {
            optionsBuilder.AddInterceptors(interceptors);
        }

        var options = optionsBuilder.Options;
        return new ApplicationDbContext(options);
    }

    public WorkerTickerQDbContext CreateTickerDbContext()
    {
        var options = new DbContextOptionsBuilder<WorkerTickerQDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .UseInternalServiceProvider(_tickerServices)
            .Options;
        return new WorkerTickerQDbContext(options);
    }

    public async Task ResetAsync()
    {
        await using var dbContext = CreateDbContext();
        await dbContext.Database.ExecuteSqlRawAsync(
            """
            TRUNCATE TABLE
                "worker"."TimeTickers",
                "worker"."CronTickerOccurrences",
                "worker"."CronTickers",
                "IgdbSyncRuns",
                "GameTypes",
                "GameStatuses",
                "PopularityTypes",
                "Games",
                "Genres",
                "Themes",
                "GameModes",
                "PlayerPerspectives",
                "PlatformTypes",
                "WebsiteTypes",
                "Platforms",
                "ExternalGameSources",
                "Companies",
                "Collections",
                "Franchises"
            CASCADE;
            """
        );
    }

    private static string ResolveMigrationsDirectory()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var migrationsDirectory = Path.Combine(directory.FullName, "db", "migrations");
            if (Directory.Exists(migrationsDirectory))
            {
                return migrationsDirectory;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not find db/migrations from the test runtime directory.");
    }
}
