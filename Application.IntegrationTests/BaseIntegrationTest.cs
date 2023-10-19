using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly ISender Sender;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();

        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}