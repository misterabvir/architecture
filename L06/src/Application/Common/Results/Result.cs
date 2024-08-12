
namespace Application.Common.Results;

public class Result
{
    public Error Error { get; private set; } = Error.None;
    public bool IsSuccess => Error == Error.None;
    public bool IsFailure => !IsSuccess;

    protected Result() { }
    protected Result(Error error) => Error = error;

    public static Result Success() => new();
    public static implicit operator Result(Error error) => new(error);
    public TAction Match<TAction>(Func<TAction> success, Func<Error, TAction> failure) => IsSuccess ? success() : failure(Error);
}

public class Result<T> : Result
{
    public T Value { get; private set; } = default!;
    protected Result() { }
    protected Result(Error error) : base(error) { }

    protected Result(T value) => Value = value;

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);

    
}
