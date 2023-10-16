using Domain.Entities;
using MediatR;

namespace Application.Plays.PlayGame;

public record class PlayGameCommand(Choice PlayerChoice) : IRequest<PlayGameCommandResponse>;