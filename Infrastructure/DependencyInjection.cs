using Infrastructure.Abstractions;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRandomNumberService, RandomNumberService>();
        services.AddTransient<IRandomNumberFetcher, RandomNumberFetcher>();
        services.AddTransient<IRandomNumberInternalGenerator, RandomNumberInternalGenerator>();

        var baseUrl = configuration.GetSection("RandomNumberApi").GetValue<string>("BaseUrl")
                      ?? throw new InvalidOperationException("Configuration for HTTP client is missing");

        services.AddHttpClient("RandomNumberHttpClient",
            client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });
    }
}