using System.Security.Claims;
using ApesDb.Api;
using ApesDb.Api.Authentication;
using ApesDb.Api.Authorization;
using ApesDb.Api.Options;
using ApesDb.Api.Services;
using ApesDb.Domain;
using ApesDb.Igdb.Sdk;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddOptions<Auth0Options>()
    .BindConfiguration(Auth0Options.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

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

var cacheOptions = builder
    .Configuration.GetRequiredSection(CacheOptions.SectionName)
    .Get<CacheOptions>()!;
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

builder
    .Services.AddFusionCache(RedisTicketStore.CacheName)
    .WithCacheKeyPrefix(RedisTicketStore.CacheKeyPrefix)
    .TryWithAutoSetup();

builder.Services.AddSingleton<ITicketStore, RedisTicketStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthorizationHandler, FallbackAuthorizationHandler>();

builder.Services.AddApesDbDomain(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddIgdbSdk(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = builder.Environment.IsDevelopment()
        ? Path.GetFullPath(
            Path.Combine(builder.Environment.ContentRootPath, "../../frontend/apesdb/dist")
        )
        : "wwwroot";
});

var auth0Options = builder.Configuration.GetSection(Auth0Options.SectionName).Get<Auth0Options>()!;

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "apesdb.session";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;

        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(
        "Auth0",
        options =>
        {
            options.Authority = $"https://{auth0Options.Domain}";
            options.ClientId = auth0Options.ClientId;
            options.ClientSecret = auth0Options.ClientSecret;
            options.CallbackPath = auth0Options.CallbackPath;
            options.ResponseType = "code";
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.SaveTokens = false;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.ClaimActions.MapUniqueJsonKey("email", "email");
            options.ClaimActions.MapUniqueJsonKey("name", "name");

            options.Events = new OpenIdConnectEvents
            {
                OnTicketReceived = async context =>
                {
                    var userService =
                        context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    var user = await userService.EnsureUserAsync(context.Principal!);

                    context
                        .Principal!.Identities.First()
                        .AddClaim(new Claim("ApesDbUserId", user.Id.ToString()));
                },
            };
        }
    );

builder
    .Services.AddOptions<CookieAuthenticationOptions>(
        CookieAuthenticationDefaults.AuthenticationScheme
    )
    .Configure<ITicketStore>((options, store) => options.SessionStore = store);

builder
    .Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(
        new AuthorizationPolicyBuilder()
            .AddRequirements(new FallbackAuthorizationRequirement())
            .Build()
    );

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
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
