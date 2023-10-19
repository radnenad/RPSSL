using System.Security.Cryptography;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberInternalGenerator : IRandomNumberInternalGenerator
{
    // Using RandomNumberGenerator because it provides cryptographically secure numbers 
    // Separate from the Random class, which is not thread-safe.
    // Separate method in case we want to change the implementation later.
    public int Generate()
    {
        return RandomNumberGenerator.GetInt32(100) + 1; // 1-100
    }
}