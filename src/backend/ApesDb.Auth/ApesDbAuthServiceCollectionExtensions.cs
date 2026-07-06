using System.Security.Claims;
using ApesDb.Auth.Authentication;
using ApesDb.Auth.Authorization;
using ApesDb.Auth.Options;
using ApesDb.Auth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace ApesDb.Auth;

public static class ApesDbAuthServiceCollectionExtensions
{
    public static IServiceCollection AddApesDbAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<Auth0Options>()
            .BindConfiguration(Auth0Options.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddFusionCache(RedisTicketStore.CacheName)
            .WithCacheKeyPrefix(RedisTicketStore.CacheKeyPrefix)
            .TryWithAutoSetup();

        services.AddSingleton<ITicketStore, RedisTicketStore>();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthorizationHandler, FallbackAuthorizationHandler>();
        services.AddScoped<IUserProvisioningService, UserProvisioningService>();

        var auth0Options = configuration.GetSection(Auth0Options.SectionName).Get<Auth0Options>()!;

        services
            .AddAuthentication(options =>
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
                    options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapUniqueJsonKey("picture", "picture");

                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = context =>
                        {
                            if (
                                context.Properties.Items.TryGetValue("connection", out var connection)
                                && !string.IsNullOrWhiteSpace(connection)
                            )
                            {
                                context.ProtocolMessage.Parameters["connection"] = connection!;
                            }

                            return Task.CompletedTask;
                        },
                        OnTicketReceived = async context =>
                        {
                            var userProvisioningService =
                                context.HttpContext.RequestServices.GetRequiredService<IUserProvisioningService>();
                            var provisionedUser = await userProvisioningService.EnsureUserFromPrincipalAsync(
                                context.Principal!
                            );

                            context
                                .Principal!.Identities.First()
                                .AddClaim(new Claim("ApesDbUserId", provisionedUser.Id.ToString()));
                        },
                    };
                }
            );

        services
            .AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
            .Configure<ITicketStore>((options, store) => options.SessionStore = store);

        services
            .AddAuthorizationBuilder()
            .SetFallbackPolicy(
                new AuthorizationPolicyBuilder().AddRequirements(new FallbackAuthorizationRequirement()).Build()
            );

        return services;
    }
}
