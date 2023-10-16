using Application.Choices.Common;
using AutoMapper;
using Domain.Entities;
using Domain.Factories;

namespace Application.Mappings;

public class ChoiceMappingProfile : Profile
{
    public ChoiceMappingProfile()
    {
        CreateMap<int, Choice>()
            .ConvertUsing(randomNumber => MapRandomNumberToChoice(randomNumber));

        CreateMap<Choice, ChoiceResponse>().ReverseMap();
    }

    private static Choice MapRandomNumberToChoice(int randomNumber)
    {
        if (randomNumber is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber));

        return randomNumber switch
        {
            <= 20 => ChoiceFactory.Rock,
            <= 40 => ChoiceFactory.Paper,
            <= 60 => ChoiceFactory.Scissors,
            <= 80 => ChoiceFactory.Lizard,
            _ => ChoiceFactory.Spock
        };
    }

}