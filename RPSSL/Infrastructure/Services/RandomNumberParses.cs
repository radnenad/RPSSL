using System.Text.Json;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberParses : IRandomNumberParser
{
    public int Parse(string content)
    {
        var jsonDoc = JsonDocument.Parse(content);
        return jsonDoc.RootElement.GetProperty("random_number").GetInt32();
    }
}