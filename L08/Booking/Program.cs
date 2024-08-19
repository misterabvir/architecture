using Booking;
using Booking.Application;
using Booking.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddWeb();

builder.Build().UseWeb().Run();
