using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class PlatformType : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
{
    public void Configure(EntityTypeBuilder<PlatformType> platformType)
    {
        platformType.ToTable("PlatformTypes");
        platformType.ConfigureIgdbEntity();
        platformType.Property(value => value.Name).HasMaxLength(256);
    }
}
