namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public class PricingBackgroundService : IHostedService
    {
        private readonly IPriceGenerator _priceGenerator;

        public PricingBackgroundService(IPriceGenerator priceGenerator)
        {
            _priceGenerator = priceGenerator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _ = _priceGenerator.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _priceGenerator.StopAsync();
        }
    }
}
