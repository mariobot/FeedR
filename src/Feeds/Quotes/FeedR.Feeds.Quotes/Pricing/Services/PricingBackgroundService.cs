using FeedR.Feeds.Quotes.Pricing.Requests;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public class PricingBackgroundService : BackgroundService
    {
        private int _runningStatus; // 0 or 1
        private readonly IPriceGenerator _priceGenerator;
        private readonly PricingRequestsChannel _requestsChannel;
        private readonly ILogger<PricingBackgroundService> _logger;

        public PricingBackgroundService(IPriceGenerator priceGenerator, PricingRequestsChannel requestsChannel,
            ILogger<PricingBackgroundService> logger)
        {
            _priceGenerator = priceGenerator;
            _requestsChannel = requestsChannel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Pricing background service has started.");
            await foreach (var request in _requestsChannel.Requests.Reader.ReadAllAsync(stoppingToken))
            {
                _logger.LogInformation($"Pricing background service has recived the request: {request.GetType().Name}.");

                var _ = request switch
                {
                    StartPricing => StartGeneratorAsync(),
                    StopPricing => StopGeneratorAsync(),
                    _ => Task.CompletedTask
                };
            }
            _logger.LogInformation("Pricing background service has stopped.");
        }

        private async Task StartGeneratorAsync()
        {
            if (Interlocked.Exchange(ref _runningStatus, 1) == 1)
            {
                _logger.LogInformation("Pricing generator is already running.");
                return;
            }

            await _priceGenerator.StartAsync();
        
        }

        private async Task StopGeneratorAsync()
        {
            if (Interlocked.Exchange(ref _runningStatus, 0) == 0)
            {
                _logger.LogInformation("Pricing generator is not running.");
                return;
            }

            await _priceGenerator.StopAsync();

        }
    }
}
