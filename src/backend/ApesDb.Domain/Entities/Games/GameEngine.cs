using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameEngine : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameEngineConfiguration : IEntityTypeConfiguration<GameEngine>
{
    public void Configure(EntityTypeBuilder<GameEngine> gameEngine)
    {
        gameEngine.ToTable("GameEngines");
        gameEngine.ConfigureIgdbEntity();
        gameEngine.Property(value => value.Name).HasMaxLength(256);
    }
}
