using Booking.Application.Domain;

namespace Booking.Application.Common.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId);
    Task<Table?> GetTableByIdAsync(int tableId);
    Task<bool> IsExistsReservation(DateOnly reservationDate, int tableId);
    Task<int> CreateReservation(Reservation reservation);
    Task ConfirmReservationAsync(int id);
    Task CancelReservationAsync(int id);
}