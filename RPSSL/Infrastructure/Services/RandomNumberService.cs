using System.Text.Json;
using Infrastructure.Abstractions;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private readonly AsyncCircuitBreakerPolicy<int> _circuitBreakerPolicy;
    private readonly AsyncFallbackPolicy<int> _fallbackPolicy;

    public RandomNumberService(IRandomNumberFetcher fetcher, IRandomNumberInternalGenerator internalGenerator)
    {
        _fetcher = fetcher;

        // Using a Circuit Breaker policy with Polly to enhance the resilience of our service.
        // This policy breaks the circuit after 3 consecutive exceptions and remains broken for 1 minute.
        // This helps in preventing constant hits to a potentially faulty external service and gives it time to recover.
        // While the circuit is broken, the service will handle requests using the fallback mechanism.
        _circuitBreakerPolicy = Policy<int>
            .Handle<Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));
        
        _fallbackPolicy = Policy<int>
            .Handle<Exception>()
            .FallbackAsync(_ => Task.FromResult(internalGenerator.Generate()));
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