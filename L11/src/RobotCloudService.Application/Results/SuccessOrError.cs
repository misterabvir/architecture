namespace RobotCloudService.Application.Results;

public class SuccessOrError
{
    public Error Error { get; init; } = Error.None;

    public bool IsSuccess => Error == Error.None;
    public bool IsFailure => !IsSuccess;

    protected SuccessOrError() { }

    public static SuccessOrError Success => new();
    public static implicit operator SuccessOrError(Error error) => new() { Error = error };

    public T Match<T>(Func<T> success, Func<Error, T> failure)
    {
        return IsSuccess ? success.Invoke() : failure.Invoke(Error);
    }
}
