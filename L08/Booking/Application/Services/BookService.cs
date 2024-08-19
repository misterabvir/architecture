using Booking.Application.Common;
using Booking.Application.Common.Repositories;
using Booking.Application.Common.Results;
using Booking.Application.Domain;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Application.Services;

public class BookingService(
    IBookingRepository repository, 
    IEmailSender emailSender, 
    IMemoryCache cache, 
    SettingsApplication settings) : IBookingService
{
    private string Email(string code) => $"""
        <p>Cancel: <a href="{settings.BaseAddress}/cancel/{code}]"> here </a></p>
        <p>Confirm: <a href="{settings.BaseAddress}/confirm/{code}"> here </a></p>
        """;

    public async Task<Result> Cancel(string code)
    {
        var id = cache.Get<int>(code);
        if (id == 0)
        {
            return "Not found you reservation";
        }
        await repository.CancelReservationAsync(id);
        cache.Remove(code);
        return Result.Success;
    }

    public async Task<Result> Confirm(string code)
    {
        var id = cache.Get<int>(code);
        if(id == 0)
        {
            return "Not found you reservation";
        }
        await repository.ConfirmReservationAsync(id);
        cache.Remove(code);
        return Result.Success;
    }

    public async Task<Result> CreateReservation(Reservation reservation)
    {
        if(await repository.IsExistsReservation(reservation.ReservationDate, reservation.TableId))
        {
            return $"Reservation with date {reservation.ReservationDate} on selected table already exist, sorry you can try again";
        }
        reservation.ReservationId = await repository.CreateReservation(reservation);
        var code = Convert.ToHexString(Guid.NewGuid().ToByteArray());
        cache.Set(code, reservation.ReservationId);
        await emailSender.SendEmailAsync(reservation.CustomerEmail, "Confirm reservation", Email(code));
        
        return Result.Success;
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId)
    {
        return await repository.GetRestaurantByIdAsync(restaurantId);
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
    {
        return await repository.GetRestaurantsAsync();
    }

    public async Task<Table?> GetTableByIdAsync(int tableId)
    {
        return await repository.GetTableByIdAsync(tableId);
    }
}