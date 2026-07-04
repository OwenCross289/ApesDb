using ApesDb.Api;
using ApesDb.Api.Options;
using ApesDb.Auth;
using ApesDb.Domain;
using ApesDb.Igdb.Sdk;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddOptions<CacheOptions>()
    .BindConfiguration(CacheOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

var cacheOptions = builder.Configuration.GetRequiredSection(CacheOptions.SectionName).Get<CacheOptions>()!;
var redisConfiguration = BuildRedisConfiguration(cacheOptions);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = redisConfiguration;
});

builder.Services.AddFusionCacheSystemTextJsonSerializer();
builder.Services.AddFusionCacheStackExchangeRedisBackplane(options =>
{
    options.ConfigurationOptions = redisConfiguration;
});

builder.Services.AddFusionCache().TryWithAutoSetup();

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

static ConfigurationOptions BuildRedisConfiguration(CacheOptions options)
{
    var configuration = ConfigurationOptions.Parse(options.ConnectionString);
    configuration.Password = options.Password;
    return configuration;
}
