using Application.Scoreboards.GetScoreboard;
using Application.Scoreboards.ResetScoreboard;
using Carter;
using Domain.Entities;
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
            await sender.Send(command);
            return Results.NoContent();
        });
    }
}