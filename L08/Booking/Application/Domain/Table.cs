namespace Booking.Application.Domain;

public class Table
{
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;

    public List<Reservation> Reservations { get; set; } = [];

    public string RestaurantName { get; set;} = string.Empty;
}

