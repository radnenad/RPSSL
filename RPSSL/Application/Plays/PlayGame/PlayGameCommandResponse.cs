using Domain.Entities;

namespace Application.Plays.PlayGame;

public record PlayGameCommandResponse(Choice PlayerChoice, Choice ComputerChoice, Outcome Outcome);