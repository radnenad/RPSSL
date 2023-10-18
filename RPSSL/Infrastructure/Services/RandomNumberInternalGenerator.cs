using System.Security.Cryptography;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberInternalGenerator : IRandomNumberInternalGenerator
{
    // Using RandomNumberGenerator because it provides cryptographically secure numbers 
    // and is thread-safe, ensuring that concurrent requests will not encounter race conditions.
    public int Generate()
    {
        // This approach allows us to directly create a buffer for the generated random number without
        // causing heap allocations and the associated overhead. Since an `int` type is 32 bits,
        // 4 bytes are necessary to hold its value. This buffer will be filled with random data which
        // will then be converted to an integer.
        Span<byte> randomBytes = stackalloc byte[4];
        RandomNumberGenerator.Fill(randomBytes);
        var randomNumber = BitConverter.ToInt32(randomBytes.ToArray(), 0);
        return Math.Abs(randomNumber % 100) + 1; // Ensures that the number is between 1 and 100.
    }
}