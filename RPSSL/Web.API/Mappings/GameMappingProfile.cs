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
            .ConstructUsing(src => new PlayGameCommand(GameChoiceFactory.FromId(src.PlayerChoice)));

        CreateMap<PlayGameCommandResponse, PlayGameResult>()
            .ConstructUsing(src => new PlayGameResult(
                src.Outcome.Name,
                src.PlayerGameChoice.Id,
                src.ComputerGameChoice.Id));
    }
}