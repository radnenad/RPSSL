using Application.Choices.GetAllChoices;
using Application.Choices.GetRandomChoice;
using Carter;
using MediatR;

namespace Web.API.Endpoints;

public class ChoicesModule: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("choices", async (ISender sender) =>
        {
            var query = new GetAllChoicesQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        });
        
        app.MapGet("choice", async (ISender sender) =>
        {
            var query = new GetRandomChoiceQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        });
    }
}