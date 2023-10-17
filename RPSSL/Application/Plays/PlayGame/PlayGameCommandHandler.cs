using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Plays.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, PlayGameCommandResponse>
{
    private readonly IRandomChoiceService _randomChoiceService;

    public PlayGameCommandHandler(IRandomChoiceService randomChoiceService)
    {
        _randomChoiceService = randomChoiceService;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var computerChoice = await _randomChoiceService.GetRandomChoice();
        var outcome  = EvaluateGameResult(request.PlayerChoice, computerChoice);
        return new PlayGameCommandResponse(request.PlayerChoice, computerChoice, outcome);
    }

    private static Outcome EvaluateGameResult(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;
        return playerChoice.Beats.Contains(computerChoice) ? Outcome.Win : Outcome.Lose;
    }

}