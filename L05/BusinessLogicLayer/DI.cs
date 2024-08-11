using BusinessLogicLayer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DI
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
               
        return services;
    }
}
