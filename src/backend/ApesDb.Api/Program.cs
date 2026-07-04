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
builder.Services.AddApesDbDomain(builder.Configuration);
builder.Services.AddApesDbAuth(builder.Configuration);

builder.Services.AddIgdbSdk(builder.Configuration);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = builder.Environment.IsDevelopment()
        ? Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "../../frontend/apesdb/dist"))
        : "wwwroot";
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();
app.UseApesDbAuth();
app.UseSwaggerGen();
app.UseFastEndpoints(config => config.Endpoints.RoutePrefix = ApiRoutes.Api.Prefix);
app.UseEndpoints(_ => { });

app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = Path.GetFullPath(
        Path.Combine(app.Environment.ContentRootPath, "../../frontend/apesdb/dist")
    );
});

app.Run();
