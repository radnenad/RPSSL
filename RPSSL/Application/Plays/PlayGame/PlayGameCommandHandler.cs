using Application.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Plays.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, PlayGameCommandResponse>
{
    private readonly IRandomChoiceService _randomChoiceService;
    private readonly IGameResultRepository _gameResultRepository;

    public PlayGameCommandHandler(IRandomChoiceService randomChoiceService, IGameResultRepository gameResultRepository)
    {
        _randomChoiceService = randomChoiceService;
        _gameResultRepository = gameResultRepository;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var computerChoice = await _randomChoiceService.GetRandomChoice();
        var outcome = EvaluateGameResult(request.PlayerChoice, computerChoice);

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
        var gameResult =
            new GameResult(request.PlayerId, request.PlayerChoice, computerChoice, outcome);
        _gameResultRepository.AddResult(gameResult);
    }
}