using System.Security.Claims;
using ApesDb.Auth.Services;
using ApesDb.Common;
using ApesDb.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace ApesDb.Auth.IntegrationTests;

public sealed class UserProvisioningServiceTests : IAsyncLifetime, IDisposable
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder().WithImage("postgres:18").Build();

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        await ApplyMigrationsAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }

    public void Dispose()
    {
        _postgres.DisposeAsync().AsTask().GetAwaiter().GetResult();
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

    private async Task ApplyMigrationsAsync()
    {
        var sql = await File.ReadAllTextAsync("V1__Initial_schema.sql");
        await using var connection = new NpgsqlConnection(_postgres.GetConnectionString());
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }
}

public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateTime utcNow)
    {
        UtcNow = utcNow;
        Now = utcNow.ToLocalTime();
        OffsetNow = new DateTimeOffset(Now);
        OffsetUtcNow = new DateTimeOffset(utcNow);
    }

    public DateTime Now { get; private set; }

    public DateTime UtcNow
    {
        get => _utcNow;
        set
        {
            _utcNow = value;
            Now = value.ToLocalTime();
            OffsetNow = new DateTimeOffset(Now);
            OffsetUtcNow = new DateTimeOffset(value);
        }
    }

    public DateTimeOffset OffsetNow { get; private set; }

    public DateTimeOffset OffsetUtcNow { get; private set; }

    private DateTime _utcNow;
}
