using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class GameResultEvaluator : IGameResultEvaluator
{
    public Outcome Evaluate(Choice playerChoice, Choice computerChoice)
    {
        if (playerChoice == computerChoice) return Outcome.Tie;
        if (playerChoice.Beats.Contains(computerChoice)) return Outcome.Win;
        if (computerChoice.Beats.Contains(playerChoice)) return Outcome.Lose;
        
        throw new UndefinedGameLogicException(playerChoice, computerChoice);
    }
}