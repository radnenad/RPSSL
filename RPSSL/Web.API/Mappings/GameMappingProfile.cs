using Application.Plays.PlayGame;
using AutoMapper;
using Domain.Entities;
using Web.API.Contracts;

namespace Web.API.Mappings;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<PlayGameRequest, PlayGameCommand>()
            .ForMember(dest => dest.PlayerChoice, opts => opts.MapFrom(src => (Choice)src.PlayerChoice));

        CreateMap<PlayGameCommandResponse, PlayGameResult>()
            .ForMember(dest => dest.Results, opts => opts.MapFrom(src => src.Outcome.ToString().ToLower()))
            .ForMember(dest => dest.Player, opts => opts.MapFrom(src => (int)src.PlayerChoice))
            .ForMember(dest => dest.Computer, opts => opts.MapFrom(src => (int)src.ComputerChoice));
    }
}