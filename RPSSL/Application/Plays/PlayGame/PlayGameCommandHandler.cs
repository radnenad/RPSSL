using Application.Abstractions;
using Domain.Entities;
using Domain.Factories;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Plays.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, PlayGameCommandResponse>
{
    private readonly IRandomChoiceService _randomChoiceService;
    private readonly IGameResultRepository _gameResultRepository;
    private readonly ILogger<PlayGameCommandHandler> _logger;

    public PlayGameCommandHandler(IRandomChoiceService randomChoiceService, IGameResultRepository gameResultRepository,
        ILogger<PlayGameCommandHandler> logger)
    {
        _randomChoiceService = randomChoiceService;
        _gameResultRepository = gameResultRepository;
        _logger = logger;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var playerChoice = ChoiceFactory.FromId(request.PlayerChoiceId);
        var computerChoice = await _randomChoiceService.GetRandomChoice();
        
        var outcome = EvaluateGameResult(playerChoice, computerChoice);
        
        _logger.LogInformation(
            $"Game played. PlayerChoice: {playerChoice.Name}, " +
            $"ComputerChoice: {computerChoice.Name}, Outcome: {outcome.Name}");

        SaveGameResult(request.PlayerId, playerChoice, computerChoice, outcome);

        return new PlayGameCommandResponse(outcome.Name, playerChoice.Id, computerChoice.Id);
    }

    private static Outcome EvaluateGameResult(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;
        return playerChoice.Beats.Contains(computerChoice) ? Outcome.Win : Outcome.Lose;
    }

    private void SaveGameResult(string playerId, Choice playerChoice, Choice computerChoice, Outcome outcome)
    {
        var gameResult = new GameResult(playerId, playerChoice, computerChoice, outcome);
        _gameResultRepository.AddResult(gameResult);
    }
}