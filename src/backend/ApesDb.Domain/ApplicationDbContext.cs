using ApesDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApesDb.Domain;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Game> Games => Set<Game>();

    public DbSet<GameType> GameTypes => Set<GameType>();

    public DbSet<GameStatus> GameStatuses => Set<GameStatus>();

    public DbSet<PopularGame> PopularGames => Set<PopularGame>();

    public DbSet<GameRelation> GameRelations => Set<GameRelation>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<GameGenre> GameGenres => Set<GameGenre>();

    public DbSet<Theme> Themes => Set<Theme>();

    public DbSet<GameTheme> GameThemes => Set<GameTheme>();

    public DbSet<GameMode> GameModes => Set<GameMode>();

    public DbSet<GameGameMode> GameGameModes => Set<GameGameMode>();

    public DbSet<PlayerPerspective> PlayerPerspectives => Set<PlayerPerspective>();

    public DbSet<GamePlayerPerspective> GamePlayerPerspectives => Set<GamePlayerPerspective>();

    public DbSet<Platform> Platforms => Set<Platform>();

    public DbSet<GamePlatform> GamePlatforms => Set<GamePlatform>();

    public DbSet<PlatformLink> PlatformLinks => Set<PlatformLink>();

    public DbSet<ExternalGameSource> ExternalGameSources => Set<ExternalGameSource>();

    public DbSet<GameExternalIdentifier> GameExternalIdentifiers => Set<GameExternalIdentifier>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<GameCompany> GameCompanies => Set<GameCompany>();

    public DbSet<Collection> Collections => Set<Collection>();

    public DbSet<GameCollection> GameCollections => Set<GameCollection>();

    public DbSet<Franchise> Franchises => Set<Franchise>();

    public DbSet<GameFranchise> GameFranchises => Set<GameFranchise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
