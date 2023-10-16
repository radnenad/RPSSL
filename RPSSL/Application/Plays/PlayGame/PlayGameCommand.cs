using Domain.Entities;
using MediatR;

namespace Application.Plays.PlayGame;

public record PlayGameCommand(GameChoice PlayerGameChoice) : IRequest<PlayGameCommandResponse>;