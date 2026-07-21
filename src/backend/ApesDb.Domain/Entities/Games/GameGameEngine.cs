using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameGameEngine
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long GameEngineId { get; set; }

    public GameEngine GameEngine { get; set; } = null!;
}

public sealed class GameGameEngineConfiguration : IEntityTypeConfiguration<GameGameEngine>
{
    public void Configure(EntityTypeBuilder<GameGameEngine> gameGameEngine)
    {
        gameGameEngine.ToTable("GameGameEngines");
        gameGameEngine.HasKey(value => new { value.GameId, value.GameEngineId });
        gameGameEngine.HasIndex(value => value.GameEngineId);
        gameGameEngine
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameGameEngine
            .HasOne(value => value.GameEngine)
            .WithMany()
            .HasForeignKey(value => value.GameEngineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
