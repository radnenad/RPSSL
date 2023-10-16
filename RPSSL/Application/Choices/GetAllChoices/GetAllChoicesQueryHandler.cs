using Application.Choices.Common;
using Domain.Factories;
using MediatR;

namespace Application.Choices.GetAllChoices;

internal sealed class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public Task<IEnumerable<ChoiceResponse>> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
    {
        var allChoices = GameChoiceFactory.GetAll();
        var choiceResponseList = allChoices.Select(c => new ChoiceResponse(c.Id, c.Name));
        
        return Task.FromResult(choiceResponseList);
    }
}