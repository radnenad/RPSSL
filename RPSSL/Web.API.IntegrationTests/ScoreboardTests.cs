using Application.Plays.PlayGame;
using Application.Scoreboards.GetScoreboard;
using Application.Scoreboards.ResetScoreboard;
using FluentValidation;
using Xunit;

namespace Web.API.IntegrationTests;

public class ScoreboardTests : BaseIntegrationTest
{
    public ScoreboardTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Get_ShouldReturnEmptyList_WhenGameIsNotPlayed()
    {
        const string playerId = "player1";
        var query = new GetScoreboardQuery(playerId);
        var scoreboard = await Sender.Send(query);

        Assert.NotNull(scoreboard);
        Assert.NotNull(scoreboard.Results);
        Assert.Empty(scoreboard.Results);
    }
    
    [Fact]
    public async Task Get_ShouldReturnList_WhenGameIsPlayed()
    {
        const string playerId = "player2";
        var playGameCommand = new PlayGameCommand(playerId, 2);
        await Sender.Send(playGameCommand);
        var query = new GetScoreboardQuery(playerId);
        var scoreboard = await Sender.Send(query);

        Assert.NotNull(scoreboard);
        Assert.NotNull(scoreboard.Results);
        Assert.NotEmpty(scoreboard.Results);
    }

    [Fact]
    public async Task Get_ShouldThrowValidationException_WhenPlayerIdIsOrEmpty()
    {
        var playerId = string.Empty;
        var query = new GetScoreboardQuery(playerId);
        var exception = await Assert.ThrowsAsync<ValidationException>(async () => 
        {
            await Sender.Send(query);
        });
        
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }
    
    [Fact]
    public async Task Reset_ShouldReturnSuccessResult_WhenPlayerExists()
    {
        const string playerId = "player3";
        var playGameCommand = new PlayGameCommand(playerId, 4);
        await Sender.Send(playGameCommand);
        var command = new ResetScoreBoardCommand(playerId);
        var result = await Sender.Send(command);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task Reset_ShouldReturnFailureResult_WhenPlayerNotExists()
    {
        const string playerId = "player4";
        var command = new ResetScoreBoardCommand(playerId);
        var result = await Sender.Send(command);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public async Task Reset_ShouldThrowValidationException_WhenPlayerIdIsEmpty()
    {
        const string playerId = "";
        var command = new ResetScoreBoardCommand(playerId);
        var exception = await Assert.ThrowsAsync<ValidationException>(async () => 
        {
            await Sender.Send(command);
        });
        
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }

}