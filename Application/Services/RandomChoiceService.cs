using Application.Abstractions;
using Domain.Entities;
using Domain.Factories;
using Infrastructure.Abstractions;

namespace Application.Services;

public class RandomChoiceService : IRandomChoiceService
{
    private readonly IRandomNumberService _randomNumberService;

    public RandomChoiceService(IRandomNumberService randomNumberService)
    {
        _randomNumberService = randomNumberService;
    }

    public async Task<Choice> GetRandomChoice()
    {
        var randomNumber = await _randomNumberService.GetRandomNumber();
        return ChoiceFactory.FromRandomNumber(randomNumber.Value);
    }
}