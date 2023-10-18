namespace Domain.Entities;

public record GameResult(string PlayerId, Choice PlayerChoice, Choice ComputerChoice, Outcome Outcome,
    DateTime Timestamp);