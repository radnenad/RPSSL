using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Factories;

namespace Application.UnitTests;

public class GameResultEvaluatorTests
{
    private readonly GameResultEvaluator _gameResultEvaluator = new();

    [Theory]
    [ClassData(typeof(GameTestData))]
    public void EvaluateGameResult_ShouldReturnExpectedOutcome(Choice playerChoice, Choice computerChoice,
        Outcome expectedOutcome)
    {
        var result = _gameResultEvaluator.Evaluate(playerChoice, computerChoice);
        Assert.Equal(expectedOutcome, result);
    }
    
    [Fact]
    public void EvaluateGameResult_ShouldThrowException_ForInvalidChoices()
    {
        var invalidPlayerChoice = new Choice(6, "invalid");
        var computerChoice = ChoiceFactory.Rock;
        
        var exception = Assert.Throws<UndefinedGameLogicException>(() =>
        {
            _gameResultEvaluator.Evaluate(invalidPlayerChoice, computerChoice);
        });
        
        Assert.NotNull(exception);
        Assert.IsType<UndefinedGameLogicException>(exception);
    }
    
}