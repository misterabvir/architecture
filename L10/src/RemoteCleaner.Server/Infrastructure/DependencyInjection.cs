using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Infrastructure.Persistence;
using RemoteCleaner.Server.Infrastructure.Repositories;

namespace RemoteCleaner.Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RemoteCleanerDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            options.UseSnakeCaseNamingConvention();
        });
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStationRepository, StationRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

}
