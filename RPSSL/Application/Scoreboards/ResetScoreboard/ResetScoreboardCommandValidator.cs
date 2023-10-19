using Application.Shared;
using FluentValidation;

namespace Application.Scoreboards.ResetScoreboard;

public class ResetScoreboardCommandValidator : AbstractValidator<ResetScoreBoardCommand>
{
    public ResetScoreboardCommandValidator()
    {
        RuleFor(x => x.PlayerId).SetValidator(new PlayerIdValidator());
    }
}