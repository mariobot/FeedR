using FeedR.Aggregator.Services;
using FeedR.Shared.Readis;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<PricingStreamBackgroundService>()
    .AddRedis(builder.Configuration)
    .AddStreaming();

var app = builder.Build();

app.MapGet("/", () => "FeedR Aggregator");

app.Run();