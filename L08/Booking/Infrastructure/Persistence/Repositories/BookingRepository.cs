using Booking.Application.Common.Repositories;
using Booking.Application.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Infrastructure.Persistence.Repositories;

public class BookingCacheRepository(BookingRepository decorated, IMemoryCache cache) : IBookingRepository
{
    public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId)
    {
        var restaurant = cache.Get<Restaurant>($"restaurant#{restaurantId}");
        if (restaurant is null) {

            restaurant = await decorated.GetRestaurantByIdAsync(restaurantId);
            if(restaurant is not null)
            {
                cache.Set($"restaurant#{restaurantId}", restaurant);
            }
        }
        return restaurant;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
    {
        var restaurants = cache.Get<IEnumerable<Restaurant>>("all-restaurant");
        if (restaurants is null) 
        {
            restaurants = await decorated.GetRestaurantsAsync();
            if (restaurants.Any())
            {
                cache.Set("all-restaurant", restaurants);
            }
        }
        return restaurants;

    }

    public async Task<Table?> GetTableByIdAsync(int tableId)
    {
        var table = cache.Get<Table>($"table#{tableId}");
        if(table is null)
        {
            table = await decorated.GetTableByIdAsync(tableId);
            if(table is not null)
            {
                cache.Set($"table#{tableId}", table);
            }
        }
        return table;
    }



    public async Task CancelReservationAsync(int id)
    {
        await decorated.CancelReservationAsync(id);
    }

    public async Task ConfirmReservationAsync(int id)
    {
        await decorated.ConfirmReservationAsync(id);
    }

    public async Task<int> CreateReservation(Reservation reservation)
    {
        return await decorated.CreateReservation(reservation);
    }

    public async Task<bool> IsExistsReservation(DateOnly reservationDate, int tableId)
    {
        return await decorated.IsExistsReservation(reservationDate, tableId);
    }
}
