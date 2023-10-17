using Application.Choices.GetAllChoices;
using Application.Choices.GetRandomChoice;
using Xunit;

namespace Web.API.IntegrationTests;

public class ChoiceTests : BaseIntegrationTest
{
    public ChoiceTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        
    }

    [Fact]
    public async Task GetAllChoices_ShouldReturnAllChoices()
    {
        var query = new GetAllChoicesQuery();
        var choices = await Sender.Send(query);
        
        Assert.NotNull(choices);
        
        var choiceResponses = choices.ToList();
        
        Assert.NotEmpty(choiceResponses);
        Assert.Equal(5, choiceResponses.Count);
    }
    
    [Fact]
    public async Task GetAllChoices_ShouldReturnChoice()
    {
        var query = new GetRandomChoiceQuery();
        var choice = await Sender.Send(query);
        
        Assert.NotNull(choice);

        var choiceId = choice.Id;
        
        Assert.InRange(choiceId, 1, 5);
    }
}