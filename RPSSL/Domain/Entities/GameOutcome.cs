namespace Domain.Entities;

public class GameOutcome
{
    public string Name { get; }

    private GameOutcome(string name)
    {
        Name = name;
    }

    public static readonly GameOutcome Win = new("Win");
    public static readonly GameOutcome Lose = new("Lose");
    public static readonly GameOutcome Draw = new("Draw");
}
