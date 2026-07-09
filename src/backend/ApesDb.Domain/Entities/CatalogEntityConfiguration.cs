using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public interface IIgdbEntity
{
    Guid Id { get; set; }

    long IgdbId { get; set; }

    Guid? Checksum { get; set; }

    DateTime? IgdbUpdatedAt { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }

    DateTime LastSyncedAt { get; set; }
}

internal static class CatalogEntityConfiguration
{
    public static void ConfigureIgdbEntity<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : class, IIgdbEntity
    {
        entity.HasKey(value => value.Id);
        entity.HasIndex(value => value.IgdbId).IsUnique();
        entity.Property(value => value.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        entity.Property(value => value.CreatedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        entity.Property(value => value.UpdatedAt).HasDefaultValueSql("now()");
        entity.Property(value => value.LastSyncedAt).HasDefaultValueSql("now()");
    }
}
