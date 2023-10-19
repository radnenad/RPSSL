using Application.Plays.PlayGame;
using Carter;
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
            var command = new PlayGameCommand(playerId, request.PlayerChoiceId);
            var response = await sender.Send(command);
            return Results.Ok(response);
        });
    }
}