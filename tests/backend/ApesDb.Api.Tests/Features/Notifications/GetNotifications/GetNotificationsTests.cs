using ApesDb.Api.Tests.Infrastructure.Authentication;
using ApesDb.Api.Tests.Infrastructure.Factories;
using ApesDb.Api.Tests.Infrastructure.Http;
using NotificationListResponse = ApesDb.Api.Features.Notifications.GetNotifications.NotificationsResponse;

namespace ApesDb.Api.Tests.Features.Notifications.GetNotifications;

public sealed class GetNotificationsTests
{
    private readonly SharedGetApiFactory _factory;

    public GetNotificationsTests(SharedGetApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AnonymousUserCannotGetNotifications()
    {
        using var client = ApiTestClient.CreateAnonymous(_factory);
        using var response = await client.GetAsync("/api/notifications", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<object>(response));
    }

    [Theory]
    [InlineData("owner")]
    [InlineData("member")]
    [InlineData("invitee")]
    [InlineData("outsider")]
    public async Task ExistingUserSeesExpectedNotifications(string identityKey)
    {
        using var client = ApiTestClient.CreateAuthenticated(_factory, TestUsers.Find(identityKey)!);
        using var response = await client.GetAsync("/api/notifications", TestContext.Current.CancellationToken);

        await Verify(await HttpResponseSnapshot.CreateAsync<NotificationListResponse>(response))
            .UseParameters(identityKey);
    }
}
