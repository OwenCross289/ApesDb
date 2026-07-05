using ApesDb.Api;
using ApesDb.Auth;
using ApesDb.Common;
using ApesDb.Domain;
using ApesDb.Igdb.Sdk;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApesDbForwardedHeaders();
builder.Services.AddApesDbCommon();
builder.Services.AddApesDbCache(builder.Configuration);
builder.Services.AddApesDbDataProtection();
builder.Services.AddApesDbDomain(builder.Configuration);
builder.Services.AddApesDbAuth(builder.Configuration);

builder.Services.AddIgdbSdk(builder.Configuration);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "wwwroot";
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();
app.UseApesDbAuth();
app.UseSwaggerGen();
app.UseFastEndpoints(config => config.Endpoints.RoutePrefix = ApiRoutes.Api.Prefix);
app.UseEndpoints(_ => { });

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseSpa(spa =>
{
    spa.Options.SourcePath = Path.GetFullPath(
        Path.Combine(app.Environment.ContentRootPath, "../../frontend/apesdb")
    );

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    }
});

app.Run();
