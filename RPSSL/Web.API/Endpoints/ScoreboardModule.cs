using Application.Scoreboards.GetScoreboard;
using Application.Scoreboards.ResetScoreboard;
using Carter;
using MediatR;
using Web.API.Extensions;

namespace Web.API.Endpoints;

public class ScoreboardModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("scoreboard", async (ISender sender, HttpContext context) =>
        {
            var playerId = context.GetUserId();
            var query = new GetScoreboardQuery(playerId);
            var response = await sender.Send(query);
            return Results.Ok(response);
        });

        app.MapPut("scoreboard/reset", async (ISender sender, HttpContext context) =>
        {
            var playerId = context.GetUserId();
            var command = new ResetScoreBoardCommand(playerId);
            var response = await sender.Send(command);
            
            return response.IsSuccess 
                ? Results.NoContent() 
                : Results.BadRequest(response.ErrorMessage);
        });
    }
}