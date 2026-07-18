using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class WebsiteType : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class WebsiteTypeConfiguration : IEntityTypeConfiguration<WebsiteType>
{
    public void Configure(EntityTypeBuilder<WebsiteType> websiteType)
    {
        websiteType.ToTable("WebsiteTypes");
        websiteType.ConfigureIgdbEntity();
        websiteType.Property(value => value.Name).HasMaxLength(256);
    }
}
