using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class GamePlatform
{
    public long GameId { get; set; }

    public Game Game { get; set; } = null!;

    public long PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;
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
