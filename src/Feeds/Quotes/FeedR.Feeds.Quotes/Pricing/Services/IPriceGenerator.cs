using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal interface IPriceGenerator
{
    IAsyncEnumerable<CurrencyPair> StartAsync();
    Task StopAsync();
}

