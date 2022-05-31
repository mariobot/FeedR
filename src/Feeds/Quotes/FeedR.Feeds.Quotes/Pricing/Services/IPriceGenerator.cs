namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public interface IPriceGenerator
    {
        Task StartAsync();
        Task StopAsync();
    }
}
