namespace Domain.Exceptions;

public sealed class InvalidChoiceException : Exception
{
    public InvalidChoiceException(int id)
        :base($"The choice with the ID = {id} is not valid")
    {
    }
}