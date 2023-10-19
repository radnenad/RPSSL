using System.Text.Json;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberFetcher : IRandomNumberFetcher
{
    private readonly HttpClient _httpClient;
    
    public RandomNumberFetcher(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("RandomNumberHttpClient");
    }

    public async Task<RandomNumber> FetchRandomNumberAsync()
    {
        var response = await _httpClient.GetAsync("/random");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var number = ParseRandomNumber(content);
        return new RandomNumber(number);
    }
    
    private static int ParseRandomNumber(string content)
    {
        var jsonDoc = JsonDocument.Parse(content);
        return jsonDoc.RootElement.GetProperty("random_number").GetInt32();
    }
}