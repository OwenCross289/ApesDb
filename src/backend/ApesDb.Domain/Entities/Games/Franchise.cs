using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class Franchise : IIgdbEntity
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

public sealed class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
{
    public void Configure(EntityTypeBuilder<Franchise> franchise)
    {
        franchise.ToTable("Franchises");
        franchise.ConfigureIgdbEntity();
        franchise.Property(value => value.Name).HasMaxLength(512);
        franchise.Property(value => value.Slug).HasMaxLength(512);
        franchise.Property(value => value.IgdbUrl).HasMaxLength(2048);
    }
}
