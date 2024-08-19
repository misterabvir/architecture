namespace Booking.Application.Common.Results;

public class Result
{
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

    public static Result Success => new();
    public static implicit operator Result(string message) => new() { ErrorMessage = message };
}
