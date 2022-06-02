using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services;

public interface IPriceGenerator
{
    IAsyncEnumerable<CurrencyPair> StartAsync();
    Task StopAsync();
}

