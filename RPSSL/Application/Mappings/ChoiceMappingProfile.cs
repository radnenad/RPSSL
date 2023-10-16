using Application.Choices.Common;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class ChoiceMappingProfile : Profile
{
    public ChoiceMappingProfile()
    {
        CreateMap<int, Choice>()
            .ConvertUsing(randomNumber => MapRandomNumberToChoice(randomNumber));

        CreateMap<Choice, ChoiceResponse>()
            .ConstructUsing(src => new ChoiceResponse(src, src.ToString().ToLower()));
    }

    //TODO improve the algorithm
    private static Choice MapRandomNumberToChoice(int randomNumber)
    {
        switch (randomNumber)
        {
            case < 1:
            case > 100:
                throw new ArgumentOutOfRangeException(nameof(randomNumber));
            case <= 20:
                return Choice.Rock;
            case <= 40:
                return Choice.Paper;
            case <= 60:
                return Choice.Scissors;
            case <= 80:
                return Choice.Lizard;
            default:
                return Choice.Spock;
        }
    }
}