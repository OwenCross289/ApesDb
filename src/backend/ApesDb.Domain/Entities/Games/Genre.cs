using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class Genre : IIgdbEntity
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

public sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> genre)
    {
        genre.ToTable("Genres");
        genre.ConfigureIgdbEntity();
        ConfigureTaxonomyFields(genre);
    }

    internal static void ConfigureTaxonomyFields<TEntity>(EntityTypeBuilder<TEntity> entity)
        where TEntity : class
    {
        entity.Property("Name").HasMaxLength(256);
        entity.Property("Slug").HasMaxLength(256);
        entity.Property("IgdbUrl").HasMaxLength(2048);
    }
}
