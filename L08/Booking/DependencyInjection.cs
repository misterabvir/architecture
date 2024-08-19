namespace Booking;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {

        services.AddControllersWithViews();
        return services;
    }

    public static WebApplication UseWeb(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Book}/{action=Index}/{id?}");
        return app;
    }

}