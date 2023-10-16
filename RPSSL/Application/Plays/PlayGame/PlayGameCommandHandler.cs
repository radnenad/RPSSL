using Application.Choices.GetRandomChoice;
using Domain.Entities;
using Domain.Factories;
using MediatR;

namespace Application.Plays.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, PlayGameCommandResponse>
{
    private readonly ISender _sender;

    public PlayGameCommandHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<PlayGameCommandResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var computerChoiceResponse = await _sender.Send(new GetRandomChoiceQuery(), cancellationToken);
        var computerChoice = GameChoiceFactory.FromId(computerChoiceResponse.Id);
        
        var outcome  = DetermineOutcome(request.PlayerGameChoice, computerChoice);
        return new PlayGameCommandResponse(request.PlayerGameChoice, computerChoice, outcome);
    }
    
    private GameOutcome DetermineOutcome(GameChoice playerGameChoice, GameChoice computerGameChoice)
    {
        if (playerGameChoice == computerGameChoice) return GameOutcome.Draw;

        return playerGameChoice.Beats.Contains(computerGameChoice) ? GameOutcome.Win : GameOutcome.Lose;
    }

}