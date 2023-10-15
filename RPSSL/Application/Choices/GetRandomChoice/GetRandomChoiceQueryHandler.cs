using Application.Choices.Common;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.Choices.GetRandomChoice;

public class GetRandomChoiceQueryHandler : IRequestHandler<GetRandomChoiceQuery, ChoiceResponse>
{
    private readonly IRandomNumberService _randomNumberService;
    private readonly IMapper _mapper;

    public GetRandomChoiceQueryHandler(IRandomNumberService randomNumberService, IMapper mapper)
    {
        _randomNumberService = randomNumberService;
        _mapper = mapper;
    }

    public async Task<ChoiceResponse> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        var randomNumber = await _randomNumberService.GetRandomNumber();
        var choice = _mapper.Map<Choice>(randomNumber);
        var response = _mapper.Map<ChoiceResponse>(choice);

        return response;
    }
}