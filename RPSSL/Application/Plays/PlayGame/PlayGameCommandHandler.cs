using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.Plays.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, PlayGameCommandResponse>
{
    private readonly IRandomNumberService _randomNumberService;
    private readonly IMapper _mapper;

    public PlayGameCommandHandler(IRandomNumberService randomNumberService, IMapper mapper)
    {
        _randomNumberService = randomNumberService;
        _mapper = mapper;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var computerChoice = await GetComputerChoice();
        var outcome  = EvaluateGameResult(request.PlayerChoice, computerChoice);
        return new PlayGameCommandResponse(request.PlayerChoice, computerChoice, outcome);
    }

    private async Task<Choice> GetComputerChoice()
    {
        var randomNumber = await _randomNumberService.GetRandomNumber();
        return _mapper.Map<Choice>(randomNumber);
    }

    private static Outcome EvaluateGameResult(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;
        return playerChoice.Beats.Contains(computerChoice) ? Outcome.Win : Outcome.Lose;
    }

}