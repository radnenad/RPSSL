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
        var computerChoice = ChoiceFactory.FromId(computerChoiceResponse.Id);
        
        var outcome  = DetermineOutcome(request.PlayerChoice, computerChoice);
        return new PlayGameCommandResponse(request.PlayerChoice, computerChoice, outcome);
    }
    
    private static Outcome DetermineOutcome(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;

        return playerChoice.Beats.Contains(computerChoice) ? Outcome.Win : Outcome.Lose;
    }

}