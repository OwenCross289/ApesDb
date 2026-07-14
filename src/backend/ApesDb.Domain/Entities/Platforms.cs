using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class PlatformType : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class WebsiteType : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class Platform : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public string? Abbreviation { get; set; }

    public string? AlternativeName { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? IgdbUrl { get; set; }

    public long? PlatformTypeId { get; set; }

    public PlatformType? PlatformType { get; set; }

    public int? Generation { get; set; }

    public string? LogoImageId { get; set; }

    public int? LogoWidth { get; set; }

    public int? LogoHeight { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GamePlatform
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;
}

public sealed class PlatformLink : IIgdbEntity
{
    public long Id { get; set; }

    public long PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;

    public long WebsiteTypeId { get; set; }

    public WebsiteType WebsiteType { get; set; } = null!;

    public required string Url { get; set; }

    public bool Trusted { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class ExternalGameSource : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class ExternalGame : IIgdbEntity
{
    public long Id { get; set; }

    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long ExternalGameSourceId { get; set; }

    public ExternalGameSource ExternalGameSource { get; set; } = null!;

    public long? PlatformId { get; set; }

    public Platform? Platform { get; set; }

    public string? ExternalId { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public int? Year { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
{
    public void Configure(EntityTypeBuilder<PlatformType> platformType)
    {
        platformType.ToTable("PlatformTypes");
        platformType.ConfigureIgdbEntity();
        platformType.Property(value => value.Name).HasMaxLength(256);
    }
}

public sealed class WebsiteTypeConfiguration : IEntityTypeConfiguration<WebsiteType>
{
    public void Configure(EntityTypeBuilder<WebsiteType> websiteType)
    {
        websiteType.ToTable("WebsiteTypes");
        websiteType.ConfigureIgdbEntity();
        websiteType.Property(value => value.Name).HasMaxLength(256);
    }
}

public sealed class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> platform)
    {
        platform.ToTable("Platforms");
        platform.ConfigureIgdbEntity();
        platform.HasIndex(value => value.PlatformTypeId);
        platform.Property(value => value.Name).HasMaxLength(256);
        platform.Property(value => value.Abbreviation).HasMaxLength(128);
        platform.Property(value => value.AlternativeName).HasMaxLength(256);
        platform.Property(value => value.Slug).HasMaxLength(256);
        platform.Property(value => value.IgdbUrl).HasMaxLength(2048);
        platform.Property(value => value.LogoImageId).HasMaxLength(128);
        platform.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Platforms_Generation", "\"Generation\" IS NULL OR \"Generation\" >= 0");
            table.HasCheckConstraint("CK_Platforms_LogoWidth", "\"LogoWidth\" IS NULL OR \"LogoWidth\" > 0");
            table.HasCheckConstraint("CK_Platforms_LogoHeight", "\"LogoHeight\" IS NULL OR \"LogoHeight\" > 0");
        });
        platform
            .HasOne(value => value.PlatformType)
            .WithMany()
            .HasForeignKey(value => value.PlatformTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public sealed class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> gamePlatform)
    {
        gamePlatform.ToTable("GamePlatforms");
        gamePlatform.HasKey(value => new { value.GameId, value.PlatformId });
        gamePlatform.HasIndex(value => value.PlatformId);
        gamePlatform
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gamePlatform
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class PlatformLinkConfiguration : IEntityTypeConfiguration<PlatformLink>
{
    public void Configure(EntityTypeBuilder<PlatformLink> platformLink)
    {
        platformLink.ToTable("PlatformLinks");
        platformLink.ConfigureIgdbEntity();
        platformLink.HasIndex(value => value.PlatformId);
        platformLink.HasIndex(value => value.WebsiteTypeId);
        platformLink.Property(value => value.Url).HasMaxLength(2048);
        platformLink
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
        platformLink
            .HasOne(value => value.WebsiteType)
            .WithMany()
            .HasForeignKey(value => value.WebsiteTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public sealed class ExternalGameSourceConfiguration : IEntityTypeConfiguration<ExternalGameSource>
{
    public void Configure(EntityTypeBuilder<ExternalGameSource> externalGameSource)
    {
        externalGameSource.ToTable("ExternalGameSources");
        externalGameSource.ConfigureIgdbEntity();
        externalGameSource.Property(value => value.Name).HasMaxLength(256);
    }
}

public sealed class ExternalGameConfiguration : IEntityTypeConfiguration<ExternalGame>
{
    public void Configure(EntityTypeBuilder<ExternalGame> externalGame)
    {
        externalGame.ToTable("ExternalGames");
        externalGame.ConfigureIgdbEntity();
        externalGame.HasIndex(value => value.GameId);
        externalGame.HasIndex(value => value.ExternalGameSourceId);
        externalGame.HasIndex(value => value.PlatformId);
        externalGame.HasIndex(value => new { value.ExternalGameSourceId, value.ExternalId });
        externalGame.Property(value => value.ExternalId).HasMaxLength(512);
        externalGame.Property(value => value.Name).HasMaxLength(512);
        externalGame.Property(value => value.Url).HasMaxLength(2048);
        externalGame.ToTable(table =>
            table.HasCheckConstraint("CK_ExternalGames_Year", "\"Year\" IS NULL OR \"Year\" BETWEEN 0 AND 9999")
        );
        externalGame
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        externalGame
            .HasOne(value => value.ExternalGameSource)
            .WithMany()
            .HasForeignKey(value => value.ExternalGameSourceId)
            .HasConstraintName("FK_ExternalGames_ExternalGameSources_ExternalGameSourceId")
            .OnDelete(DeleteBehavior.Restrict);
        externalGame
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
