using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameCollection
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long CollectionId { get; set; }

    public Collection Collection { get; set; } = null!;
}

public sealed class GameCollectionConfiguration : IEntityTypeConfiguration<GameCollection>
{
    public void Configure(EntityTypeBuilder<GameCollection> gameCollection)
    {
        gameCollection.ToTable("GameCollections");
        gameCollection.HasKey(value => new { value.GameId, value.CollectionId });
        gameCollection.HasIndex(value => value.CollectionId);
        gameCollection
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameCollection
            .HasOne(value => value.Collection)
            .WithMany()
            .HasForeignKey(value => value.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
