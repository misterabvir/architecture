namespace Booking.Models;

public class ResultViewModel
{
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string Details { get; init; }
    public required string HeaderClass {get; init;}

    public static ResultViewModel CheckEmail() =>
        new()
        {
            Title = "Reservation Successful!",
            Message = "Please check your email and confirm your reservation.",
            Details = "We've sent you an email with a confirmation link. Your reservation will be finalized once you click the link.",
            HeaderClass = "simple-header"
        };

    public static ResultViewModel Fail(string error) =>
        new()
        {
            Title = "Reservation Failed!",
            Message = "Unfortunately, we couldn't process your reservation",
            Details = $"Please try again or choose a different option. {error}",
            HeaderClass = "fail-header"
        };
    public static ResultViewModel Confirm() =>
    new()
    {
        Title = "Reservation Confirmed!",
        Message = "Your reservation has been successfully confirmed",
        Details = "We look forward to seeing you. If you need to make any changes, please contact us.",
        HeaderClass = "success-header"
    };
    public static ResultViewModel Cancel() =>
    new()
    {
        Title = "Reservation Canceled!",
        Message = "Your reservation has been successfully canceled.",
        Details = "We're sorry to see you go. If you change your mind, feel free to make a new reservation.",
        HeaderClass = "simple-header"
    };

}
