using Booking.Application.Common.Results;
using Booking.Application.Domain;

namespace Booking.Application.Services;

public interface IBookingService
{
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId);
    Task<Table?> GetTableByIdAsync(int tableId);
    Task<Result> CreateReservation(Reservation reservation);
    Task<Result> Confirm(string code);
    Task<Result> Cancel(string code);
}
