using ApesDb.Api;
using ApesDb.Auth;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Igdb.Sdk;
using FastEndpoints;
using FastEndpoints.Swagger;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApesDbForwardedHeaders();
builder.Services.AddApesDbCommon();
builder.Services.AddApesDbCache(builder.Configuration);
builder.Services.AddApesDbDataProtection();
builder.Services.AddApesDbDomain(builder.Configuration);
builder.Services.AddApesDbAuth(builder.Configuration);

builder.Services.AddIgdbSdk(builder.Configuration);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
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
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "wwwroot";
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();

app.Use(
    (context, next) =>
    {
        context.Request.Scheme = "https";
        return next();
    }
);

app.UseApesDbAuth();
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
app.UseFastEndpoints(config => config.Endpoints.RoutePrefix = ApiRoutes.Api.Prefix);
app.UseEndpoints(_ => { });

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles(
        new StaticFileOptions
        {
            OnPrepareResponse = context =>
            {
                if (!string.Equals(context.File.Name, "sw.js", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                context.Context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
                context.Context.Response.Headers.Pragma = "no-cache";
                context.Context.Response.Headers.Expires = "0";
            },
        }
    );
}

app.UseSpa(spa =>
{
    spa.Options.SourcePath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "../../frontend/apesdb"));

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    }
});

app.Run();
