using ApesDb.Api;
using ApesDb.Api.Options;
using ApesDb.Igdb.Sdk;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddOptions<FrontendSpaOptions>()
    .BindConfiguration(FrontendSpaOptions.SectionName)
    .Validate(options => !string.IsNullOrWhiteSpace(options.DevServerUrl));
builder.Services.AddIgdbSdk(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "wwwroot";
});

var app = builder.Build();

app.UseRouting();
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
        var options = app.Services.GetRequiredService<IOptions<FrontendSpaOptions>>().Value;

        spa.UseProxyToSpaDevelopmentServer(options.DevServerUrl);
    }
});

app.Run();
