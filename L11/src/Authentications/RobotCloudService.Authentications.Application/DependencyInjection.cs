using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RobotCloudService.Application.Behaviors;

namespace RobotCloudService.Authentications.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMediatr()
            .AddCache(configuration)
            .AddQueue(configuration);
    } 


    private static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }


    private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("CacheConnection") ?? throw new NullReferenceException("CacheConnection string not found");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
        });
        return services;
    }

    private static IServiceCollection AddQueue(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(options =>
        {
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("QueueConnection") ?? throw new Exception("RabbitMq connection string not found"));
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
