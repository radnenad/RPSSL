using Domain.Entities;

namespace Application.Mappings;

using System.Collections.Generic;
using System.Security.Cryptography;

public static class FlavorTextMapper
{
    private static readonly List<string> WinningFlavors = new()
    {
        "Awesome job!",
        "You nailed it!",
        "Epic move!",
        "Fantastic choice!",
        "Unbeatable move!"
    };

    private static readonly List<string> LosingFlavors = new()
    {
        "Nice try!",
        "Almost had it!",
        "Give it another go!",
        "You'll get them next time!",
        "Keep pushing!"
    };

    public static string GetFlavorText(Choice playerChoice, Choice computerChoice, Outcome outcome)
    {
        if (outcome == Outcome.Tie)
        {
            return $"Both chose {playerChoice.Name}. It's a tie!";
        }

        return outcome == Outcome.Win 
            ? $"{GetWinningMessage(playerChoice, computerChoice)} {GetRandomWinningFlavorText()}" 
            : $"{GetLosingMessage(playerChoice, computerChoice)} {GetRandomLosingFlavorText()}";
    }

    private static string GetRandomWinningFlavorText() 
        => WinningFlavors[RandomNumberGenerator.GetInt32(WinningFlavors.Count)];

    private static string GetRandomLosingFlavorText() 
        => LosingFlavors[RandomNumberGenerator.GetInt32(LosingFlavors.Count)];

    private static string GetWinningMessage(Choice playerChoice, Choice computerChoice)
    {
        return playerChoice.Name switch
        {
            "rock" => $"Your rock crushes the {computerChoice.Name}. You win!",
            "paper" => $"Your paper covers the {computerChoice.Name}. You win!",
            "scissors" => $"Your scissors cuts the {computerChoice.Name}. You win!",
            "lizard" => $"Your lizard devours the {computerChoice.Name}. You win!",
            "spock" => $"Your spock smashes the {computerChoice.Name}. You win!",
            _ => "Unknown outcome."
        };
    }

    private static string GetLosingMessage(Choice playerChoice, Choice computerChoice)
    {
        return computerChoice.Name switch
        {
            "rock" => $"Computer's rock crushes your {playerChoice.Name}. You lose!",
            "paper" => $"Computer's paper covers your {playerChoice.Name}. You lose!",
            "scissors" => $"Computer's scissors cuts your {playerChoice.Name}. You lose!",
            "lizard" => $"Computer's lizard devours your {playerChoice.Name}. You lose!",
            "spock" => $"Computer's spock smashes your {playerChoice.Name}. You lose!",
            _ => "Unknown outcome."
        };
    }
}

