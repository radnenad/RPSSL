using Application.Choices.Common;
using Domain.Factories;
using MediatR;

namespace Application.Choices.GetAllChoices;

internal sealed class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public Task<IEnumerable<ChoiceResponse>> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
    {
        var allChoices = ChoiceFactory.GetAll();
        var choiceResponseList = allChoices.Select(choice => new ChoiceResponse(choice.Id, choice.Name));
        
        return Task.FromResult(choiceResponseList);
    }
}