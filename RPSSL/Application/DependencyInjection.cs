using Application.Abstractions;
using Application.Behaviors;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IRandomChoiceService, RandomChoiceService>();
        services.AddTransient<IGameResultEvaluator, GameResultEvaluator>();
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);
    }
}