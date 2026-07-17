using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities;

public enum TeamKind
{
    Solo,
    Group,
}

public sealed class Team
{
    public Guid Id { get; init; }

    public Guid OwnerUserId { get; init; }

    public required string Name { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public TeamKind Kind { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; set; }
}

public sealed class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> team)
    {
        team.HasKey(t => t.Id);
        // The unique partial index UX_Teams_OwnerUserId_Solo (one solo team per user) is created
        // by the Flyway migration and intentionally not modeled here: EF Core cannot represent two
        // indexes over the same property set, and only the lookup index below matters for queries.
        team.HasIndex(t => t.OwnerUserId);
        team.Property(t => t.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        team.Property(t => t.Name).HasMaxLength(256);
        team.Property(t => t.ProfilePictureUrl).HasMaxLength(2048);
        team.Property(t => t.Kind).HasConversion<string>().HasMaxLength(32);
        team.Property(t => t.CreatedAt).HasDefaultValueSql("now()");
        team.Property(t => t.UpdatedAt).HasDefaultValueSql("now()");
        team.HasOne<User>().WithMany().HasForeignKey(t => t.OwnerUserId).OnDelete(DeleteBehavior.Cascade);
        team.ToTable(t => t.HasCheckConstraint("CK_Teams_Kind", "\"Kind\" IN ('Solo', 'Group')"));
    }
}
