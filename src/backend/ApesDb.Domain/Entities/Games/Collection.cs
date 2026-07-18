using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class Collection : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> collection)
    {
        collection.ToTable("Collections");
        collection.ConfigureIgdbEntity();
        collection.Property(value => value.Name).HasMaxLength(512);
        collection.Property(value => value.Slug).HasMaxLength(512);
        collection.Property(value => value.IgdbUrl).HasMaxLength(2048);
    }
}
