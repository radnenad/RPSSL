using Carter;

namespace Web.API.Endpoints;

public class ChoicesModule: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("choices", () => Results.Ok("Hello World!"));
    }
}