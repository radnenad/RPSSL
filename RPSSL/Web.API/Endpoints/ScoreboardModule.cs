using Application.Scoreboards.GetScoreboard;
using Carter;
using Domain.Repositories;
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

        app.MapPut("scoreboard/reset", (IGameResultRepository repository, HttpContext context) =>
        {
            var playerId = context.GetUserId();

            repository.ResetPlayerScoreboard(playerId);

            return Results.Ok();
        });
    }
}