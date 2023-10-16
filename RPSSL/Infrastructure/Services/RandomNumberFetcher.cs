using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberFetcher : IRandomNumberFetcher
{
    private readonly HttpClient _httpClient;
    
    public RandomNumberFetcher(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("RandomNumberHttpClient");
    }

    public async Task<string> FetchRandomNumberAsync()
    {
        var response = await _httpClient.GetAsync("/random");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}