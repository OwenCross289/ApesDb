using System.Net;
using System.Threading.RateLimiting;
using ApesDb.Igdb.Sdk.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using Polly.RateLimiting;
using Polly.Retry;
using Polly.Timeout;
using Refit;

namespace ApesDb.Igdb.Sdk;

public static class IgdbSdkServiceCollectionExtensions
{
    public static IServiceCollection AddIgdbSdk(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddOptions<IgdbOptions>()
            .Bind(configuration.GetSection(IgdbOptions.SectionName))
            .Validate(
                options =>
                    !string.IsNullOrWhiteSpace(options.ClientId)
                    && !string.IsNullOrWhiteSpace(options.ClientSecret),
                "IGDB client credentials must be configured."
            )
            .Validate(
                options => Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _),
                "IGDB base URL must be an absolute URI."
            )
            .Validate(
                options => Uri.TryCreate(options.TokenUrl, UriKind.Absolute, out _),
                "IGDB token URL must be an absolute URI."
            );

        services.AddHttpClient<IIgdbAccessTokenClient, IgdbAccessTokenClient>();
        services.AddSingleton<IIgdbAccessTokenProvider, IgdbAccessTokenProvider>();
        services.AddTransient<IgdbAuthenticationHandler>();
        services.AddScoped<IIgdbGameService, IgdbGameService>();

        services
            .AddRefitClient<IIgdbApi>()
            .ConfigureHttpClient(
                (serviceProvider, client) =>
                {
                    var options = serviceProvider.GetRequiredService<IOptions<IgdbOptions>>().Value;
                    client.BaseAddress = new Uri(options.BaseUrl);
                }
            )
            .AddHttpMessageHandler<IgdbAuthenticationHandler>()
            .AddResilienceHandler(
                "igdb",
                static builder =>
                {
                    builder.AddRetry(
                        new HttpRetryStrategyOptions
                        {
                            MaxRetryAttempts = 3,
                            Delay = TimeSpan.FromMilliseconds(250),
                            BackoffType = DelayBackoffType.Exponential,
                            UseJitter = true,
                            ShouldHandle = static args =>
                                ValueTask.FromResult(ShouldRetry(args.Outcome)),
                        }
                    );
                    builder.AddRateLimiter(
                        new SlidingWindowRateLimiter(
                            new SlidingWindowRateLimiterOptions
                            {
                                PermitLimit = 4,
                                Window = TimeSpan.FromSeconds(1),
                                SegmentsPerWindow = 4,
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 0,
                            }
                        )
                    );
                }
            );

        return services;
    }

    private static bool ShouldRetry(Outcome<HttpResponseMessage> outcome)
    {
        if (
            outcome.Exception
            is HttpRequestException
                or TimeoutRejectedException
                or RateLimiterRejectedException
        )
        {
            return true;
        }

        return outcome.Result is { } response
            && (
                response.StatusCode
                    is HttpStatusCode.RequestTimeout
                        or HttpStatusCode.TooManyRequests
                || (int)response.StatusCode >= 500
            );
    }
}
