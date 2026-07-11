using FastEndpoints.Swagger;
using NSwag;

namespace ApesDb.Api;

public static class ApesDbApiSwaggerExtensions
{
    public static IServiceCollection AddApesDbSwagger(this IServiceCollection services)
    {
        services.SwaggerDocument(o =>
        {
            o.EnableJWTBearerAuth = false;

            o.DocumentSettings = s =>
            {
                s.Title = "ApesDb API";
                s.Version = "v1";

                s.AddAuth(
                    "SessionCookie",
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        In = OpenApiSecurityApiKeyLocation.Cookie,
                        Name = "apesdb.session",
                        Description =
                            "Stateful session cookie issued after Auth0/Google login. "
                            + "[Login with Google](/api/auth/login?connection=google&returnUrl=/swagger)",
                    }
                );
            };
        });

        return services;
    }

    public static IApplicationBuilder UseApesDbSwagger(this IApplicationBuilder app)
    {
        app.UseSwaggerGen(uiConfig: ui =>
        {
            ui.DocumentTitle = "ApesDb API";
            ui.CustomHeadContent += """
                <style>
                    .swagger-ui .scheme-container .schemes .auth-wrapper .authorize { display: none !important; }
                    .swagger-ui .apesdb-login-link {
                        display: inline-block !important;
                        padding: 4px 16px !important;
                        background-color: #007bff !important;
                        color: #fff !important;
                        text-decoration: none !important;
                        border-radius: 4px !important;
                        font-weight: bold !important;
                        font-family: sans-serif !important;
                        font-size: 14px !important;
                        line-height: 1.5 !important;
                        border: 1px solid #007bff !important;
                    }
                    .swagger-ui .apesdb-login-link:hover {
                        background-color: #0056b3 !important;
                        border-color: #0056b3 !important;
                        color: #fff !important;
                    }
                </style>
                <script>
                    document.addEventListener('DOMContentLoaded', function () {
                        function setupLoginButton() {
                            var authWrapper = document.querySelector('.scheme-container .auth-wrapper');
                            if (!authWrapper) {
                                setTimeout(setupLoginButton, 100);
                                return;
                            }

                            var existing = document.querySelector('.apesdb-login-link');
                            if (existing) {
                                existing.remove();
                            }

                            var link = document.createElement('a');
                            link.href = '/api/auth/login?connection=google&returnUrl=/swagger';
                            link.className = 'apesdb-login-link';
                            link.textContent = 'Login with Google';

                            authWrapper.appendChild(link);
                        }

                        setupLoginButton();
                    });
                </script>
                """;
        });

        return app;
    }
}
