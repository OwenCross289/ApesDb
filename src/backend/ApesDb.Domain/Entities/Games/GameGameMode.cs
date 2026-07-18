using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameGameMode
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long GameModeId { get; set; }

    public GameMode GameMode { get; set; } = null!;
}

public sealed class GameGameModeConfiguration : IEntityTypeConfiguration<GameGameMode>
{
    public void Configure(EntityTypeBuilder<GameGameMode> gameGameMode)
    {
        gameGameMode.ToTable("GameGameModes");
        gameGameMode.HasKey(value => new { value.GameId, value.GameModeId });
        gameGameMode.HasIndex(value => value.GameModeId);
        gameGameMode
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameGameMode
            .HasOne(value => value.GameMode)
            .WithMany()
            .HasForeignKey(value => value.GameModeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
