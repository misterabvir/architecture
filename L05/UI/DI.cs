using DataAccessLayer.Persistence;
using Microsoft.Extensions.DependencyInjection;
using UI.Menus;

namespace UI;

public static class DI
{
    public static IServiceCollection AddUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenu>();
        services.AddScoped<ModelMenu>();
        services.AddScoped<ProjectMenu>();
        services.AddScoped<SelectAvailableProjectMenu>();
        services.AddScoped<SettingMenu>();
               
        return services;
    }
}
