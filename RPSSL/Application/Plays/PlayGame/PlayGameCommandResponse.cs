using Domain.Entities;

namespace Application.Plays.PlayGame;

public record PlayGameCommandResponse(GameChoice PlayerGameChoice, GameChoice ComputerGameChoice, GameOutcome Outcome);