using Application.Choices.Common;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class ChoiceProfile : Profile
{
    public ChoiceProfile()
    {
        CreateMap<int, Choice>()
            .ConvertUsing(randomNumber => MapRandomNumberToChoice(randomNumber));

        CreateMap<Choice, ChoiceResponse>().ReverseMap();
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