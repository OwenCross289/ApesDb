using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }

    public required string Auth0Subject { get; init; }

    public required string Email { get; set; }

    public required string Name { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; set; }
}

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> user)
    {
        user.HasKey(u => u.Id);
        user.HasIndex(u => u.Auth0Subject).IsUnique();
        user.HasIndex(u => u.Email);
        user.Property(u => u.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        user.Property(u => u.Auth0Subject).HasMaxLength(256);
        user.Property(u => u.Email).HasMaxLength(256);
        user.Property(u => u.Name).HasMaxLength(256);
        user.Property(u => u.PictureUrl).HasMaxLength(2048);
        user.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
        user.Property(u => u.UpdatedAt).HasDefaultValueSql("now()");
    }
}
