namespace Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    
    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new(true);
    public static Result Failure(string errorMessage) => new(false, errorMessage);
}
