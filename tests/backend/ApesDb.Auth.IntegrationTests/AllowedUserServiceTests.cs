using System.Security.Claims;
using ApesDb.Auth.Services.UserProvisioning;
using ApesDb.Auth.Services.UserSignIn;
using ApesDb.Domain;
using ApesDb.Domain.Entities.Users;
using ApesDb.Shared.Services.Users;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace ApesDb.Auth.IntegrationTests;

public sealed class AllowedUserServiceTests : IAsyncLifetime
{
    private const string DatabaseName = "apesdb";
    private const string DatabaseUsername = "apesdb";
    private const string DatabasePassword = "apesdb";
    private const string PostgresNetworkAlias = "postgres";

    private readonly INetwork _network;
    private readonly PostgreSqlContainer _postgres;
    private readonly IContainer _flyway;

    public AllowedUserServiceTests()
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
            .WithEnvironment("FLYWAY_SCHEMAS", "migrations,public")
            .WithResourceMapping(new DirectoryInfo(ResolveMigrationsDirectory()), "/flyway/sql")
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
    public async Task AddAsync_NormalizesEmailAndMakesDuplicateAdditionsIdempotent()
    {
        await using var dbContext = CreateDbContext();
        var service = new AllowedUserService(dbContext);

        var added = await service.AddAsync("  Allowed.User@Example.COM  ");
        var duplicateAdded = await service.AddAsync("allowed.user@example.com");

        Assert.True(added);
        Assert.False(duplicateAdded);
        var allowedUser = await dbContext.AllowedUsers.SingleAsync(value => value.Email == "allowed.user@example.com");
        Assert.NotEqual(Guid.Empty, allowedUser.Id);
        Assert.NotEqual(default, allowedUser.CreatedAt);
    }

    [Fact]
    public async Task AddAsync_RejectsInvalidEmailAddresses()
    {
        await using var dbContext = CreateDbContext();
        var service = new AllowedUserService(dbContext);
        var invalidEmails = new[]
        {
            string.Empty,
            "not-an-email",
            "Display Name <display@example.com>",
            new string('a', 257),
        };

        foreach (var email in invalidEmails)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(email));
        }
    }

    [Fact]
    public async Task AddAsync_RemainsUniqueWhenAddedConcurrently()
    {
        const string email = "concurrent.allowed@example.com";
        var additions = Enumerable
            .Range(0, 8)
            .Select(async _ =>
            {
                await using var dbContext = CreateDbContext();
                var service = new AllowedUserService(dbContext);
                return await service.AddAsync(email);
            });

        var results = await Task.WhenAll(additions);

        Assert.Single(results, value => value);
        await using var verificationDbContext = CreateDbContext();
        Assert.Equal(1, await verificationDbContext.AllowedUsers.CountAsync(value => value.Email == email));
    }

    [Fact]
    public async Task DatabaseRejectsDuplicateEmail()
    {
        const string email = "database-unique@example.com";
        await using var dbContext = CreateDbContext();
        dbContext.AllowedUsers.Add(new AllowedUser { Email = email });
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();
        dbContext.AllowedUsers.Add(new AllowedUser { Email = email });

        await Assert.ThrowsAsync<DbUpdateException>(() => dbContext.SaveChangesAsync());
    }

    [Fact]
    public async Task TrySignInAsync_DeniesUnlistedEmailWithoutProvisioningUser()
    {
        const string subject = "auth0|not-allowed";
        await using var dbContext = CreateDbContext();
        var signInService = CreateSignInService(dbContext);

        var result = await signInService.TrySignInAsync(
            CreatePrincipal(subject, "not-allowed@example.com", "Not Allowed")
        );

        Assert.True(result.IsT0);
        Assert.IsType<NotAllowed>(result.AsT0);
        Assert.False(await dbContext.Users.AnyAsync(value => value.Auth0Subject == subject));
    }

    [Fact]
    public async Task TrySignInAsync_AllowsCaseInsensitiveEmailAndProvisionsUser()
    {
        const string subject = "auth0|allowed";
        await using var dbContext = CreateDbContext();
        var allowedUserService = new AllowedUserService(dbContext);
        await allowedUserService.AddAsync("allowed.signin@example.com");
        var provisioningService = new UserProvisioningService(
            dbContext,
            new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc))
        );
        var signInService = new UserSignInService(allowedUserService, provisioningService);

        var result = await signInService.TrySignInAsync(
            CreatePrincipal(subject, "Allowed.SignIn@Example.COM", "Allowed User")
        );

        Assert.True(result.IsT1);
        Assert.NotEqual(Guid.Empty, result.AsT1.Id);
        Assert.True(await dbContext.Users.AnyAsync(value => value.Auth0Subject == subject));
    }

    [Fact]
    public async Task TrySignInAsync_DeniesMissingEmailWithoutProvisioningUser()
    {
        const string subject = "auth0|missing-email";
        await using var dbContext = CreateDbContext();
        var signInService = CreateSignInService(dbContext);
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.NameIdentifier, subject), new Claim(ClaimTypes.Name, "Missing Email") },
                "Test"
            )
        );

        var result = await signInService.TrySignInAsync(principal);

        Assert.True(result.IsT0);
        Assert.IsType<NotAllowed>(result.AsT0);
        Assert.False(await dbContext.Users.AnyAsync(value => value.Auth0Subject == subject));
    }

    private UserSignInService CreateSignInService(ApplicationDbContext dbContext)
    {
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        return new UserSignInService(
            new AllowedUserService(dbContext),
            new UserProvisioningService(dbContext, dateTimeProvider)
        );
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
