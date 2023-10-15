namespace Domain.Entities;

public class Choice
{
    public ChoiceEnum Id { get; }
    public string Name { get; }
    
    public Choice(ChoiceEnum id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static readonly Choice Rock = new(ChoiceEnum.Rock, "Rock");
    public static readonly Choice Paper = new(ChoiceEnum.Paper, "Paper");
    public static readonly Choice Scissors = new(ChoiceEnum.Scissors, "Scissors");
    public static readonly Choice Lizard = new(ChoiceEnum.Lizard, "Lizard");
    public static readonly Choice Spock = new(ChoiceEnum.Spock, "Spock");
}