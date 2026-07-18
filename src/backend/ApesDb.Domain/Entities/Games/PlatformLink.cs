using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class PlatformLink : IIgdbEntity
{
    public long Id { get; set; }

    public long PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;

    public long WebsiteTypeId { get; set; }

    public WebsiteType WebsiteType { get; set; } = null!;

    public required string Url { get; set; }

    public bool Trusted { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PlatformLinkConfiguration : IEntityTypeConfiguration<PlatformLink>
{
    public void Configure(EntityTypeBuilder<PlatformLink> platformLink)
    {
        platformLink.ToTable("PlatformLinks");
        platformLink.ConfigureIgdbEntity();
        platformLink.HasIndex(value => value.PlatformId);
        platformLink.HasIndex(value => value.WebsiteTypeId);
        platformLink.Property(value => value.Url).HasMaxLength(2048);
        platformLink
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
        platformLink
            .HasOne(value => value.WebsiteType)
            .WithMany()
            .HasForeignKey(value => value.WebsiteTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
