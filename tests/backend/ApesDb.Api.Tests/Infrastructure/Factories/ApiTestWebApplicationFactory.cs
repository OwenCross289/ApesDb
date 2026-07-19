using ApesDb.Api;
using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Time;
using ApesDb.Api.Tests.TestData;
using ApesDb.Common;
using ApesDb.Domain;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Testcontainers.PostgreSql;
using Xunit;

namespace ApesDb.Api.Tests.Infrastructure.Factories;

public abstract class ApiTestWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private const string DatabaseName = "apesdb";
    private const string DatabaseUsername = "apesdb";
    private const string DatabasePassword = "apesdb";
    private const string PostgresNetworkAlias = "postgres";
    private const string RedisPassword = "apesdb";
    private const ushort RedisPort = 6379;

    private readonly Dictionary<string, string?> _originalEnvironment = [];
    private readonly IContainer _flyway;
    private readonly INetwork _network;
    private readonly PostgreSqlContainer _postgres;
    private readonly IContainer _redis;
    private string? _databaseConnectionString;
    private bool _environmentRestored;
    private string? _redisConnectionString;

    protected ApiTestWebApplicationFactory()
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
        _redis = new ContainerBuilder()
            .WithImage("redis:8")
            .WithCommand("redis-server", "--requirepass", RedisPassword)
            .WithPortBinding(RedisPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(RedisPort))
            .Build();
        _flyway = new ContainerBuilder()
            .WithImage("flyway/flyway:12-alpine")
            .WithCommand("migrate")
            .WithEnvironment("FLYWAY_URL", $"jdbc:postgresql://{PostgresNetworkAlias}:5432/{DatabaseName}")
            .WithEnvironment("FLYWAY_USER", DatabaseUsername)
            .WithEnvironment("FLYWAY_PASSWORD", DatabasePassword)
            .WithEnvironment("FLYWAY_CONNECT_RETRIES", "60")
            .WithEnvironment("FLYWAY_BASELINE_ON_MIGRATE", "true")
            .WithEnvironment("FLYWAY_DEFAULT_SCHEMA", "migrations")
            .WithEnvironment("FLYWAY_SCHEMAS", "migrations,public")
            .WithResourceMapping(new DirectoryInfo(ResolveMigrationsDirectory()), "/flyway/sql")
            .WithNetwork(_network)
            .DependsOn(_postgres)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Successfully applied"))
            .Build();
    }

    public virtual async ValueTask InitializeAsync()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await Task.WhenAll(_postgres.StartAsync(cancellationToken), _redis.StartAsync(cancellationToken));
        await _flyway.StartAsync(cancellationToken);

        _databaseConnectionString = _postgres.GetConnectionString();
        _redisConnectionString = $"localhost:{_redis.GetMappedPublicPort(RedisPort)}";
        SetTestEnvironment();
        await SeedAsync(cancellationToken);
    }

    public override async ValueTask DisposeAsync()
    {
        try
        {
            await base.DisposeAsync();
        }
        finally
        {
            RestoreEnvironment();
            await _flyway.DisposeAsync();
            await _redis.DisposeAsync();
            await _postgres.DisposeAsync();
            await _network.DisposeAsync();
        }
    }

    protected async Task SeedAsync(CancellationToken cancellationToken)
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var graph = BaseTestData.Create();
        dbContext.AddRange(graph.Entities);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var databaseConnectionString = GetDatabaseConnectionString();
        var redisConnectionString = GetRedisConnectionString();

        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration(
            (_, configuration) =>
            {
                configuration.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["Database:ConnectionString"] = databaseConnectionString,
                        ["Cache:ConnectionString"] = redisConnectionString,
                        ["Cache:Password"] = RedisPassword,
                        ["Auth0:Domain"] = "auth.example.test",
                        ["Auth0:ClientId"] = "apesdb-api-tests",
                        ["Auth0:ClientSecret"] = "not-a-real-secret",
                        ["Auth0:CallbackPath"] = "/api/auth/callback",
                        ["Auth0:GoogleConnectionName"] = "google-oauth2",
                    }
                );
            }
        );
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IDateTimeProvider>();
            services.AddSingleton<IDateTimeProvider>(new TestDateTimeProvider(TestClock.UtcNow));
            services
                .AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
                    FakeAuthenticationHandler.SchemeName,
                    _ => { }
                );
            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = FakeAuthenticationHandler.SchemeName;
            });
            services.PostConfigure<OpenIdConnectOptions>(
                "Auth0",
                options =>
                {
                    var configuration = new OpenIdConnectConfiguration
                    {
                        AuthorizationEndpoint = "https://auth.example.test/authorize",
                        EndSessionEndpoint = "https://auth.example.test/logout",
                        Issuer = "https://auth.example.test/",
                    };
                    options.Configuration = configuration;
                    options.ConfigurationManager = new StaticConfigurationManager<OpenIdConnectConfiguration>(
                        configuration
                    );
                }
            );
        });
    }

    private void SetTestEnvironment()
    {
        SetEnvironmentVariable("Database__ConnectionString", GetDatabaseConnectionString());
        SetEnvironmentVariable("Cache__ConnectionString", GetRedisConnectionString());
        SetEnvironmentVariable("Cache__Password", RedisPassword);
        SetEnvironmentVariable("Auth0__Domain", "auth.example.test");
        SetEnvironmentVariable("Auth0__ClientId", "apesdb-api-tests");
        SetEnvironmentVariable("Auth0__ClientSecret", "not-a-real-secret");
        SetEnvironmentVariable("Auth0__CallbackPath", "/api/auth/callback");
        SetEnvironmentVariable("Auth0__GoogleConnectionName", "google-oauth2");
    }

    private void SetEnvironmentVariable(string name, string value)
    {
        _originalEnvironment[name] = Environment.GetEnvironmentVariable(name);
        Environment.SetEnvironmentVariable(name, value);
    }

    private void RestoreEnvironment()
    {
        if (_environmentRestored)
        {
            return;
        }

        foreach (var pair in _originalEnvironment)
        {
            Environment.SetEnvironmentVariable(pair.Key, pair.Value);
        }

        _environmentRestored = true;
    }

    private string GetDatabaseConnectionString()
    {
        if (_databaseConnectionString is null)
        {
            throw new InvalidOperationException("The PostgreSQL test container has not started.");
        }

        return _databaseConnectionString;
    }

    private string GetRedisConnectionString()
    {
        if (_redisConnectionString is null)
        {
            throw new InvalidOperationException("The Redis test container has not started.");
        }

        return _redisConnectionString;
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
