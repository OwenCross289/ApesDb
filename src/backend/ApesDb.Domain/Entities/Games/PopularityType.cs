using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class PopularityType : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public long? ExternalPopularitySourceId { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PopularityTypeConfiguration : IEntityTypeConfiguration<PopularityType>
{
    public void Configure(EntityTypeBuilder<PopularityType> popularityType)
    {
        popularityType.ToTable("PopularityTypes");
        popularityType.ConfigureIgdbEntity();
        popularityType.HasIndex(value => value.ExternalPopularitySourceId);
        popularityType.Property(value => value.Name).HasMaxLength(256);
    }
}
