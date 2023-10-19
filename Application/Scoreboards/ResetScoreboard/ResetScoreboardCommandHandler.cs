using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Scoreboards.ResetScoreboard;

public class ResetScoreboardCommandHandler : IRequestHandler<ResetScoreBoardCommand, Result>
{
    private readonly IGameResultRepository _resultRepository;

    public ResetScoreboardCommandHandler(IGameResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public Task<Result> Handle(ResetScoreBoardCommand request, CancellationToken cancellationToken)
    {
        return _resultRepository.ResetPlayerScoreboard(request.PlayerId)
            ? Task.FromResult(Result.Success())
            : Task.FromResult(Result.Failure("Player data not found. Nothing to reset."));
    }
}