using Application.Choices.Common;
using AutoMapper;
using Domain.Entities;
using Domain.Factories;

namespace Application.Mappings;

public class ChoiceMappingProfile : Profile
{
    public ChoiceMappingProfile()
    {
        CreateMap<int, GameChoice>()
            .ConvertUsing(randomNumber => MapRandomNumberToChoice(randomNumber));

        CreateMap<GameChoice, ChoiceResponse>().ReverseMap();
    }

    private static GameChoice MapRandomNumberToChoice(int randomNumber)
    {
        if (randomNumber is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber));

        return randomNumber switch
        {
            <= 20 => GameChoiceFactory.Rock,
            <= 40 => GameChoiceFactory.Paper,
            <= 60 => GameChoiceFactory.Scissors,
            <= 80 => GameChoiceFactory.Lizard,
            _ => GameChoiceFactory.Spock
        };
    }

}