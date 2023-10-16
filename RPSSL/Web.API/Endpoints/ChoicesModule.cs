using Application.Choices.GetAllChoices;
using Application.Choices.GetRandomChoice;
using Carter;
using MediatR;
using Web.API.Contracts;

namespace Web.API.Endpoints;

public class ChoicesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("choices", async (ISender sender) =>
        {
            var query = new GetAllChoicesQuery();
            var response = await sender.Send(query);
            return Results.Ok(response);
        });

        app.MapGet("choice", async (ISender sender) =>
        {
            var query = new GetRandomChoiceQuery();
            var response = await sender.Send(query);
            return Results.Ok(response);
        });
    }
}