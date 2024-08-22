using CloudService.Application.Base.Repositories;
using CloudService.Application.Base.Services;
using CloudService.Infrastructure.Contexts;
using CloudService.Infrastructure.Repositories;
using CloudService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddServices(configuration);
        services.AddCache(configuration);
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection")
            ?? throw new Exception("DbConnection not configured");
        
        services.AddDbContext<CloudServiceDbContext>(options => {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository, UserCacheRepository>();
        services.AddScoped<DeviceRepository>();
        services.AddScoped<IDeviceRepository, DeviceCacheRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new Exception("JwtSetting not configured");

        services.AddSingleton(tokenSettings);
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = tokenSettings.TokenValidationParameters);
        services.AddAuthorizationBuilder();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
            
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionCacheString = configuration.GetConnectionString("CacheConnection") ??
                throw new Exception("CacheConnection string not configured");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionCacheString;
            options.InstanceName = "cloud-service";
            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { connectionCacheString }
            };
        });
        return services;
    }
}