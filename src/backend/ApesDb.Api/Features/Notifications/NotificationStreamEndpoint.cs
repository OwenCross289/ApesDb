using System.Text.Json;
using FastEndpoints;

namespace ApesDb.Api.Features.Notifications;

public sealed class NotificationStreamEndpoint : EndpointWithoutRequest
{
    internal static readonly TimeSpan HeartbeatInterval = TimeSpan.FromSeconds(25);

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly NotificationStreamService _streamService;

    public NotificationStreamEndpoint(NotificationStreamService streamService)
    {
        _streamService = streamService;
    }

    public override void Configure()
    {
        Get(ApiRoutes.Notifications.Stream);
        Summary(summary => summary.Summary = "Streams notification events to the client over server-sent events.");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.GetApesDbUserId();
        var requestAborted = HttpContext.RequestAborted;

        HttpContext.Response.ContentType = "text/event-stream";
        HttpContext.Response.Headers.CacheControl = "no-cache";
        HttpContext.Response.Headers["X-Accel-Buffering"] = "no";

        using var subscription = _streamService.Subscribe(userId);
        var reader = subscription.Reader;

        try
        {
            await WriteFrameAsync(": connected", requestAborted);

            while (!requestAborted.IsCancellationRequested)
            {
                var readTask = reader.WaitToReadAsync(requestAborted).AsTask();
                var completed = await Task.WhenAny(readTask, Task.Delay(HeartbeatInterval, requestAborted));
                if (completed != readTask)
                {
                    await WriteFrameAsync(": ping", requestAborted);
                    continue;
                }

                if (!readTask.Result)
                {
                    break;
                }

                while (reader.TryRead(out var streamEvent))
                {
                    var payload = JsonSerializer.Serialize(streamEvent.Data, SerializerOptions);
                    await WriteFrameAsync($"event: {streamEvent.Kind}\ndata: {payload}", requestAborted);
                }
            }
        }
        catch (OperationCanceledException) when (requestAborted.IsCancellationRequested)
        {
            // The client disconnected; nothing left to do.
        }
    }

    private async Task WriteFrameAsync(string frame, CancellationToken requestAborted)
    {
        await HttpContext.Response.WriteAsync($"{frame}\n\n", requestAborted);
        await HttpContext.Response.Body.FlushAsync(requestAborted);
    }
}
