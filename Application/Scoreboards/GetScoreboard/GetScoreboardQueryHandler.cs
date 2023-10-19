using Application.Mappings;
using Domain.Repositories;
using MediatR;

namespace Application.Scoreboards.GetScoreboard;

public class GetScoreboardQueryHandler : IRequestHandler<GetScoreboardQuery, ScoreboardResponse>
{
    private readonly IGameResultRepository _gameResultRepository;

    public GetScoreboardQueryHandler(IGameResultRepository gameResultRepository)
    {
        _gameResultRepository = gameResultRepository;
    }

    public Task<ScoreboardResponse> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
    {
        var gameResults = _gameResultRepository.GetRecentResultsForPlayer(request.PlayerId);

        var scoreboardResults = gameResults.Select(s =>
                new ScoreboardResult(FlavorTextMapper.GetFlavorText(s.PlayerChoice, s.ComputerChoice, s.Outcome),
                    s.PlayerChoice.Name, s.ComputerChoice.Name, s.Outcome.Name)).ToList();

        var scoreBoardResponse = new ScoreboardResponse(scoreboardResults);

        return Task.FromResult(scoreBoardResponse);
    }
}