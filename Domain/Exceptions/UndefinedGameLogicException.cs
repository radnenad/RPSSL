using Domain.Entities;

namespace Domain.Exceptions;

public sealed class UndefinedGameLogicException : Exception
{
    public UndefinedGameLogicException(Choice playerChoice, Choice computerChoice) : base(
        $"The game logic for {playerChoice.Name} vs {computerChoice.Name} is not defined")
    {
    }
}