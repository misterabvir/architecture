using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Booking.Models;
using Booking.Application.Services;
using Booking.Application.Domain;

namespace Booking.Controllers;

public class BookController(IBookingService service) : Controller
{
    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var restaurants = await service.GetRestaurantsAsync();
        var vm = restaurants.Select(RestaurantViewModel.ToViewModel);
        return View(vm);
    }

    [HttpGet]
    [Route("/restaurant/{restaurantId}")]
    public async Task<IActionResult> Restaurant(int restaurantId)
    {
        var restaurant = await service.GetRestaurantByIdAsync(restaurantId);

        if (restaurant is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var vm = RestaurantViewModel.ToViewModel(restaurant);
        return View(vm);
    }

    [HttpGet]
    [Route("/tables/{tableId}")]
    public async Task<IActionResult> Table(int tableId)
    {
        var table = await service.GetTableByIdAsync(tableId);

        if (table is null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(new ReservationViewModel() { Table = TableViewModel.ToViewModel(table) });
    }

    [HttpPost]
    [Route("/reserving")]
    public async Task<IActionResult> Reserving(ReservationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Table = TableViewModel.ToViewModel((await service.GetTableByIdAsync(model.TableId))!);
            return View("Table", model);
        }

        var result = await service.CreateReservation(new Reservation()
        {
            CustomerEmail = model.CustomerEmail,
            CustomerName = model.CustomerName,
            CustomerPhone = model.CustomerPhone,
            ReservationDate = model.ReservationDate,
            GuestCount = model.GuestCount,
            TableId = model.TableId
        });

        if (result.IsSuccess)
        {
            return View("Result", ResultViewModel.CheckEmail());
        }

        return View("Result", ResultViewModel.Fail(result.ErrorMessage));
    }

    [Route("/confirm/{code}")]
    public async Task<IActionResult> Confirm(string code)
    {
        var result = await service.Confirm(code);
        if (result.IsSuccess) {
            return View("Result", ResultViewModel.Confirm());
        }
        return View("Result", ResultViewModel.Fail(result.ErrorMessage));
    }

    [Route("/cancel/{code}")]
    public async Task<IActionResult> Cancel(string code)
    {
        var result = await service.Cancel(code);
        if (result.IsSuccess)
        {
            return View("Result", ResultViewModel.Cancel());
        }
        return View("Result", ResultViewModel.Fail(result.ErrorMessage));
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
