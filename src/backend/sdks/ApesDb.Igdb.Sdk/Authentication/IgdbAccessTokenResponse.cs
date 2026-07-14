using System.Text.Json.Serialization;

namespace ApesDb.Igdb.Sdk.Authentication;

internal sealed record IgdbAccessTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("token_type")] string TokenType
);
