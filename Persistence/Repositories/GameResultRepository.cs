using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories;

public class GameResultRepository : IGameResultRepository
{
    private readonly ILogger<GameResultRepository> _logger;
    private static readonly ConcurrentDictionary<string, ConcurrentQueue<GameResult>> PlayerResults = new();
    private const int MaxRecentResults = 10;
    
    public GameResultRepository(ILogger<GameResultRepository> logger)
    {
        _logger = logger;
    }

    public void AddResult(GameResult result)
    {
        var playerQueue = PlayerResults.GetOrAdd(result.PlayerId, new ConcurrentQueue<GameResult>());
        playerQueue.Enqueue(result);

        _logger.LogInformation($"Added game result for player {result.PlayerId}. Outcome: {result.Outcome.Name}");

        while (playerQueue.Count > MaxRecentResults)
        {
            playerQueue.TryDequeue(out _);
        }
    }

    public IEnumerable<GameResult> GetRecentResultsForPlayer(string playerId)
    {
        return PlayerResults.TryGetValue(playerId, out var playerQueue)
            ? playerQueue.Reverse().Take(MaxRecentResults)
            : Enumerable.Empty<GameResult>();
    }

    public bool ResetPlayerScoreboard(string playerId)
    {
        return PlayerResults.ContainsKey(playerId) 
               && PlayerResults.TryRemove(playerId, out _);
    }
}