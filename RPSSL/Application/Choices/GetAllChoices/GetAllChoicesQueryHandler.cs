using MediatR;

namespace Application.Choices.GetAllChoices;

public class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, IEnumerable<Choice>>
{
    public Task<IEnumerable<Choice>> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
    {
        var choices = Enum.GetValues(typeof(ChoiceEnum))
            .Cast<ChoiceEnum>()
            .Select(choice => new Choice((int)choice, choice.ToString().ToLower()))
            .ToList();

        return Task.FromResult<IEnumerable<Choice>>(choices);
    }
}