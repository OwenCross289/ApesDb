using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class ExternalGameSource : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class ExternalGameSourceConfiguration : IEntityTypeConfiguration<ExternalGameSource>
{
    public void Configure(EntityTypeBuilder<ExternalGameSource> externalGameSource)
    {
        externalGameSource.ToTable("ExternalGameSources");
        externalGameSource.ConfigureIgdbEntity();
        externalGameSource.Property(value => value.Name).HasMaxLength(256);
    }
}
