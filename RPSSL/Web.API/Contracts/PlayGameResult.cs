using System.Text.Json.Serialization;

namespace Web.API.Contracts;

public record PlayGameResult(string Results, int Player, int Computer);