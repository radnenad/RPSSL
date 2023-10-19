using FluentValidation;

namespace Application.Validators;

public class PlayerIdValidator : AbstractValidator<string>
{
    public PlayerIdValidator()
    {
        RuleFor(x => x).NotNull().NotEmpty()
            .WithMessage("Player cannot be identified");
    }
}