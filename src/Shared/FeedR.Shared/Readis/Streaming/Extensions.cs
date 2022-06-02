using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Readis.Streaming;

public static class Extensions
{
    public static IServiceCollection AddRedisStreaming(this IServiceCollection service) =>
        service.AddSingleton<IStreamPublisher, RedisStreamPublisher>()
                .AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();
}

