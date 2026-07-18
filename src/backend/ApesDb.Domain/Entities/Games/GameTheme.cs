using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameTheme
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long ThemeId { get; set; }

    public Theme Theme { get; set; } = null!;
}

public sealed class GameThemeConfiguration : IEntityTypeConfiguration<GameTheme>
{
    public void Configure(EntityTypeBuilder<GameTheme> gameTheme)
    {
        gameTheme.ToTable("GameThemes");
        gameTheme.HasKey(value => new { value.GameId, value.ThemeId });
        gameTheme.HasIndex(value => value.ThemeId);
        gameTheme
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameTheme
            .HasOne(value => value.Theme)
            .WithMany()
            .HasForeignKey(value => value.ThemeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
