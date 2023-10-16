using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberGenerator : IRandomNumberGenerator
{
    private static readonly Random NumberGenerator = new();

    public int Generate()
    {
        return NumberGenerator.Next(1, 101);
    }
}