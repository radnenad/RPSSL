using System.Security.Cryptography;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

public class RandomNumberInternalGenerator : IRandomNumberInternalGenerator
{
    public int Generate()
    {
        Span<byte> randomBytes = stackalloc byte[4];
        RandomNumberGenerator.Fill(randomBytes);
        var randomNumber = BitConverter.ToInt32(randomBytes.ToArray(), 0);
        return Math.Abs(randomNumber % 101);
    }
}