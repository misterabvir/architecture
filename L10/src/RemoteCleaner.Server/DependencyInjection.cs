using RemoteCleaner.Server.Endpoints;
using RemoteCleaner.Server.Hubs;

namespace RemoteCleaner.Server;

public static class DependencyInjection
{
    public static IServiceCollection AddServer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSignalR();
        return services;
    }

    public static WebApplication UseServer(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapAppEndpoint();
        app.MapHub<RemoteHub>("/remote-hub");

        return app;
    }
}
