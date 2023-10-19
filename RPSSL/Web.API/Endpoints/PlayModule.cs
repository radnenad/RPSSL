using Application.Plays.PlayGame;
using Carter;
using Domain.Factories;
using MediatR;
using Web.API.Contracts;
using Web.API.Extensions;

namespace Web.API.Endpoints;

public class PlayModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("play", async (PlayGameRequest request, ISender sender, HttpContext context) =>
        {
            var playerId = context.GetUserId();
            if (string.IsNullOrWhiteSpace(playerId)) // TODO  do with input validation or in middleware
            {
                return Results.BadRequest("Player cannot be identified"); 
            }
            
            var choice = ChoiceFactory.FromId(request.PlayerChoiceId);

            var command = new PlayGameCommand(playerId, choice);
            var response = await sender.Send(command);
            return Results.Ok(response);
        });
    }
}