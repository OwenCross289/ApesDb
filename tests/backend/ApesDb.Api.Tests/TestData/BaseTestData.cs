namespace ApesDb.Api.Tests.TestData;

public sealed class BaseTestData
{
    public const int FranchiseGameCount = 201;
    public const int DependencyOnlyGameCount = 74;
    public const int TotalGameCount = FranchiseGameCount + DependencyOnlyGameCount;

    public static readonly Guid SharedTeamId = Guid.Parse("01910000-0000-7000-8000-000000002099");
    public static readonly Guid PendingInviteId = Guid.Parse("01910000-0000-7000-8000-000000003007");

    private BaseTestData(IReadOnlyList<object> entities)
    {
        Entities = entities;
    }

    public IReadOnlyList<object> Entities { get; }

    public static BaseTestData Create()
    {
        var allowedUsers = AllowedUserTestData.Create();
        var users = UserTestData.Create();
        var usersById = users.ToDictionary(user => user.Id);
        var profiles = ProfileTestData.Create(usersById);
        var teams = TeamTestData.Create();
        var teamsById = teams.ToDictionary(team => team.Id);
        var teamMemberships = TeamMembershipTestData.Create(teams, teamsById, usersById);
        var notifications = NotificationTestData.Create(usersById);

        var gameTypes = GameTypeTestData.Create();
        var gameStatuses = GameStatusTestData.Create();
        var genres = GenreTestData.Create();
        var themes = ThemeTestData.Create();
        var gameModes = GameModeTestData.Create();
        var playerPerspectives = PlayerPerspectiveTestData.Create();
        var platformTypes = PlatformTypeTestData.Create();
        var websiteTypes = WebsiteTypeTestData.Create();
        var collections = CollectionTestData.Create();
        var franchises = FranchiseTestData.Create();
        var companies = CompanyTestData.Create();
        var externalGameSources = ExternalGameSourceTestData.Create();
        var popularityTypes = PopularityTypeTestData.Create();
        var platforms = PlatformTestData.Create(platformTypes);
        var games = GameTestData.Create(gameTypes, gameStatuses);
        var platformLinks = PlatformLinkTestData.Create(platforms, websiteTypes);
        var gameGenres = GameGenreTestData.Create(games, genres);
        var gameThemes = GameThemeTestData.Create(games, themes);
        var gameGameModes = GameGameModeTestData.Create(games, gameModes);
        var gamePlayerPerspectives = GamePlayerPerspectiveTestData.Create(games, playerPerspectives);
        var gamePlatforms = GamePlatformTestData.Create(games, platforms);
        var gameCollections = GameCollectionTestData.Create(games, collections);
        var gameFranchises = GameFranchiseTestData.Create(games, franchises);
        var gameCompanies = GameCompanyTestData.Create(games, companies);
        var externalGames = ExternalGameTestData.Create(games, externalGameSources, platforms);
        var popularGames = PopularGameTestData.Create(games, popularityTypes);
        var gameRelations = GameRelationTestData.Create(games);

        var entities = new List<object>();
        entities.AddRange(allowedUsers);
        entities.AddRange(users);
        entities.AddRange(profiles);
        entities.AddRange(teams);
        entities.AddRange(teamMemberships);
        entities.AddRange(notifications);
        entities.AddRange(gameTypes.Values);
        entities.AddRange(gameStatuses.Values);
        entities.AddRange(genres.Values);
        entities.AddRange(themes.Values);
        entities.AddRange(gameModes.Values);
        entities.AddRange(playerPerspectives.Values);
        entities.AddRange(platformTypes.Values);
        entities.AddRange(websiteTypes.Values);
        entities.AddRange(collections.Values);
        entities.AddRange(franchises.Values);
        entities.AddRange(companies.Values);
        entities.AddRange(externalGameSources.Values);
        entities.AddRange(popularityTypes.Values);
        entities.AddRange(platforms.Values);
        entities.AddRange(games.Values);
        entities.AddRange(platformLinks);
        entities.AddRange(gameGenres);
        entities.AddRange(gameThemes);
        entities.AddRange(gameGameModes);
        entities.AddRange(gamePlayerPerspectives);
        entities.AddRange(gamePlatforms);
        entities.AddRange(gameCollections);
        entities.AddRange(gameFranchises);
        entities.AddRange(gameCompanies);
        entities.AddRange(externalGames);
        entities.AddRange(popularGames);
        entities.AddRange(gameRelations);
        return new BaseTestData(entities);
    }
}
