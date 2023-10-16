using Application.Plays.PlayGame;
using AutoMapper;
using Carter;
using MediatR;
using Web.API.Contracts;

namespace Web.API.Endpoints;

public class PlaysModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("play", async (PlayGameRequest request, ISender sender, IMapper mapper) =>
        {
            var command = mapper.Map<PlayGameCommand>(request);
            var response = await sender.Send(command);
            var result = mapper.Map<PlayGameResult>(response);
            return Results.Ok(result);
        });
    }
}