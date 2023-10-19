using MediatR;

namespace Application.Plays.PlayGame;

public record PlayGameCommand(string PlayerId, int PlayerChoiceId) : IRequest<PlayGameCommandResponse>;