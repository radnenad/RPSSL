using Application.Validators;
using FluentValidation;

namespace Application.Scoreboards.GetScoreboard;

public class GetScoreboardQueryValidator : AbstractValidator<GetScoreboardQuery>
{
    public GetScoreboardQueryValidator()
    {
        RuleFor(x => x.PlayerId).SetValidator(new PlayerIdValidator());
    }
}