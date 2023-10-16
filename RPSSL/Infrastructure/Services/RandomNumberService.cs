using Infrastructure.Abstractions;
using Polly;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private readonly IRandomNumberParser _parser;
    private readonly AsyncFallbackPolicy<int> _fallbackPolicy;

    public RandomNumberService(
        IRandomNumberFetcher fetcher, 
        IRandomNumberInternalGenerator internalGenerator, 
        IRandomNumberParser parser)
    {
        _fetcher = fetcher;
        _parser = parser;
        
        _fallbackPolicy = Policy<int>
            .Handle<Exception>()
            .FallbackAsync(_ =>Task.FromResult(internalGenerator.Generate()));
    }

    public async Task<int> GetRandomNumber()
    {
        return await _fallbackPolicy.ExecuteAsync(async () =>
        {
            var response = await _fetcher.FetchRandomNumberAsync();
            return _parser.Parse(response);
        });
    }
}