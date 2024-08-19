using Booking.Application.Common;
using Booking.Application.Services;

namespace Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(SettingsApplication.SectionName).Get<SettingsApplication>()
            ?? throw new Exception("Settings Application not configured");
        services.AddSingleton(settings);
        services.AddTransient<IBookingService, BookingService>();
        services.AddMemoryCache();
        return services;
    }


}