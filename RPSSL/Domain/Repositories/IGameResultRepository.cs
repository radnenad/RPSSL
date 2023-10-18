using Domain.Entities;

namespace Domain.Repositories;

public interface IGameResultRepository
{
    void AddResult(GameResult result);
    IEnumerable<GameResult> GetRecentResultsForPlayer(string playerId);
    public void ResetPlayerScoreboard(string playerId);
}