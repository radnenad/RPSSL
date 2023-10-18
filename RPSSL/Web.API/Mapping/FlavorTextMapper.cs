using Domain.Entities;

namespace Web.API.Mapping;

public static class FlavorTextMapper
{
    public static string GetFlavorText(Choice playerChoice, Choice computerChoice, Outcome outcome)
    {
        if (outcome == Outcome.Tie)
        {
            return $"Both chose {playerChoice.Name}. It's a tie!";
        }
    
        return outcome == Outcome.Win 
            ? GetWinningMessage(playerChoice, computerChoice) 
            : GetLosingMessage(playerChoice, computerChoice);
    }

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

