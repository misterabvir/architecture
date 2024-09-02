using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Infrastructure.Authorizations;
using RobotCloudService.Remotes.Infrastructure.Backgrounds;
using RobotCloudService.Remotes.Infrastructure.Persistence;
using RobotCloudService.Remotes.Infrastructure.Repositories;

namespace RobotCloudService.Remotes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection  AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddRepositories()
            .AddBackgroundServices()
            .AddServices(configuration);
    }


    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found.");

        services.AddDbContext<RemoteDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
          
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository,UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }   
    
    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddSingleton<StopCleanBackgroundService>();
        services.AddHostedService(
            provider => provider.GetRequiredService<StopCleanBackgroundService>());

        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddToken(configuration);
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Robot Cloud Service Api (Remote)", Version = "v1" });
            option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}
