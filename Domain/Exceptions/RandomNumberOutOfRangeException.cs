namespace Domain.Exceptions;

public sealed class RandomNumberOutOfRangeException : Exception
{
    public RandomNumberOutOfRangeException(int number)
        : base($"The random number {number} is out of expected range")
    {
    }
}