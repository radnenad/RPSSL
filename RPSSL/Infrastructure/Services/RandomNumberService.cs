using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private AsyncCircuitBreakerPolicy<int>? _circuitBreakerPolicy;
    private AsyncFallbackPolicy<int>? _fallbackPolicy;

    public RandomNumberService(IRandomNumberFetcher fetcher, IRandomNumberInternalGenerator internalGenerator,
        ILogger<RandomNumberService> logger)
    {
        _fetcher = fetcher;
        SetupResilienceStrategy(internalGenerator, logger);
    }

    public async Task<int> GetRandomNumber()
    {
        return await _fallbackPolicy!.WrapAsync(_circuitBreakerPolicy)
            .ExecuteAsync(() => _fetcher.FetchRandomNumberAsync());
    }

    private void SetupResilienceStrategy(IRandomNumberInternalGenerator internalGenerator, ILogger logger)
    {
        _circuitBreakerPolicy = Policy<int>.Handle<Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1),
                (ex, _) => { logger.LogWarning($"Circuit broken due to: {ex.Exception.Message}."); },
                () => { logger.LogInformation("Circuit reset."); });

        _fallbackPolicy = Policy<int>.Handle<Exception>()
            .FallbackAsync(_ =>
            {
                logger.LogWarning("Falling back to internal random number generator due to external service failure.");
                return Task.FromResult(internalGenerator.Generate());
            });
    }
}