using System.Text.Json;
using Infrastructure.Abstractions;
using Infrastructure.Configurations;
using Polly;
using Polly.Fallback;

namespace Infrastructure.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncFallbackPolicy<int> _fallbackPolicy;

    private static readonly Random RandomNumberGenerator = new();

    public RandomNumberService(IHttpClientFactory httpClientFactory, RandomNumberApiConfig randomNumberApiConfig)
    {
        _httpClient = httpClientFactory.CreateClient("RandomNumberHttpClient");
        _httpClient.BaseAddress = new Uri(
            randomNumberApiConfig.BaseUrl ??
            throw new InvalidOperationException("BaseUrl must be configured."));

        _fallbackPolicy = Policy<int>
            .Handle<HttpRequestException>()
            .FallbackAsync(GenerateInternalRandomNumberAsync);
    }

    public async Task<int> GetRandomNumber()
    {
        return await _fallbackPolicy.ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync("/random");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return ExtractNumberFromResponse(content);
        });
    }

    private static Task<int> GenerateInternalRandomNumberAsync(CancellationToken cancellationToken)
    {
        var randomValue = RandomNumberGenerator.Next(1, 101);
        return Task.FromResult(randomValue);
    }

    private static int ExtractNumberFromResponse(string response)
    {
        var jsonDoc = JsonDocument.Parse(response);
        return jsonDoc.RootElement.GetProperty("random_number").GetInt32();
    }
}