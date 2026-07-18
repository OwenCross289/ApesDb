using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GameFranchise
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long FranchiseId { get; set; }

    public Franchise Franchise { get; set; } = null!;
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
