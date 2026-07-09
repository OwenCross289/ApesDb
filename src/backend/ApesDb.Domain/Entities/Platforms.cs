using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class Platform : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public string? Abbreviation { get; set; }

    public string? AlternativeName { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? IgdbUrl { get; set; }

    public long? IgdbPlatformTypeId { get; set; }

    public string? PlatformTypeName { get; set; }

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
    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;
}

public sealed class PlatformLink : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public Guid PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;

    public long? IgdbWebsiteTypeId { get; set; }

    public string? WebsiteTypeName { get; set; }

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
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public required string Name { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameExternalIdentifier : IIgdbEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public long IgdbId { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public Guid ExternalGameSourceId { get; set; }

    public ExternalGameSource ExternalGameSource { get; set; } = null!;

    public Guid? PlatformId { get; set; }

    public Platform? Platform { get; set; }

    public required string ExternalId { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public int? Year { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> platform)
    {
        platform.ToTable("Platforms");
        platform.ConfigureIgdbEntity();
        platform.Property(value => value.Name).HasMaxLength(256);
        platform.Property(value => value.Abbreviation).HasMaxLength(128);
        platform.Property(value => value.AlternativeName).HasMaxLength(256);
        platform.Property(value => value.Slug).HasMaxLength(256);
        platform.Property(value => value.IgdbUrl).HasMaxLength(2048);
        platform.Property(value => value.PlatformTypeName).HasMaxLength(256);
        platform.Property(value => value.LogoImageId).HasMaxLength(128);
        platform.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Platforms_Generation", "\"Generation\" IS NULL OR \"Generation\" >= 0");
            table.HasCheckConstraint("CK_Platforms_LogoWidth", "\"LogoWidth\" IS NULL OR \"LogoWidth\" > 0");
            table.HasCheckConstraint("CK_Platforms_LogoHeight", "\"LogoHeight\" IS NULL OR \"LogoHeight\" > 0");
        });
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
        platformLink.Property(value => value.WebsiteTypeName).HasMaxLength(256);
        platformLink.Property(value => value.Url).HasMaxLength(2048);
        platformLink
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
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

public sealed class GameExternalIdentifierConfiguration : IEntityTypeConfiguration<GameExternalIdentifier>
{
    public void Configure(EntityTypeBuilder<GameExternalIdentifier> externalIdentifier)
    {
        externalIdentifier.ToTable("GameExternalIdentifiers");
        externalIdentifier.ConfigureIgdbEntity();
        externalIdentifier.HasIndex(value => value.GameId);
        externalIdentifier.HasIndex(value => value.PlatformId);
        externalIdentifier.HasIndex(value => new { value.ExternalGameSourceId, value.ExternalId });
        externalIdentifier.Property(value => value.ExternalId).HasMaxLength(512);
        externalIdentifier.Property(value => value.Name).HasMaxLength(512);
        externalIdentifier.Property(value => value.Url).HasMaxLength(2048);
        externalIdentifier.ToTable(table =>
            table.HasCheckConstraint(
                "CK_GameExternalIdentifiers_Year",
                "\"Year\" IS NULL OR \"Year\" BETWEEN 0 AND 9999"
            )
        );
        externalIdentifier
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        externalIdentifier
            .HasOne(value => value.ExternalGameSource)
            .WithMany()
            .HasForeignKey(value => value.ExternalGameSourceId)
            .HasConstraintName("FK_GameExternalIdentifiers_ExternalSource")
            .OnDelete(DeleteBehavior.Restrict);
        externalIdentifier
            .HasOne(value => value.Platform)
            .WithMany()
            .HasForeignKey(value => value.PlatformId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
