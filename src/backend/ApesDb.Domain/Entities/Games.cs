using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class Game : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? Storyline { get; set; }

    public DateTime? FirstReleaseDate { get; set; }

    public decimal? TotalRating { get; set; }

    public long? TotalRatingCount { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? GameTypeId { get; set; }

    public GameType? GameType { get; set; }

    public Guid? GameStatusId { get; set; }

    public GameStatus? GameStatus { get; set; }

    public string? CoverImageId { get; set; }

    public int? CoverWidth { get; set; }

    public int? CoverHeight { get; set; }

    public string? CoverSmallUrl { get; set; }

    public string? CoverLargeUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameType : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameStatus : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PopularGame
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public int Rank { get; set; }

    public int SourceRank { get; set; }

    public decimal Score { get; set; }

    public long IgdbPopularityTypeId { get; set; }

    public DateTime CalculatedAt { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime SyncedAt { get; set; }
}

public enum GameRelationType
{
    Dlc,
    Expansion,
    StandaloneExpansion,
}

public sealed class GameRelation
{
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid RelatedGameId { get; set; }

    public Game RelatedGame { get; set; } = null!;

    public GameRelationType RelationType { get; set; }

    public DateTime CreatedAt { get; set; }
}

public sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> game)
    {
        game.ToTable("Games");
        game.ConfigureIgdbEntity();
        game.Property(value => value.Name).HasMaxLength(512);
        game.Property(value => value.Slug).HasMaxLength(512);
        game.Property(value => value.TotalRating).HasPrecision(8, 4);
        game.Property(value => value.IgdbUrl).HasMaxLength(2048);
        game.Property(value => value.CoverImageId).HasMaxLength(128);
        game.Property(value => value.CoverSmallUrl).HasMaxLength(2048);
        game.Property(value => value.CoverLargeUrl).HasMaxLength(2048);
        game.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_Games_TotalRating",
                "\"TotalRating\" IS NULL OR (\"TotalRating\" >= 0 AND \"TotalRating\" <= 100)"
            );
            table.HasCheckConstraint(
                "CK_Games_TotalRatingCount",
                "\"TotalRatingCount\" IS NULL OR \"TotalRatingCount\" >= 0"
            );
            table.HasCheckConstraint("CK_Games_CoverWidth", "\"CoverWidth\" IS NULL OR \"CoverWidth\" > 0");
            table.HasCheckConstraint("CK_Games_CoverHeight", "\"CoverHeight\" IS NULL OR \"CoverHeight\" > 0");
        });
        game.HasOne(value => value.GameType)
            .WithMany()
            .HasForeignKey(value => value.GameTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        game.HasOne(value => value.GameStatus)
            .WithMany()
            .HasForeignKey(value => value.GameStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public sealed class GameTypeConfiguration : IEntityTypeConfiguration<GameType>
{
    public void Configure(EntityTypeBuilder<GameType> gameType)
    {
        gameType.ToTable("GameTypes");
        gameType.ConfigureIgdbEntity();
        gameType.Property(value => value.Name).HasMaxLength(256);
    }
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

public sealed class PopularGameConfiguration : IEntityTypeConfiguration<PopularGame>
{
    public void Configure(EntityTypeBuilder<PopularGame> popularGame)
    {
        popularGame.ToTable("PopularGames");
        popularGame.HasKey(value => value.GameId);
        popularGame.HasIndex(value => value.Rank).IsUnique();
        popularGame.Property(value => value.Score).HasPrecision(28, 18);
        popularGame.Property(value => value.SyncedAt).HasDefaultValueSql("now()");
        popularGame.ToTable(table =>
        {
            table.HasCheckConstraint("CK_PopularGames_Rank", "\"Rank\" BETWEEN 1 AND 1000");
            table.HasCheckConstraint("CK_PopularGames_SourceRank", "\"SourceRank\" > 0");
            table.HasCheckConstraint("CK_PopularGames_Score", "\"Score\" >= 0");
        });
        popularGame
            .HasOne(value => value.Game)
            .WithOne()
            .HasForeignKey<PopularGame>(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class GameRelationConfiguration : IEntityTypeConfiguration<GameRelation>
{
    public void Configure(EntityTypeBuilder<GameRelation> gameRelation)
    {
        gameRelation.ToTable("GameRelations");
        gameRelation.HasKey(value => new
        {
            value.GameId,
            value.RelatedGameId,
            value.RelationType,
        });
        gameRelation.HasIndex(value => value.RelatedGameId);
        gameRelation.Property(value => value.RelationType).HasConversion<string>().HasMaxLength(32);
        gameRelation.Property(value => value.CreatedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        gameRelation.ToTable(table =>
        {
            table.HasCheckConstraint("CK_GameRelations_DifferentGames", "\"GameId\" <> \"RelatedGameId\"");
            table.HasCheckConstraint(
                "CK_GameRelations_RelationType",
                "\"RelationType\" IN ('Dlc', 'Expansion', 'StandaloneExpansion')"
            );
        });
        gameRelation
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameRelation
            .HasOne(value => value.RelatedGame)
            .WithMany()
            .HasForeignKey(value => value.RelatedGameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
