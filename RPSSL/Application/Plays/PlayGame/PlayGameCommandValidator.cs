using FluentValidation;

namespace Application.Plays.PlayGame;

public class PlayGameCommandValidator : AbstractValidator<PlayGameCommand>
{
    public PlayGameCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotNull().NotEmpty()
            .WithMessage("Player cannot be identified");
        
        RuleFor(x => x.PlayerChoiceId).NotNull()
            .WithMessage("Player choice must be provided");
        
        RuleFor(x => x.PlayerChoiceId).InclusiveBetween(1, 5)
            .WithMessage("Invalid player choice selected");

    }
}