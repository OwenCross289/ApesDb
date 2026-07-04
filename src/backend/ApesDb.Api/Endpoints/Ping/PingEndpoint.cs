using FastEndpoints;

namespace ApesDb.Api.Endpoints.Ping;

public sealed class PingEndpoint : EndpointWithoutRequest<PingResponse>
{
    public override void Configure()
    {
        Get(ApiRoutes.Ping.Path);
        AllowAnonymous();
        Summary(summary =>
        {
            summary.Summary = "Health-style ping endpoint.";
            summary.Description = "Returns a basic payload so the API host can be verified quickly.";
        });
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return Send.OkAsync(new PingResponse("pong"), ct);
    }
}
