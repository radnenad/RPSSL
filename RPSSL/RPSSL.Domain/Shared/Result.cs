namespace RPSSL.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; private set; }

    protected Result(bool isSuccess, string errorMessage)
    {
        switch (isSuccess)
        {
            case true when !string.IsNullOrEmpty(errorMessage):
                throw new InvalidOperationException();
            case false when string.IsNullOrEmpty(errorMessage):
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                ErrorMessage = errorMessage;
                break;
        }
    }

    public static Result Success() => new(true, string.Empty);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, string.Empty);

    public static Result Failure(string errorMessage) => new(false, errorMessage);
    public static Result<TValue> Failure<TValue>(string errorMessage) => new(default, false, errorMessage);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, string errorMessage) 
        : base(isSuccess, errorMessage) =>
        _value = value;

    public T Value => IsSuccess ? 
        _value! 
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<T>(T? value) => value is not null 
            ? Success(value) 
            : Failure<T>(string.Empty);
}