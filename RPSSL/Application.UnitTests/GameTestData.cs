using System.Collections;
using Domain.Entities;
using Domain.Factories;

namespace Application.UnitTests;

public class GameTestData : IEnumerable<object[]>
{
    private static readonly List<object[]> Data = new()
    {
        // Rock combinations
        new object[] { ChoiceFactory.Rock, ChoiceFactory.Rock, Outcome.Tie },
        new object[] { ChoiceFactory.Rock, ChoiceFactory.Paper, Outcome.Lose },
        new object[] { ChoiceFactory.Rock, ChoiceFactory.Scissors, Outcome.Win },
        new object[] { ChoiceFactory.Rock, ChoiceFactory.Lizard, Outcome.Win },
        new object[] { ChoiceFactory.Rock, ChoiceFactory.Spock, Outcome.Lose },

        // Paper combinations
        new object[] { ChoiceFactory.Paper, ChoiceFactory.Rock, Outcome.Win },
        new object[] { ChoiceFactory.Paper, ChoiceFactory.Paper, Outcome.Tie },
        new object[] { ChoiceFactory.Paper, ChoiceFactory.Scissors, Outcome.Lose },
        new object[] { ChoiceFactory.Paper, ChoiceFactory.Lizard, Outcome.Lose },
        new object[] { ChoiceFactory.Paper, ChoiceFactory.Spock, Outcome.Win },

        // Scissors combinations
        new object[] { ChoiceFactory.Scissors, ChoiceFactory.Rock, Outcome.Lose },
        new object[] { ChoiceFactory.Scissors, ChoiceFactory.Paper, Outcome.Win },
        new object[] { ChoiceFactory.Scissors, ChoiceFactory.Scissors, Outcome.Tie },
        new object[] { ChoiceFactory.Scissors, ChoiceFactory.Lizard, Outcome.Win },
        new object[] { ChoiceFactory.Scissors, ChoiceFactory.Spock, Outcome.Lose },

        // Lizard combinations
        new object[] { ChoiceFactory.Lizard, ChoiceFactory.Rock, Outcome.Lose },
        new object[] { ChoiceFactory.Lizard, ChoiceFactory.Paper, Outcome.Win },
        new object[] { ChoiceFactory.Lizard, ChoiceFactory.Scissors, Outcome.Lose },
        new object[] { ChoiceFactory.Lizard, ChoiceFactory.Lizard, Outcome.Tie },
        new object[] { ChoiceFactory.Lizard, ChoiceFactory.Spock, Outcome.Win },

        // Spock combinations
        new object[] { ChoiceFactory.Spock, ChoiceFactory.Rock, Outcome.Win },
        new object[] { ChoiceFactory.Spock, ChoiceFactory.Paper, Outcome.Lose },
        new object[] { ChoiceFactory.Spock, ChoiceFactory.Scissors, Outcome.Win },
        new object[] { ChoiceFactory.Spock, ChoiceFactory.Lizard, Outcome.Lose },
        new object[] { ChoiceFactory.Spock, ChoiceFactory.Spock, Outcome.Tie }
    };

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}