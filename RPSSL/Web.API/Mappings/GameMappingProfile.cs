using Application.Plays.PlayGame;
using AutoMapper;
using Domain.Factories;
using Web.API.Contracts;

namespace Web.API.Mappings;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<PlayGameRequest, PlayGameCommand>()
            .ConstructUsing(src => new PlayGameCommand(ChoiceFactory.FromId(src.PlayerChoiceId)));

        CreateMap<PlayGameCommandResponse, PlayGameResult>()
            .ConstructUsing(src => new PlayGameResult(
                src.Outcome.Name,
                src.PlayerChoice.Id,
                src.ComputerChoice.Id));
    }
}