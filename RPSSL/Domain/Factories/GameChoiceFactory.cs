using Domain.Entities;

namespace Domain.Factories;

public static class GameChoiceFactory
{
    public static readonly GameChoice Rock = new(1, "rock");
    public static readonly GameChoice Paper = new(2, "paper");
    public static readonly GameChoice Scissors = new(3, "scissors");
    public static readonly GameChoice Lizard = new(4, "lizard");
    public static readonly GameChoice Spock = new(5, "spock");

    static GameChoiceFactory()
    {
        Rock.Beats.AddRange(new[] { Scissors, Lizard });
        Paper.Beats.AddRange(new[] { Rock, Spock });
        Scissors.Beats.AddRange(new[] { Paper, Lizard });
        Lizard.Beats.AddRange(new[] { Spock, Paper });
        Spock.Beats.AddRange(new[] { Scissors, Rock });
    }

    public static GameChoice FromId(int id)
    {
        return id switch
        {
            1 => Rock,
            2 => Paper,
            3 => Scissors,
            4 => Lizard,
            5 => Spock,
            _ => throw new ArgumentException($"Invalid choice ID: {id}"),
        };
    }
    
    public static IEnumerable<GameChoice> GetAll() => new[] { Rock, Paper, Scissors, Lizard, Spock };
}