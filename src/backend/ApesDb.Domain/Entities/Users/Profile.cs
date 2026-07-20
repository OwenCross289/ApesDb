using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Users;

public sealed class Profile
{
    public Guid UserId { get; init; }

    public string? AboutMe { get; set; }

    public bool IsPublic { get; set; }

    public required User User { get; init; }
}

public sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> profile)
    {
        profile.HasKey(value => value.UserId);
        profile.HasIndex(value => value.IsPublic);
        profile.Property(value => value.AboutMe).HasMaxLength(4000);
        profile
            .HasOne(value => value.User)
            .WithOne(value => value.Profile)
            .HasForeignKey<Profile>(value => value.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
