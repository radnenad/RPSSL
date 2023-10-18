using Application.Plays.PlayGame;
using Domain.Factories;
using Xunit;

namespace Web.API.IntegrationTests;

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
    public async Task PlayGame_ShouldReturnResponse_WhenChoiceIsValid(int choiceId)
    {
        var validChoice = ChoiceFactory.FromId(choiceId);
        var request = new PlayGameCommand("id", validChoice); //TODO use a fixture for this
        var response = await Sender.Send(request);
        
        Assert.NotNull(response);
        Assert.InRange(response.ComputerChoice.Id, 1, 5);
        Assert.InRange(response.Outcome.Id, 1, 3);
    }
    
    //TODO async Task PlayGame_ShouldReturnError_WhenChoiceIsNotValid(int choiceId)
}