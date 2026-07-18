using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace ApesDb.Api;

internal static class ResponseSenderExtensions
{
    public static Task AcceptedAsync<TRequest, TResponse>(this ResponseSender<TRequest, TResponse> sender)
        where TRequest : notnull
    {
        return sender.ResultAsync(TypedResults.Accepted((string?)null));
    }

    public static Task ConflictAsync<TRequest, TResponse>(this ResponseSender<TRequest, TResponse> sender)
        where TRequest : notnull
    {
        return sender.ResultAsync(TypedResults.Conflict());
    }
}
