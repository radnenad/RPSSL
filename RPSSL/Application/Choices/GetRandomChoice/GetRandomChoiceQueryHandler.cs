using Application.Abstractions;
using Application.Choices.Common;
using AutoMapper;
using MediatR;

namespace Application.Choices.GetRandomChoice;

public class GetRandomChoiceQueryHandler : IRequestHandler<GetRandomChoiceQuery, ChoiceResponse>
{
    private readonly IRandomChoiceService _randomChoiceService;
    private readonly IMapper _mapper;

    public GetRandomChoiceQueryHandler(IMapper mapper, IRandomChoiceService randomChoiceService)
    {
        _mapper = mapper;
        _randomChoiceService = randomChoiceService;
    }

    public async Task<ChoiceResponse> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        var choice = await _randomChoiceService.GetRandomChoice();
        var response = _mapper.Map<ChoiceResponse>(choice);
        return response;
    }
}