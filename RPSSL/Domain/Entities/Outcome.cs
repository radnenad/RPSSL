namespace Domain.Entities;

public class Outcome
{
    public int Id { get; }
    public string Name { get; }

    private Outcome(int id, string name)
    {
        Name = name;
        Id = id;
    }

    public static readonly Outcome Win = new(1, "win");
    public static readonly Outcome Lose = new(2, "lose");
    public static readonly Outcome Tie = new(3, "tie");
}
