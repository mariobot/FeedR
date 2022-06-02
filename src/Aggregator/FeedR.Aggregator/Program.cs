using FeedR.Aggregator.Services;
using FeedR.Shared.Readis;
using FeedR.Shared.Readis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHostedService<PricingStreamBackgroundService>()
    .AddSerialization()
    .AddStreaming()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming();

var app = builder.Build();

app.MapGet("/", () => "FeedR Aggregator");

app.Run();