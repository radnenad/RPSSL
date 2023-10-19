using MediatR;

namespace Application.Scoreboards.GetScoreboard;

public record GetScoreboardQuery(string PlayerId) : IRequest<ScoreboardResponse>;
