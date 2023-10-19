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
        _resultRepository.ResetPlayerScoreboard(request.PlayerId);
        return Task.FromResult(Result.Success());
    }
}