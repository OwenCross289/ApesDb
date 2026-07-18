using ApesDb.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Teams;

public enum TeamMembershipStatus
{
    Invited = 0,
    Accepted = 1,
}

public sealed class TeamMembership
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Team Team { get; set; } = null!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public TeamMembershipStatus Status { get; set; }

    public Guid? InvitedByUserId { get; set; }

    public User? InvitedByUser { get; set; }

    public DateTime InvitedAt { get; set; }

    public DateTime? AcceptedAt { get; set; }
}

public sealed class TeamMembershipConfiguration : IEntityTypeConfiguration<TeamMembership>
{
    public void Configure(EntityTypeBuilder<TeamMembership> membership)
    {
        membership.HasKey(value => value.Id);
        membership.Property(value => value.Id).HasDefaultValueSql("uuidv7()").ValueGeneratedOnAdd();
        membership.Property(value => value.InvitedAt).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        membership.HasIndex(value => new { value.TeamId, value.UserId }).IsUnique();
        membership.HasIndex(value => new { value.UserId, value.Status });
        membership
            .HasOne(value => value.Team)
            .WithMany()
            .HasForeignKey(value => value.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
        membership
            .HasOne(value => value.User)
            .WithMany()
            .HasForeignKey(value => value.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        membership
            .HasOne(value => value.InvitedByUser)
            .WithMany()
            .HasForeignKey(value => value.InvitedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        membership.ToTable(table =>
        {
            table.HasCheckConstraint("CK_TeamMemberships_Status", "\"Status\" IN (0, 1)");
            table.HasCheckConstraint(
                "CK_TeamMemberships_Acceptance",
                "(\"Status\" = 0 AND \"AcceptedAt\" IS NULL) OR (\"Status\" = 1 AND \"AcceptedAt\" IS NOT NULL)"
            );
        });
    }
}
