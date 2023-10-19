using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private AsyncCircuitBreakerPolicy<RandomNumber>? _circuitBreakerPolicy;
    private AsyncFallbackPolicy<RandomNumber>? _fallbackPolicy;

    public RandomNumberService(IRandomNumberFetcher fetcher, IRandomNumberInternalGenerator internalGenerator,
        ILogger<RandomNumberService> logger)
    {
        _fetcher = fetcher;
        SetupResilienceStrategy(internalGenerator, logger);
    }

    public async Task<RandomNumber> GetRandomNumber()
    {
        return await _fallbackPolicy!.WrapAsync(_circuitBreakerPolicy)
            .ExecuteAsync(() => _fetcher.FetchRandomNumberAsync());
    }

    private void SetupResilienceStrategy(IRandomNumberInternalGenerator internalGenerator, ILogger logger)
    {
        const int handleEventsAllowedBeforeBreaking = 3;
        const int durationOfBreakMinutes = 1;

        _circuitBreakerPolicy = Policy<RandomNumber>
            .Handle<Exception>()
            .CircuitBreakerAsync(
                handleEventsAllowedBeforeBreaking, 
                TimeSpan.FromMinutes(durationOfBreakMinutes),
                (ex, _) => { logger.LogWarning($"Circuit broken due to: {ex.Exception.Message}."); },
                () => { logger.LogInformation("Circuit reset."); });

        _fallbackPolicy = Policy<RandomNumber>
            .Handle<Exception>()
            .FallbackAsync(_ =>
            {
                logger.LogWarning("Falling back to internal random number generator due to external service failure.");
                return Task.FromResult(internalGenerator.Generate());
            });
    }
}