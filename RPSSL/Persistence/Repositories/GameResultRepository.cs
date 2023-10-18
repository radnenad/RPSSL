using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

public class GameResultRepository : IGameResultRepository
{
    private static readonly ConcurrentDictionary<string, ConcurrentQueue<GameResult>> PlayerResults = new();

    public void AddResult(GameResult result)
    {
        var playerQueue = PlayerResults.GetOrAdd(result.PlayerId, new ConcurrentQueue<GameResult>());
        playerQueue.Enqueue(result);
        
        while (playerQueue.Count > 10)
        {
            playerQueue.TryDequeue(out _);
        }
    }

    public IEnumerable<GameResult> GetRecentResultsForPlayer(string playerId)
    {
        return PlayerResults.TryGetValue(playerId, out var playerQueue) 
            ? playerQueue.Reverse().Take(10) 
            : Enumerable.Empty<GameResult>();
    }

    public void ResetPlayerScoreboard(string playerId)
    {
        if (PlayerResults.ContainsKey(playerId))
        {
            PlayerResults.TryRemove(playerId, out _);
        }
    }
}
