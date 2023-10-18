using System.Text.Json;
using Infrastructure.Abstractions;
using Polly;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly IRandomNumberFetcher _fetcher;
    private readonly AsyncFallbackPolicy<int> _fallbackPolicy;

    public RandomNumberService(IRandomNumberFetcher fetcher, IRandomNumberInternalGenerator internalGenerator)
    {
        _fetcher = fetcher;

        _fallbackPolicy = Policy<int>.Handle<Exception>()
            .FallbackAsync(_ => Task.FromResult(internalGenerator.Generate()));
    }

    public async Task<int> GetRandomNumber()
    {
        return await _fallbackPolicy.ExecuteAsync(async () =>
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