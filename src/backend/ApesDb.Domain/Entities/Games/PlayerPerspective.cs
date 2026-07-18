using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApesDb.Domain.Entities.Games;

public sealed class PlayerPerspective : IIgdbEntity
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

public sealed class PlayerPerspectiveConfiguration : IEntityTypeConfiguration<PlayerPerspective>
{
    public void Configure(EntityTypeBuilder<PlayerPerspective> playerPerspective)
    {
        playerPerspective.ToTable("PlayerPerspectives");
        playerPerspective.ConfigureIgdbEntity();
        GenreConfiguration.ConfigureTaxonomyFields(playerPerspective);
    }
}
