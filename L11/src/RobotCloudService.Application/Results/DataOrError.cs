namespace RobotCloudService.Application.Results;

public class DataOrError<T> : SuccessOrError
{
    public T Value { get; set; } = default!;
    public static implicit operator DataOrError<T>(Error error) => new() { Error = error };
    public static implicit operator DataOrError<T>(T value) => new() { Value = value }; 
};
