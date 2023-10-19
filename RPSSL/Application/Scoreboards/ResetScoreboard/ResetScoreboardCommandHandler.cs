using Domain.Repositories;
using MediatR;

namespace Application.Scoreboards.ResetScoreboard;

public class ResetScoreboardCommandHandler : IRequestHandler<ResetScoreBoardCommand>
{
    private readonly IGameResultRepository _resultRepository;

    public ResetScoreboardCommandHandler(IGameResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public Task Handle(ResetScoreBoardCommand request, CancellationToken cancellationToken)
    {
        _resultRepository.ResetPlayerScoreboard(request.PlayerId);
        return Task.CompletedTask;
    }
}