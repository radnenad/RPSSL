using System.Text.Json;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private readonly AsyncCircuitBreakerPolicy<int> _circuitBreakerPolicy;
    private readonly AsyncFallbackPolicy<int> _fallbackPolicy;
    private readonly ILogger<RandomNumberService> _logger;

    public RandomNumberService(IRandomNumberFetcher fetcher, IRandomNumberInternalGenerator internalGenerator,
        ILogger<RandomNumberService> logger)
    {
        _fetcher = fetcher;
        _logger = logger;

        // Using a Circuit Breaker policy with Polly to enhance the resilience of our service.
        // This policy breaks the circuit after 3 consecutive exceptions and remains broken for 1 minute.
        // This helps in preventing constant hits to a potentially faulty external service and gives it time to recover.
        // While the circuit is broken, the service will handle requests using the fallback mechanism.
        _circuitBreakerPolicy = Policy<int>
            .Handle<Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

        _fallbackPolicy = Policy<int>
            .Handle<Exception>()
            .FallbackAsync(_ =>
            {
                _logger.LogWarning(
                    "Falling back to internal random number generator due to external service failure.");
                return Task.FromResult(internalGenerator.Generate());
            });
    }

    public async Task<int> GetRandomNumber()
    {
        return await _fallbackPolicy.WrapAsync(_circuitBreakerPolicy)
            .ExecuteAsync(async () =>
            {
                var response = await _fetcher.FetchRandomNumberAsync();
                return ParseRandomNumber(response);
            });
    }

    private static int ParseRandomNumber(string content)
    {
        var jsonDoc = JsonDocument.Parse(content);
        return jsonDoc.RootElement.GetProperty("random_number").GetInt32();
    }
}