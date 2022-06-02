using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services
{
    internal sealed class PricingStreamBackgroundService : BackgroundService
    {
        private readonly IStreamSubscriber streamSubscriber;
        private readonly ILogger<PricingStreamBackgroundService> logger;

        public PricingStreamBackgroundService(IStreamSubscriber streamSubscriber, ILogger<PricingStreamBackgroundService> logger)
        {
            this.streamSubscriber = streamSubscriber;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await streamSubscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair => {
                logger.LogInformation($"Pricing {currencyPair.Symbol} = {currencyPair.Value:F}, timestamp:{currencyPair.Timestamp}");
            });
        }
    }

    internal sealed record CurrencyPair(string Symbol, decimal Value, long Timestamp);
}
