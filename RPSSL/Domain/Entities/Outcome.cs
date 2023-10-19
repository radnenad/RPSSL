namespace Domain.Entities;

public class Outcome
{
    public string Name { get; }

    private Outcome(string name)
    {
        Name = name;
    }

    public static readonly Outcome Win = new("win");
    public static readonly Outcome Lose = new("lose");
    public static readonly Outcome Tie = new("tie");
}
