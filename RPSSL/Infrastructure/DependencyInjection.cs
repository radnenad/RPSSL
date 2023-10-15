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
        services.AddHttpClient();

        services.AddScoped<IRandomNumberService, RandomNumberService>();

        var randomNumberApiConfig = new RandomNumberApiConfig();
        configuration.GetSection("RandomNumberApi").Bind(randomNumberApiConfig);
        services.AddSingleton(randomNumberApiConfig);

        return services;
    }
}