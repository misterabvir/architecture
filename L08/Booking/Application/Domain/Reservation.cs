namespace Booking.Application.Domain;

public class Reservation
{
    public int ReservationId { get; set; }
    public int TableId { get; set; }
    public DateOnly ReservationDate { get; set; }
    public int GuestCount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string Status {get; set;} = string.Empty;
}