using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Infrastructure.Persistence;
using RobotCloudService.Authentications.Infrastructure.Repositories;
using RobotCloudService.Authentications.Infrastructure.Services;

namespace RobotCloudService.Authentications.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection  AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddRepositories()
            .AddServices(configuration);
    }


    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found.");

        services.AddDbContext<AuthenticationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
          
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHashService(configuration)
            .AddSmtp(configuration)
            .AddToken(configuration)
            .AddVerification(configuration);       
        
        return services;
    }
    
}
