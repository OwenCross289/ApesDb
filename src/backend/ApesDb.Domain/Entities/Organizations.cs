using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public sealed class Company : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public int? CountryCode { get; set; }

    public string? IgdbUrl { get; set; }

    public string? LogoImageId { get; set; }

    public int? LogoWidth { get; set; }

    public int? LogoHeight { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameCompany : IIgdbEntity
{
    public long Id { get; set; }

    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public bool Developer { get; set; }

    public bool Publisher { get; set; }

    public bool Porting { get; set; }

    public bool Supporting { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class Collection : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameCollection
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long CollectionId { get; set; }

    public Collection Collection { get; set; } = null!;
}

public sealed class Franchise : IIgdbEntity
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public string? Slug { get; set; }

    public string? IgdbUrl { get; set; }

    public Guid? Checksum { get; set; }

    public DateTime? IgdbUpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }
}

public sealed class GameFranchise
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long FranchiseId { get; set; }

    public Franchise Franchise { get; set; } = null!;
}

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> company)
    {
        company.ToTable("Companies");
        company.ConfigureIgdbEntity();
        company.Property(value => value.Name).HasMaxLength(512);
        company.Property(value => value.Slug).HasMaxLength(512);
        company.Property(value => value.IgdbUrl).HasMaxLength(2048);
        company.Property(value => value.LogoImageId).HasMaxLength(128);
        company.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Companies_LogoWidth", "\"LogoWidth\" IS NULL OR \"LogoWidth\" > 0");
            table.HasCheckConstraint("CK_Companies_LogoHeight", "\"LogoHeight\" IS NULL OR \"LogoHeight\" > 0");
        });
    }
}

public sealed class GameCompanyConfiguration : IEntityTypeConfiguration<GameCompany>
{
    public void Configure(EntityTypeBuilder<GameCompany> gameCompany)
    {
        gameCompany.ToTable("GameCompanies");
        gameCompany.ConfigureIgdbEntity();
        gameCompany.HasIndex(value => value.GameId);
        gameCompany.HasIndex(value => value.CompanyId);
        gameCompany.ToTable(table =>
            table.HasCheckConstraint(
                "CK_GameCompanies_Role",
                "\"Developer\" OR \"Publisher\" OR \"Porting\" OR \"Supporting\""
            )
        );
        gameCompany
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameCompany
            .HasOne(value => value.Company)
            .WithMany()
            .HasForeignKey(value => value.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> collection)
    {
        collection.ToTable("Collections");
        collection.ConfigureIgdbEntity();
        collection.Property(value => value.Name).HasMaxLength(512);
        collection.Property(value => value.Slug).HasMaxLength(512);
        collection.Property(value => value.IgdbUrl).HasMaxLength(2048);
    }
}

public sealed class GameCollectionConfiguration : IEntityTypeConfiguration<GameCollection>
{
    public void Configure(EntityTypeBuilder<GameCollection> gameCollection)
    {
        gameCollection.ToTable("GameCollections");
        gameCollection.HasKey(value => new { value.GameId, value.CollectionId });
        gameCollection.HasIndex(value => value.CollectionId);
        gameCollection
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameCollection
            .HasOne(value => value.Collection)
            .WithMany()
            .HasForeignKey(value => value.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
{
    public void Configure(EntityTypeBuilder<Franchise> franchise)
    {
        franchise.ToTable("Franchises");
        franchise.ConfigureIgdbEntity();
        franchise.Property(value => value.Name).HasMaxLength(512);
        franchise.Property(value => value.Slug).HasMaxLength(512);
        franchise.Property(value => value.IgdbUrl).HasMaxLength(2048);
    }
}

public sealed class GameFranchiseConfiguration : IEntityTypeConfiguration<GameFranchise>
{
    public void Configure(EntityTypeBuilder<GameFranchise> gameFranchise)
    {
        gameFranchise.ToTable("GameFranchises");
        gameFranchise.HasKey(value => new { value.GameId, value.FranchiseId });
        gameFranchise.HasIndex(value => value.FranchiseId);
        gameFranchise
            .HasOne(value => value.Game)
            .WithMany()
            .HasForeignKey(value => value.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        gameFranchise
            .HasOne(value => value.Franchise)
            .WithMany()
            .HasForeignKey(value => value.FranchiseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
