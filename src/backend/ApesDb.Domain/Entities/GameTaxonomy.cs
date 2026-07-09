using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class Genre : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class Theme : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameMode : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PlayerPerspective : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameGenre
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid GenreId { get; set; }

    public Genre Genre { get; set; } = null!;
}

public sealed class GameTheme
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid ThemeId { get; set; }

    public Theme Theme { get; set; } = null!;
}

public sealed class GameGameMode
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid GameModeId { get; set; }

    public GameMode GameMode { get; set; } = null!;
}

public sealed class GamePlayerPerspective
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid PlayerPerspectiveId { get; set; }

    public PlayerPerspective PlayerPerspective { get; set; } = null!;
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

public sealed class ThemeConfiguration : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> theme)
    {
        theme.ToTable("Themes");
        theme.ConfigureIgdbEntity();
        GenreConfiguration.ConfigureTaxonomyFields(theme);
    }
}

public sealed class GameModeConfiguration : IEntityTypeConfiguration<GameMode>
{
    public void Configure(EntityTypeBuilder<GameMode> gameMode)
    {
        gameMode.ToTable("GameModes");
        gameMode.ConfigureIgdbEntity();
        GenreConfiguration.ConfigureTaxonomyFields(gameMode);
    }
}

public sealed class PlayerPerspectiveConfiguration : IEntityTypeConfiguration<PlayerPerspective>
{
    public void Configure(EntityTypeBuilder<PlayerPerspective> playerPerspective)
    {
        playerPerspective.ToTable("PlayerPerspectives");
        playerPerspective.ConfigureIgdbEntity();
        GenreConfiguration.ConfigureTaxonomyFields(playerPerspective);
    }
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
