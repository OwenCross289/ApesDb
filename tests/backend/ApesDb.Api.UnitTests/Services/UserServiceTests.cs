using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApesDb.Api.Data;
using ApesDb.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.UnitTests.Services;

public sealed class UserServiceTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _dbContext = new ApplicationDbContext(options);
        _service = new UserService(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private static ClaimsPrincipal CreatePrincipal(string sub, string email, string name)
    {
        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.NameIdentifier, sub),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
            }
        );
        return new ClaimsPrincipal(identity);
    }

    [Fact]
    public async Task EnsureUserAsyncCreatesUserWhenMissing()
    {
        var principal = CreatePrincipal("new-sub", "new@example.com", "New User");

        var user = await _service.EnsureUserAsync(principal, TestContext.Current.CancellationToken);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("new-sub", user.Auth0Subject);
        Assert.Equal("new@example.com", user.Email);
        Assert.Equal("New User", user.Name);
    }

    [Fact]
    public async Task EnsureUserAsyncUpdatesExistingUser()
    {
        var principal = CreatePrincipal("existing-sub", "old@example.com", "Old Name");
        var existing = await _service.EnsureUserAsync(
            principal,
            TestContext.Current.CancellationToken
        );
        var originalId = existing.Id;

        var updatedPrincipal = CreatePrincipal("existing-sub", "new@example.com", "New Name");
        var updated = await _service.EnsureUserAsync(
            updatedPrincipal,
            TestContext.Current.CancellationToken
        );

        Assert.Equal(originalId, updated.Id);
        Assert.Equal("new@example.com", updated.Email);
        Assert.Equal("New Name", updated.Name);
    }

    [Fact]
    public async Task EnsureUserAsyncThrowsWhenSubjectClaimMissing()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity());

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.EnsureUserAsync(principal, TestContext.Current.CancellationToken)
        );
    }
}
