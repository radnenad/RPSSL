using MediatR;

namespace Application.Choices.GetAllChoices;

public record GetAllChoicesQuery : IRequest<IEnumerable<Choice>>;

public sealed record Choice(int Id, string Name);

public enum ChoiceEnum
{
    Rock = 1,
    Paper = 2,
    Scissors = 3,
    Lizard = 4,
    Spock = 5
}
