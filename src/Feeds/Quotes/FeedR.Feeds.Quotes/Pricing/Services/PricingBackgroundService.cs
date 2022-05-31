using FeedR.Feeds.Quotes.Pricing.Requests;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public class PricingBackgroundService : BackgroundService
    {
        private readonly IPriceGenerator _priceGenerator;
        private readonly PricingRequestsChannel _requestsChannel;

        public PricingBackgroundService(IPriceGenerator priceGenerator, PricingRequestsChannel requestsChannel)
        {
            _priceGenerator = priceGenerator;
            _requestsChannel = requestsChannel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var request in _requestsChannel.Requests.Reader.ReadAllAsync(stoppingToken))
            {
                var _ = request switch
                {
                    StartPricing => _priceGenerator.StartAsync(),
                    StopPricing => _priceGenerator.StopAsync(),
                    _ => Task.CompletedTask
                };
            }
        }
    }
}
