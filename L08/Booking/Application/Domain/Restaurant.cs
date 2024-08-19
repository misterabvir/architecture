namespace Booking.Application.Domain;

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public List<Table> Tables { get; set; } = [];
}
