using System.Text.Json.Serialization;

namespace Web.API.Contracts;

public record PlayGameRequest
{
    [JsonPropertyName("player")]
    public int PlayerChoice { get; set; }
}