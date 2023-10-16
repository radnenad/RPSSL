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
        services.AddTransient<IRandomNumberInternalGenerator, RandomNumberInternalGenerator>();
        services.AddTransient<IRandomNumberParser, RandomNumberParser>();

        var randomNumberApiConfig = new RandomNumberApiConfig();
        configuration.GetSection("RandomNumberApi").Bind(randomNumberApiConfig);

        services.AddHttpClient("RandomNumberHttpClient",
            client =>
            {
                client.BaseAddress = new Uri(randomNumberApiConfig.BaseUrl ?? throw new InvalidOperationException());
            });

        return services;
    }
}