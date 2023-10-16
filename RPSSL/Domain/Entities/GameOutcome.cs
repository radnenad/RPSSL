namespace Domain.Entities;

public class GameOutcome
{
    public string Name { get; }

    private GameOutcome(string name)
    {
        Name = name;
    }

    public static readonly GameOutcome Win = new("win");
    public static readonly GameOutcome Lose = new("lose");
    public static readonly GameOutcome Tie = new("tie");
}
