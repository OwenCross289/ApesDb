using System.Security.Claims;
using ApesDb.Auth.Services;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace ApesDb.Auth.IntegrationTests;

public sealed class UserProvisioningServiceTests : IAsyncLifetime
{
    private const string DatabaseName = "apesdb";
    private const string DatabaseUsername = "apesdb";
    private const string DatabasePassword = "apesdb";
    private const string PostgresNetworkAlias = "postgres";

    private readonly INetwork _network;
    private readonly PostgreSqlContainer _postgres;
    private readonly IContainer _flyway;

    public UserProvisioningServiceTests()
    {
        var migrationsDirectory = ResolveMigrationsDirectory();

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
            .WithEnvironment("FLYWAY_SCHEMAS", "migrations,public")
            .WithResourceMapping(new DirectoryInfo(migrationsDirectory), "/flyway/sql")
            .WithNetwork(_network)
            .DependsOn(_postgres)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Successfully applied"))
            .Build();
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
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_CreatesNewUser_WhenSubjectDoesNotExist()
    {
        var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTimeProvider = new FixedDateTimeProvider(fixedDate);
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);
        var principal = CreatePrincipal("auth0|123", "test@example.com", "Test User");

        var result = await service.EnsureUserFromPrincipalAsync(principal);

        Assert.NotEqual(Guid.Empty, result.Id);
        var user = await dbContext.Users.SingleAsync(u => u.Auth0Subject == "auth0|123");
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("Test User", user.Name);
        Assert.Equal(fixedDate, user.CreatedAt);
        Assert.Equal(fixedDate, user.UpdatedAt);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_UpdatesExistingUser_WhenSubjectExists()
    {
        var initialDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var updatedDate = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTimeProvider = new FixedDateTimeProvider(initialDate);
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);
        var principal = CreatePrincipal("auth0|456", "old@example.com", "Old Name");
        var created = await service.EnsureUserFromPrincipalAsync(principal);

        dateTimeProvider.UtcNow = updatedDate;
        var updatedPrincipal = CreatePrincipal("auth0|456", "new@example.com", "New Name");
        var result = await service.EnsureUserFromPrincipalAsync(updatedPrincipal);

        Assert.Equal(created.Id, result.Id);
        var user = await dbContext.Users.SingleAsync(u => u.Auth0Subject == "auth0|456");
        Assert.Equal("new@example.com", user.Email);
        Assert.Equal("New Name", user.Name);
        Assert.Equal(initialDate, user.CreatedAt);
        Assert.Equal(updatedDate, user.UpdatedAt);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_PersistsMappedAuth0NameClaim()
    {
        var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTimeProvider = new FixedDateTimeProvider(fixedDate);
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "auth0|789"),
                    new Claim(ClaimTypes.Email, "mapped@example.com"),
                    new Claim(ClaimTypes.Name, "Mapped Auth0 Name"),
                    new Claim("name", "Literal Name Claim"),
                },
                "Test"
            )
        );

        await service.EnsureUserFromPrincipalAsync(principal);

        var user = await dbContext.Users.SingleAsync(u => u.Auth0Subject == "auth0|789");
        Assert.Equal("Mapped Auth0 Name", user.Name);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_CreatesSoloTeam_WhenUserIsNew()
    {
        var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTimeProvider = new FixedDateTimeProvider(fixedDate);
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);
        var principal = CreatePrincipal("auth0|team-1", "team@example.com", "Team Owner");

        var result = await service.EnsureUserFromPrincipalAsync(principal);

        var team = await dbContext.Teams.SingleAsync(t => t.OwnerUserId == result.Id);
        Assert.Equal(TeamKind.Solo, team.Kind);
        Assert.Equal("Team Owner's Team", team.Name);
        Assert.Null(team.ProfilePictureUrl);
        Assert.Equal(fixedDate, team.CreatedAt);
        Assert.Equal(fixedDate, team.UpdatedAt);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_NamesSoloTeamSoloTeam_WhenNameIsEmpty()
    {
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);
        var principal = CreatePrincipal("auth0|team-2", "noname@example.com", string.Empty);

        var result = await service.EnsureUserFromPrincipalAsync(principal);

        var team = await dbContext.Teams.SingleAsync(t => t.OwnerUserId == result.Id);
        Assert.Equal("Solo Team", team.Name);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_DoesNotCreateDuplicateSoloTeam_WhenUserLogsInAgain()
    {
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);

        await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|team-3", "repeat@example.com", "Repeat User")
        );
        var result = await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|team-3", "repeat@example.com", "Repeat User")
        );

        var soloTeamCount = await dbContext.Teams.CountAsync(t =>
            t.OwnerUserId == result.Id && t.Kind == TeamKind.Solo
        );
        Assert.Equal(1, soloTeamCount);
    }

    private static ClaimsPrincipal CreatePrincipal(string subject, string email, string name)
    {
        return new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, subject),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name),
                },
                "Test"
            )
        );
    }

    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_postgres.GetConnectionString())
            .Options;

        return new ApplicationDbContext(options);
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
