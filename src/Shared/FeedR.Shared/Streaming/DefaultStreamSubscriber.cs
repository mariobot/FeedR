namespace FeedR.Shared.Streaming
{
    internal sealed class DefaultStreamSubscriber : IStreamSubscriber
    {
        public Task SubscribeAsync<T>(string topic, Action<T> handle) where T : class => Task.CompletedTask;
    }
}
