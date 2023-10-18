using Domain.Entities;

namespace Domain.Factories;

public static class ChoiceFactory
{
    private static readonly Choice Rock = new(1, "rock");
    private static readonly Choice Paper = new(2, "paper");
    private static readonly Choice Scissors = new(3, "scissors");
    private static readonly Choice Lizard = new(4, "lizard");
    private static readonly Choice Spock = new(5, "spock");
    
    private static readonly Dictionary<int, Choice> ChoicesById = new()
    {
        {1, Rock},
        {2, Paper},
        {3, Scissors},
        {4, Lizard},
        {5, Spock}
    };

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
        if (ChoicesById.TryGetValue(id, out var choice))
            return choice;

        throw new ArgumentException($"Invalid choice ID: {id}");
    }

    public static Choice FromRandomNumber(int randomNumber)
    {
        if (randomNumber is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(randomNumber));

        // With modulo operator we ensure that the number is between 1 and 5.
        var choiceId = randomNumber % 5 + 1; 
        return FromId(choiceId);
    }
    
    public static IEnumerable<Choice> GetAll() => ChoicesById.Values;
}