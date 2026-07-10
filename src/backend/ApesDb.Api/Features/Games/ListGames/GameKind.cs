using System.Text.Json.Serialization;

namespace ApesDb.Api.Features.Games.ListGames;

[JsonConverter(typeof(JsonStringEnumConverter<GameKind>))]
public enum GameKind
{
    [JsonStringEnumMemberName("main")]
    Main,

    [JsonStringEnumMemberName("dlc")]
    Dlc,

    [JsonStringEnumMemberName("expansion")]
    Expansion,

    [JsonStringEnumMemberName("standaloneExpansion")]
    StandaloneExpansion,
}
