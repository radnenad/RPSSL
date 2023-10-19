using Application.Plays.PlayGame;
using FluentValidation;

namespace Application.IntegrationTests;

public class PlayTests : BaseIntegrationTest
{
    public PlayTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task PlayGame_ShouldReturnResponse_WhenChoiceIsValid(int validChoiceId)
    {
        var command = new PlayGameCommand("userId", validChoiceId);
        var response = await Sender.Send(command);
        
        Assert.NotNull(response);
        Assert.InRange(response.Computer, 1, 5);
        Assert.Contains(response.Results, new[] {"win", "lose", "tie"});
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(-5)]
    public async Task PlayGame_ShouldThrowValidationException_WhenChoiceIsInvalid(int invalidChoiceId)
    {
        var command = new PlayGameCommand("userId", invalidChoiceId);
             
        var exception = await Assert.ThrowsAsync<ValidationException>(async () => 
        {
            await Sender.Send(command);
        });
        
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }
    
    [Fact]
    public async Task PlayGame_ShouldThrowValidationException_WhenPlayerIdIsEmpty()
    {
        var command = new PlayGameCommand("", 1);
             
        var exception = await Assert.ThrowsAsync<ValidationException>(async () => 
        {
            await Sender.Send(command);
        });
        
        Assert.NotNull(exception);
        Assert.NotEmpty(exception.Errors);
    }
}