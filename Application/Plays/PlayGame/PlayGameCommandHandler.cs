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
    private readonly IGameResultEvaluator _gameResulEvaluator;
    private readonly IGameResultRepository _gameResultRepository;
    private readonly ILogger<PlayGameCommandHandler> _logger;

    public PlayGameCommandHandler(IRandomChoiceService randomChoiceService, IGameResultRepository gameResultRepository,
        ILogger<PlayGameCommandHandler> logger, IGameResultEvaluator resultEvaluator)
    {
        _randomChoiceService = randomChoiceService;
        _gameResultRepository = gameResultRepository;
        _logger = logger;
        _gameResulEvaluator = resultEvaluator;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var playerChoice = ChoiceFactory.FromId(request.PlayerChoiceId);
        var computerChoice = await _randomChoiceService.GetRandomChoice();
        
        var outcome = _gameResulEvaluator.Evaluate(playerChoice, computerChoice);
        
        _logger.LogInformation(
            $"Game played. PlayerChoice: {playerChoice.Name}, " +
            $"ComputerChoice: {computerChoice.Name}, Outcome: {outcome.Name}");

        SaveGameResult(request.PlayerId, playerChoice, computerChoice, outcome);

        return new PlayGameCommandResponse(outcome.Name, playerChoice.Id, computerChoice.Id);
    }

    private void SaveGameResult(string playerId, Choice playerChoice, Choice computerChoice, Outcome outcome)
    {
        var gameResult = new GameResult(playerId, playerChoice, computerChoice, outcome);
        _gameResultRepository.AddResult(gameResult);
    }
}