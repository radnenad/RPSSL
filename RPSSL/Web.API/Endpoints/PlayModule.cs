using Application.Plays.PlayGame;
using Carter;
using Domain.Factories;
using MediatR;
using Web.API.Contracts;

namespace Web.API.Endpoints;

public class PlayModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("play", async (PlayGameRequest request, ISender sender, HttpContext context) =>
        {
            var choice = ChoiceFactory.FromId(request.PlayerChoiceId);

            var playerId = context.Items["UserIdentifier"] as string ?? string.Empty;
            
            //TODO validate

            var command = new PlayGameCommand(playerId, choice);
            var response = await sender.Send(command);
            var result = new GameResultDto(response.Outcome.Name, response.PlayerChoice.Id, response.ComputerChoice.Id);
            return Results.Ok(result);
        });
    }
}