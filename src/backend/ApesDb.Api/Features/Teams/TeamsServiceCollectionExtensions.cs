namespace ApesDb.Api.Features.Teams;

public static class TeamsServiceCollectionExtensions
{
    public static IServiceCollection AddTeams(this IServiceCollection services)
    {
        services.AddSingleton<ITeamProfilePictureProcessor, TeamProfilePictureProcessor>();

        return services;
    }
}
