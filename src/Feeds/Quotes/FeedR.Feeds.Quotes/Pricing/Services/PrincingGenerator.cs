using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public class PrincingGenerator : IPriceGenerator
    {
        private readonly Random _random = new Random();
        private readonly Dictionary<string, decimal> _currencyPairs = new()
        {
            ["EURUSD"] = 1.13M,
            ["EURGBP"] = 0.85M,
            ["EURCHF"] = 1.04M,
            ["EURPLN"] = 4.62M
        };
        private readonly ILogger<PrincingGenerator> logger;
        private bool _isRunning;

        public PrincingGenerator(ILogger<PrincingGenerator> logger)
        {
            this.logger = logger;
        }

        public async Task StartAsync()
        {
            _isRunning = true;
            while (_isRunning)
            {
                foreach (var (symbol, pricing) in _currencyPairs)
                {
                    if (!_isRunning)
                    {
                        return;
                    }

                    var tick = NextTick();
                    var newPricing = pricing + tick;
                    _currencyPairs[symbol] = newPricing;

                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                    logger.LogInformation($"Updated pricing for: {symbol}, {pricing:F} --> {newPricing:F} [{tick:F}]");
                    var currencyPair = new CurrencyPair(symbol, newPricing, timestamp);
                    await Task.Delay(TimeSpan.FromSeconds(1));

                }
            }
        }

        public Task StopAsync()
        {
            _isRunning = false;
            return Task.CompletedTask;
        }

        private decimal NextTick() 
        { 
            var sign = _random.Next(0,2) == 0 ? -1 : 1;
            var tick = _random.NextDouble() / 20;
            return (decimal)(sign * tick);
        }
    }
}
