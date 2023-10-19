using Domain.Entities;

namespace Application.Abstractions;

public interface IGameResultEvaluator
{
    Outcome Evaluate(Choice playerChoice, Choice computerChoice);
}