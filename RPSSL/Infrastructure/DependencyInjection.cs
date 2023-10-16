using Infrastructure.Abstractions;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRandomNumberService, RandomNumberService>();
        services.AddTransient<IRandomNumberFetcher, RandomNumberFetcher>();
        services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddTransient<IRandomNumberParser, RandomNumberParses>();

        var randomNumberApiConfig = new RandomNumberApiConfig();
        configuration.GetSection("RandomNumberApi").Bind(randomNumberApiConfig);
        services.AddSingleton(randomNumberApiConfig);

        services.AddHttpClient("RandomNumberHttpClient",
            client =>
            {
                client.BaseAddress = new Uri(randomNumberApiConfig.BaseUrl ?? throw new InvalidOperationException());
            });

        return services;
    }
}