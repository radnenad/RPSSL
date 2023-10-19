using Application.Abstractions;
using Domain.Entities;
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
        var computerChoice = await _randomChoiceService.GetRandomChoice();
        
        var outcome = EvaluateGameResult(request.PlayerChoice, computerChoice);
        
        _logger.LogInformation(
            $"Game played. PlayerChoice: {request.PlayerChoice.Name}, " +
            $"ComputerChoice: {computerChoice.Name}, Outcome: {outcome.Name}");

        SaveGameResult(request, computerChoice, outcome);
        
        return new PlayGameCommandResponse(request.PlayerChoice, computerChoice, outcome);
    }

    private static Outcome EvaluateGameResult(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;
        return playerChoice.Beats.Contains(computerChoice) ? Outcome.Win : Outcome.Lose;
    }

    private void SaveGameResult(PlayGameCommand request, Choice computerChoice, Outcome outcome)
    {
        var gameResult = new GameResult(request.PlayerId, request.PlayerChoice, computerChoice, outcome);
        _gameResultRepository.AddResult(gameResult);
    }
}