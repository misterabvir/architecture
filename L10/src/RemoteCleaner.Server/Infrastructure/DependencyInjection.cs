using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Infrastructure.Persistence;

namespace RemoteCleaner.Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
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

}
