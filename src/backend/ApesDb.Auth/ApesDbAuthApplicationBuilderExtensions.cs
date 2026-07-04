using Microsoft.AspNetCore.Builder;

namespace ApesDb.Auth;

public static class ApesDbAuthApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApesDbAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
