using Application.Choices.Common;
using AutoMapper;
using Domain.Factories;
using MediatR;

namespace Application.Choices.GetAllChoices;

internal sealed class GetAllChoicesQueryHandler : IRequestHandler<GetAllChoicesQuery, IEnumerable<ChoiceResponse>>
{
    private readonly IMapper _mapper;

    public GetAllChoicesQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<IEnumerable<ChoiceResponse>> Handle(GetAllChoicesQuery request, CancellationToken cancellationToken)
    {
        var allChoices = ChoiceFactory.GetAll();
        var choiceResponseList = _mapper.Map<IEnumerable<ChoiceResponse>>(allChoices);
        
        return Task.FromResult(choiceResponseList);
    }
}