using Application.Abstractions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Abstractions;

namespace Application.Services;

public class RandomChoiceService : IRandomChoiceService
{
    private readonly IRandomNumberService _randomNumberService;
    private readonly IMapper _mapper;

    public RandomChoiceService(IRandomNumberService randomNumberService, IMapper mapper)
    {
        _randomNumberService = randomNumberService;
        _mapper = mapper;
    }

    public async Task<Choice> GetRandomChoice()
    {
        var randomNumber = await _randomNumberService.GetRandomNumber();
        return _mapper.Map<Choice>(randomNumber);
    }
}