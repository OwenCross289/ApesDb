using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GamePlayerPerspective
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long PlayerPerspectiveId { get; set; }

    public PlayerPerspective PlayerPerspective { get; set; } = null!;
}

public sealed class GamePlayerPerspectiveConfiguration : IEntityTypeConfiguration<GamePlayerPerspective>
{
    public void Configure(EntityTypeBuilder<GamePlayerPerspective> gamePlayerPerspective)
    {
        gamePlayerPerspective.ToTable("GamePlayerPerspectives");
        gamePlayerPerspective.HasKey(value => new { value.GameId, value.PlayerPerspectiveId });
        gamePlayerPerspective.HasIndex(value => value.PlayerPerspectiveId);
        gamePlayerPerspective
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gamePlayerPerspective
            .HasOne(value => value.PlayerPerspective)
            .WithMany()
            .HasForeignKey(value => value.PlayerPerspectiveId)
            .HasConstraintName("FK_GamePlayerPerspectives_PlayerPerspective")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
