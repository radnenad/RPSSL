using Application.Choices.Common;
using Domain.Entities;
using MediatR;

namespace Application.Choices.GetAllChoices;

internal sealed class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public Task<IEnumerable<ChoiceResponse>> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
    {
        var choices = Enum.GetValues(typeof(Choice))
            .Cast<Choice>()
            .Select(choice => new ChoiceResponse(choice, choice.ToString().ToLower()))
            .ToList();

        return Task.FromResult<IEnumerable<ChoiceResponse>>(choices);
    }
}