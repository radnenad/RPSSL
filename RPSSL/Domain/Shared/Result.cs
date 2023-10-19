namespace Domain.Shared;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }

    protected Result(bool isSuccess, T? value = default, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
}

public class Result : Result<object?>
{
    private Result(bool isSuccess, string? errorMessage = null) : base(isSuccess, null, errorMessage)
    {
    }

    public static Result Success() => new Result(true);
    public new static Result Failure(string errorMessage) => new Result(false, errorMessage);
}