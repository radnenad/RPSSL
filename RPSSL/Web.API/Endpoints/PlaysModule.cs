using Application.Plays.PlayGame;
using AutoMapper;
using Carter;
using Domain.Entities;
using Domain.Factories;
using MediatR;
using Web.API.Contracts;

namespace Web.API.Endpoints;

public class PlaysModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("play", async (PlayGameRequest request, ISender sender, IMapper mapper) =>
        {
            var command = new PlayGameCommand(GameChoiceFactory.FromId(request.PlayerChoice));
            var response = await sender.Send(command);
            
            var result = new PlayGameResult(
                response.Outcome.Name, 
                response.PlayerGameChoice.Id,
                response.ComputerGameChoice.Id);
            
            return Results.Ok(result);
        });
    }
}