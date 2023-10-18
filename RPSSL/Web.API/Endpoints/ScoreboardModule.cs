using Carter;
using Domain.Repositories;
using Web.API.Contracts;
using Web.API.Mapping;

namespace Web.API.Endpoints;

public class ScoreboardModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("scoreboard", (IGameResultRepository repository, HttpContext context) =>
        {
            var playerId = context.Items["UserIdentifier"] as string ?? string.Empty;

            var response = repository.GetRecentResultsForPlayer(playerId);

            var result = response.Select(s => new GameResultDto(
                FlavorTextMapper.GetFlavorText(s.PlayerChoice, s.ComputerChoice, s.Outcome),
                s.PlayerChoice.Id,
                s.ComputerChoice.Id));

            return Results.Ok(result);
        });

        app.MapPut("scoreboard/reset", (IGameResultRepository repository, HttpContext context) =>
        {
            var playerId = context.Items["UserIdentifier"] as string ?? string.Empty;

            repository.ResetPlayerScoreboard(playerId);

            return Results.Ok();
        });
    }
}