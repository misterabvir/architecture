using Booking.Application.Domain;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models;

public class RestaurantViewModel
{   
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<TableViewModel> Tables { get; set; } = [];
    public string Image { get; set; } = string.Empty;

    public static RestaurantViewModel ToViewModel(Restaurant restaurant) => new()
    {
        RestaurantId = restaurant.RestaurantId,
        Name = restaurant.Name,
        Address = restaurant.Address,
        Phone = restaurant.Phone,
        Tables = restaurant.Tables.Select(TableViewModel.ToViewModel).ToList(),
        Image = $"/images/restaurants/{restaurant.Name.Split(' ').First().ToLower()}.jpg"
    };
}
public class TableViewModel
{
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string RestaurantName { get; set; } = string.Empty;

    public static TableViewModel ToViewModel(Table table) => new()
    {
        TableId = table.TableId,
        RestaurantId = table.RestaurantId,
        TableNumber = table.TableNumber,
        Capacity = table.Capacity,
        Location = table.Location,
        RestaurantName = table.RestaurantName,
        Image = $"/images/rooms/{table.Location.Split(' ').First().ToLower()}.jpg",
    };
}

public class ReservationViewModel
{
    public int TableId { get; set; }
    public DateOnly ReservationDate { get; set; }
    public int GuestCount { get; set; }

    [Required]
    [MinLength(5)]
    public string CustomerName { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string CustomerPhone { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;
    public TableViewModel? Table { get; set; }
}
