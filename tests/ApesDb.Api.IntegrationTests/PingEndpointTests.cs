using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApesDb.Api.Endpoints.Ping;
using Xunit;

namespace ApesDb.Api.IntegrationTests;

public sealed class PingEndpointTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    [Fact]
    public async Task GetPingReturnsPong()
    {
        using var client = factory.CreateClient();
        var cancellationToken = TestContext.Current.CancellationToken;

        var response = await client.GetAsync("/ping", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<PingResponse>(cancellationToken);

        Assert.NotNull(payload);
        Assert.Equal("pong", payload.Status);
    }
}
