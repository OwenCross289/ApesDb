using System;
using ApesDb.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApesDb.Api.IntegrationTests;

public sealed class ApiFactory : WebApplicationFactory<IApiMarker>
{
    static ApiFactory()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__Postgres", "InMemory");
        Environment.SetEnvironmentVariable("ConnectionStrings__Redis", "unused:6379");
        Environment.SetEnvironmentVariable("Auth0__Domain", "test.auth0.com");
        Environment.SetEnvironmentVariable("Auth0__ClientId", "test-client-id");
        Environment.SetEnvironmentVariable("Auth0__ClientSecret", "test-client-secret");
        Environment.SetEnvironmentVariable("Igdb__ClientId", "test-igdb-client-id");
        Environment.SetEnvironmentVariable("Igdb__ClientSecret", "test-igdb-client-secret");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

            services
                .AddAuthentication(TestAuthHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    TestAuthHandler.SchemeName,
                    _ => { }
                );

            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
                options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
                options.DefaultSignInScheme = TestAuthHandler.SchemeName;
                options.DefaultSignOutScheme = TestAuthHandler.SchemeName;
            });

            services.Configure<CookieAuthenticationOptions>(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                    options.Cookie.HttpOnly = false;
                }
            );
        });
    }
}
