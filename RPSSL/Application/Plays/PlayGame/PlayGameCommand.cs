using Domain.Entities;
using MediatR;

namespace Application.Plays.PlayGame;

public record PlayGameCommand(string PlayerId, Choice PlayerChoice) : IRequest<PlayGameCommandResponse>;