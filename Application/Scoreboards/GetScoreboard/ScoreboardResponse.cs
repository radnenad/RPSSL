namespace Application.Scoreboards.GetScoreboard;

public record ScoreboardResponse(IEnumerable<ScoreboardResult> Results)
{
    public IEnumerable<ScoreboardResult> Results { get; set; } = Results;
}