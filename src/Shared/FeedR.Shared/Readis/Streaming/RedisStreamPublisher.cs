using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;
using StackExchange.Redis;

namespace FeedR.Shared.Readis.Streaming
{
    internal sealed class RedisStreamPublisher : IStreamPublisher
    {
        private readonly ISubscriber subscriber;
        private readonly ISerializer serializer;

        public RedisStreamPublisher(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
        {
            this.subscriber = connectionMultiplexer.GetSubscriber();
            this.serializer = serializer;
        }

        public Task PublishAsync<T>(string topic, T data) where T : class
        {
            var payload = serializer.Serialize(data);
            return this.subscriber.PublishAsync(topic, payload);
        }
    }
}
