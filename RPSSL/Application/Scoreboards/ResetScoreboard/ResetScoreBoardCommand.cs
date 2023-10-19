using Domain.Shared;
using MediatR;

namespace Application.Scoreboards.ResetScoreboard;

public record ResetScoreBoardCommand(string PlayerId) : IRequest<Result>;