using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories;

public class GameResultRepository : IGameResultRepository
{
    private readonly ILogger<GameResultRepository> _logger;

    private static readonly ConcurrentDictionary<string, ConcurrentQueue<GameResult>> PlayerResults = new();

    public GameResultRepository(ILogger<GameResultRepository> logger)
    {
        _logger = logger;
    }

    public void AddResult(GameResult result)
    {
        var playerQueue = PlayerResults.GetOrAdd(result.PlayerId, new ConcurrentQueue<GameResult>());
        playerQueue.Enqueue(result);

        _logger.LogInformation($"Added game result for player {result.PlayerId}. Outcome: {result.Outcome.Name}");

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