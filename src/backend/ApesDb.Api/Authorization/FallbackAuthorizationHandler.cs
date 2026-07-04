using Microsoft.AspNetCore.Authorization;

namespace ApesDb.Api.Authorization;

public sealed record FallbackAuthorizationRequirement : IAuthorizationRequirement;

public sealed class FallbackAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<FallbackAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        FallbackAuthorizationRequirement requirement
    )
    {
        var httpContext = httpContextAccessor.HttpContext;

        // Allow anonymous access to the SPA and Swagger UI. API endpoints are protected
        // either by this fallback policy (for routes without explicit metadata) or by
        // their own AllowAnonymous/Authorize configuration.
        if (httpContext is not null && !httpContext.Request.Path.StartsWithSegments("/api"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.Identity?.IsAuthenticated == true)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
