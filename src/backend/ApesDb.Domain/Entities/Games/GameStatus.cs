using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameStatus : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameStatusConfiguration : IEntityTypeConfiguration<GameStatus>
{
    public void Configure(EntityTypeBuilder<GameStatus> gameStatus)
    {
        gameStatus.ToTable("GameStatuses");
        gameStatus.ConfigureIgdbEntity();
        gameStatus.Property(value => value.Name).HasMaxLength(256);
    }
}
