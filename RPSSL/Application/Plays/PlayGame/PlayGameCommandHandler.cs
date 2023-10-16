using Application.Choices.GetRandomChoice;
using Domain.Entities;
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
        var computerChoice = computerChoiceResponse.Id;
        var outcome  = DetermineOutcome(request.PlayerChoice, computerChoice);
        return new PlayGameCommandResponse(request.PlayerChoice, computerChoice, outcome);
    }
    
    private static Outcome DetermineOutcome(Choice userChoice, Choice computerChoice)
    {
        if (userChoice == computerChoice) return Outcome.Tie;

        switch (userChoice)
        {
            case Choice.Rock:
                return computerChoice is Choice.Scissors or Choice.Lizard ? Outcome.Win : Outcome.Lose;
            case Choice.Paper:
                return computerChoice is Choice.Rock or Choice.Spock ? Outcome.Win : Outcome.Lose;
            case Choice.Scissors:
                return computerChoice is Choice.Paper or Choice.Lizard ? Outcome.Win : Outcome.Lose;
            case Choice.Lizard:
                return computerChoice is Choice.Spock or Choice.Paper ? Outcome.Win : Outcome.Lose;
            case Choice.Spock:
                return computerChoice is Choice.Scissors or Choice.Rock ? Outcome.Win : Outcome.Lose;
            default:
                throw new Exception("Invalid choice"); // TODO: Replace with custom exception
        }
    }

}