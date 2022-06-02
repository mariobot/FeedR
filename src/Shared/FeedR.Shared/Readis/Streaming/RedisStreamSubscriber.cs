using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;
using StackExchange.Redis;

namespace FeedR.Shared.Readis.Streaming
{
    internal sealed class RedisStreamSubscriber : IStreamSubscriber
    {
        private readonly ISubscriber subscriber;
        private readonly ISerializer serializer;

        public RedisStreamSubscriber(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
        {
            this.subscriber = connectionMultiplexer.GetSubscriber();
            this.serializer = serializer;
        }
        public Task SubscribeAsync<T>(string topic, Action<T> handle) where T : class
            => subscriber.SubscribeAsync(topic, (_, data) =>
            {
                var payload = serializer.Deserialize<T>(data);
                if (payload is null)
                {
                    return;
                }

                handle(payload);
            });
    }
}
