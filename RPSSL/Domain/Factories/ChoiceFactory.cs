using Domain.Entities;

namespace Domain.Factories;

public static class ChoiceFactory
{
    public static readonly Choice Rock = new(1, "rock");
    public static readonly Choice Paper = new(2, "paper");
    public static readonly Choice Scissors = new(3, "scissors");
    public static readonly Choice Lizard = new(4, "lizard");
    public static readonly Choice Spock = new(5, "spock");

    static ChoiceFactory()
    {
        Rock.Beats.AddRange(new[] { Scissors, Lizard });
        Paper.Beats.AddRange(new[] { Rock, Spock });
        Scissors.Beats.AddRange(new[] { Paper, Lizard });
        Lizard.Beats.AddRange(new[] { Spock, Paper });
        Spock.Beats.AddRange(new[] { Scissors, Rock });
    }

    public static Choice FromId(int id)
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

    public static Choice FromRandomNumber(int randomNumber)
    {
        if (randomNumber is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber));

        return randomNumber switch
        {
            <= 20 => Rock,
            <= 40 => Paper,
            <= 60 => Scissors,
            <= 80 => Lizard,
            _ => Spock
        };
    }
    
    public static IEnumerable<Choice> GetAll() => new[] { Rock, Paper, Scissors, Lizard, Spock };
}