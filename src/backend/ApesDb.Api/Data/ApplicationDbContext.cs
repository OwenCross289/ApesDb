using ApesDb.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Api.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = modelBuilder.Entity<User>();

        user.HasKey(u => u.Id);
        user.HasIndex(u => u.Auth0Subject).IsUnique();
        user.HasIndex(u => u.Email);
        user.Property(u => u.Auth0Subject).HasMaxLength(256);
        user.Property(u => u.Email).HasMaxLength(256);
        user.Property(u => u.Name).HasMaxLength(256);
        user.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
        user.Property(u => u.UpdatedAt).HasDefaultValueSql("now()");
    }
}
