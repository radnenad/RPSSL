using Domain.Entities;
using MediatR;

namespace Application.Plays.PlayGame;

public record PlayGameCommand(Choice PlayerChoice) : IRequest<PlayGameCommandResponse>;