using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameGenre
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long GenreId { get; set; }

    public Genre Genre { get; set; } = null!;
}

public sealed class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> gameGenre)
    {
        gameGenre.ToTable("GameGenres");
        gameGenre.HasKey(value => new { value.GameId, value.GenreId });
        gameGenre.HasIndex(value => value.GenreId);
        gameGenre
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameGenre
            .HasOne(value => value.Genre)
            .WithMany()
            .HasForeignKey(value => value.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
