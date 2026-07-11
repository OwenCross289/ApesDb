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
                    + "[Login with Google](/api/auth/login?connection=google&returnUrl=/swagger)"
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

app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next();
});

app.UseApesDbAuth();
app.UseSwaggerGen(uiConfig: ui =>
{
    ui.DocumentTitle = "ApesDb API";
    ui.CustomHeadContent += """
        <style>
            .apesdb-login-link {
                position: absolute;
                top: 20px;
                right: 20px;
                z-index: 1000;
                padding: 8px 16px;
                background: #007bff;
                color: white;
                text-decoration: none;
                border-radius: 4px;
                font-weight: bold;
                font-family: sans-serif;
                font-size: 14px;
            }
            .apesdb-login-link:hover { background: #0056b3; }
        </style>
        <a class="apesdb-login-link" href="/api/auth/login?connection=google&returnUrl=/swagger">Login with Google</a>
        """;
});
app.UseFastEndpoints(config => config.Endpoints.RoutePrefix = ApiRoutes.Api.Prefix);
app.UseEndpoints(_ => { });

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
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
