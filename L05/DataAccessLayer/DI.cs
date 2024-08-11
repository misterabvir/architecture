using DataAccessLayer.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DI
{
    public static IServiceCollection AddDLL(this IServiceCollection services)
    {
        services.AddDbContext<ProjectDbContext>();
               
        return services;
    }
}
