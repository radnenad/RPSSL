namespace Domain.Shared;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }
    
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, T? value = default, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new(true, value);
    public static Result<T> Failure(string? errorMessage) => new(false, default, errorMessage);
}

public class Result : Result<object?>
{
    private Result(bool isSuccess, string? errorMessage = null) : base(isSuccess, null, errorMessage)
    {
    }

    public static Result Success() => new(true);
    public new static Result Failure(string errorMessage) => new(false, errorMessage);
}