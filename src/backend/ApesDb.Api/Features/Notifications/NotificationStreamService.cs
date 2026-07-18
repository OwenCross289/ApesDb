using System.Collections.Concurrent;
using System.Threading.Channels;

namespace ApesDb.Api.Features.Notifications;

public sealed class NotificationStreamService
{
    private readonly ConcurrentDictionary<
        Guid,
        ConcurrentDictionary<Guid, Channel<NotificationStreamEvent>>
    > _subscribers = new();

    public NotificationStreamSubscription Subscribe(Guid userId)
    {
        var channel = Channel.CreateUnbounded<NotificationStreamEvent>(
            new UnboundedChannelOptions { SingleReader = true, SingleWriter = false }
        );
        var subscriptionId = Guid.CreateVersion7();
        var channels = _subscribers.GetOrAdd(
            userId,
            _ => new ConcurrentDictionary<Guid, Channel<NotificationStreamEvent>>()
        );
        channels[subscriptionId] = channel;
        return new NotificationStreamSubscription(this, userId, subscriptionId, channel);
    }

    public void Publish(Guid userId, NotificationStreamEvent streamEvent)
    {
        if (!_subscribers.TryGetValue(userId, out var channels))
        {
            return;
        }

        foreach (var channel in channels.Values)
        {
            channel.Writer.TryWrite(streamEvent);
        }
    }

    private void Unsubscribe(Guid userId, Guid subscriptionId, Channel<NotificationStreamEvent> channel)
    {
        if (_subscribers.TryGetValue(userId, out var channels))
        {
            channels.TryRemove(subscriptionId, out _);
            if (channels.IsEmpty)
            {
                _subscribers.TryRemove(userId, out _);
            }
        }

        channel.Writer.TryComplete();
    }

    public sealed class NotificationStreamSubscription : IDisposable
    {
        private readonly NotificationStreamService _owner;
        private readonly Guid _userId;
        private readonly Guid _subscriptionId;
        private readonly Channel<NotificationStreamEvent> _channel;

        internal NotificationStreamSubscription(
            NotificationStreamService owner,
            Guid userId,
            Guid subscriptionId,
            Channel<NotificationStreamEvent> channel
        )
        {
            _owner = owner;
            _userId = userId;
            _subscriptionId = subscriptionId;
            _channel = channel;
        }

        public ChannelReader<NotificationStreamEvent> Reader => _channel.Reader;

        public void Dispose()
        {
            _owner.Unsubscribe(_userId, _subscriptionId, _channel);
        }
    }
}
