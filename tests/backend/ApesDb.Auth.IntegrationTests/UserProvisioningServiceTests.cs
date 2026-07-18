using System.Data.Common;
using System.Security.Claims;
using ApesDb.Auth.Services;
using ApesDb.Domain;
using ApesDb.Domain.Entities;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        var principal = CreatePrincipal(
            "auth0|123",
            "test@example.com",
            "Test User",
            "https://images.example.com/test-user.png"
        );

        var result = await service.EnsureUserFromPrincipalAsync(principal);

        Assert.NotEqual(Guid.Empty, result.Id);
        var user = await dbContext.Users.SingleAsync(u => u.Auth0Subject == "auth0|123");
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("Test User", user.Name);
        Assert.Equal("https://images.example.com/test-user.png", user.PictureUrl);
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
        var principal = CreatePrincipal(
            "auth0|456",
            "old@example.com",
            "Old Name",
            "https://images.example.com/old.png"
        );
        var created = await service.EnsureUserFromPrincipalAsync(principal);

        dateTimeProvider.UtcNow = updatedDate;
        var updatedPrincipal = CreatePrincipal(
            "auth0|456",
            "new@example.com",
            "New Name",
            "https://images.example.com/new.png"
        );
        var result = await service.EnsureUserFromPrincipalAsync(updatedPrincipal);

        Assert.Equal(created.Id, result.Id);
        var user = await dbContext.Users.SingleAsync(u => u.Auth0Subject == "auth0|456");
        Assert.Equal("new@example.com", user.Email);
        Assert.Equal("New Name", user.Name);
        Assert.Equal("https://images.example.com/new.png", user.PictureUrl);
        Assert.Equal(initialDate, user.CreatedAt);
        Assert.Equal(updatedDate, user.UpdatedAt);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_ClearsPictureUrl_WhenPictureClaimIsMissing()
    {
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        await using var dbContext = CreateDbContext();
        var service = new UserProvisioningService(dbContext, dateTimeProvider);

        await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal(
                "auth0|picture",
                "picture@example.com",
                "Picture User",
                "https://images.example.com/user.png"
            )
        );
        await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|picture", "picture@example.com", "Picture User")
        );

        var user = await dbContext.Users.SingleAsync(value => value.Auth0Subject == "auth0|picture");
        Assert.Null(user.PictureUrl);
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
        Assert.Null(team.ProfilePicture);
        Assert.Equal(fixedDate, team.CreatedAt);
        Assert.Equal(fixedDate, team.UpdatedAt);
        var membership = await dbContext.TeamMemberships.SingleAsync(value => value.TeamId == team.Id);
        Assert.Equal(result.Id, membership.UserId);
        Assert.Equal(TeamMembershipStatus.Accepted, membership.Status);
        Assert.Equal(fixedDate, membership.AcceptedAt);
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

        var created = await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|team-3", "repeat@example.com", "Repeat User")
        );
        var originalSoloTeamId = await dbContext
            .Teams.Where(value => value.OwnerUserId == created.Id && value.Kind == TeamKind.Solo)
            .Select(value => value.Id)
            .SingleAsync();
        var originalMembershipId = await dbContext
            .TeamMemberships.Where(value => value.TeamId == originalSoloTeamId)
            .Select(value => value.Id)
            .SingleAsync();
        var result = await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|team-3", "repeat@example.com", "Renamed User")
        );

        Assert.Equal(created.Id, result.Id);
        var soloTeamCount = await dbContext.Teams.CountAsync(t =>
            t.OwnerUserId == result.Id && t.Kind == TeamKind.Solo
        );
        Assert.Equal(1, soloTeamCount);
        var soloTeam = await dbContext.Teams.SingleAsync(t => t.OwnerUserId == result.Id && t.Kind == TeamKind.Solo);
        Assert.Equal(originalSoloTeamId, soloTeam.Id);
        Assert.Equal("Repeat User's Team", soloTeam.Name);
        var membership = await dbContext.TeamMemberships.SingleAsync(value => value.TeamId == soloTeam.Id);
        Assert.Equal(originalMembershipId, membership.Id);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_ExecutesOneCommand_WhenProvisioningNewUser()
    {
        var commandInterceptor = new CountingDbCommandInterceptor();
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        await using var dbContext = CreateDbContext(commandInterceptor);
        var service = new UserProvisioningService(dbContext, dateTimeProvider);

        await service.EnsureUserFromPrincipalAsync(
            CreatePrincipal("auth0|one-command-new", "new@example.com", "New User")
        );

        Assert.Equal(1, commandInterceptor.CommandCount);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_UpdatesExistingProvisioning_InOneCommand()
    {
        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTimeProvider = new FixedDateTimeProvider(now);
        Guid ownerId;
        Guid soloTeamId;
        Guid membershipId;

        await using (var setupDbContext = CreateDbContext())
        {
            var setupService = new UserProvisioningService(setupDbContext, dateTimeProvider);
            ownerId = (
                await setupService.EnsureUserFromPrincipalAsync(
                    CreatePrincipal("auth0|repair-owner", "owner@example.com", "Old Owner Name")
                )
            ).Id;
            soloTeamId = await setupDbContext
                .Teams.Where(value => value.OwnerUserId == ownerId && value.Kind == TeamKind.Solo)
                .Select(value => value.Id)
                .SingleAsync();
            membershipId = await setupDbContext
                .TeamMemberships.Where(value => value.TeamId == soloTeamId && value.UserId == ownerId)
                .Select(value => value.Id)
                .SingleAsync();
        }

        var commandInterceptor = new CountingDbCommandInterceptor();
        dateTimeProvider.UtcNow = now.AddDays(1);
        await using (var provisioningDbContext = CreateDbContext(commandInterceptor))
        {
            var service = new UserProvisioningService(provisioningDbContext, dateTimeProvider);
            var result = await service.EnsureUserFromPrincipalAsync(
                CreatePrincipal("auth0|repair-owner", "owner@example.com", "New Owner Name")
            );

            Assert.Equal(ownerId, result.Id);
            Assert.Equal(1, commandInterceptor.CommandCount);
        }

        await using var verificationDbContext = CreateDbContext();
        var soloTeam = await verificationDbContext.Teams.SingleAsync(value => value.Id == soloTeamId);
        Assert.Equal("Old Owner Name's Team", soloTeam.Name);
        var membership = await verificationDbContext.TeamMemberships.SingleAsync(value => value.TeamId == soloTeamId);
        Assert.Equal(membershipId, membership.Id);
        Assert.Equal(ownerId, membership.UserId);
        Assert.Equal(TeamMembershipStatus.Accepted, membership.Status);
        Assert.Null(membership.InvitedByUserId);
        Assert.Equal(now, membership.AcceptedAt);
    }

    [Fact]
    public async Task EnsureUserFromPrincipalAsync_RemainsUnique_WhenProvisionedConcurrently()
    {
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        var principal = CreatePrincipal("auth0|concurrent", "concurrent@example.com", "Concurrent User");
        var provisioningTasks = Enumerable
            .Range(0, 8)
            .Select(async _ =>
            {
                await using var dbContext = CreateDbContext();
                var service = new UserProvisioningService(dbContext, dateTimeProvider);
                return await service.EnsureUserFromPrincipalAsync(principal);
            });

        var results = await Task.WhenAll(provisioningTasks);

        Assert.Single(results.Select(value => value.Id).Distinct());
        await using var verificationDbContext = CreateDbContext();
        var user = await verificationDbContext.Users.SingleAsync(value => value.Auth0Subject == "auth0|concurrent");
        var team = await verificationDbContext.Teams.SingleAsync(value =>
            value.OwnerUserId == user.Id && value.Kind == TeamKind.Solo
        );
        var membership = await verificationDbContext.TeamMemberships.SingleAsync(value => value.TeamId == team.Id);
        Assert.Equal(user.Id, membership.UserId);
        Assert.Equal(TeamMembershipStatus.Accepted, membership.Status);
    }

    private static ClaimsPrincipal CreatePrincipal(string subject, string email, string name, string? pictureUrl = null)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, subject),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Name, name),
        };
        if (pictureUrl is not null)
        {
            claims.Add(new Claim("picture", pictureUrl));
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
    }

    private ApplicationDbContext CreateDbContext(DbCommandInterceptor? commandInterceptor = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(
            _postgres.GetConnectionString()
        );
        if (commandInterceptor is not null)
        {
            optionsBuilder.AddInterceptors(commandInterceptor);
        }

        return new ApplicationDbContext(optionsBuilder.Options);
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

    private sealed class CountingDbCommandInterceptor : DbCommandInterceptor
    {
        private int _commandCount;

        public int CommandCount => _commandCount;

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result
        )
        {
            Interlocked.Increment(ref _commandCount);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default
        )
        {
            Interlocked.Increment(ref _commandCount);
            return ValueTask.FromResult(result);
        }
    }
}
