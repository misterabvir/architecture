using RobotCloudService.Remotes.Presentation.Endpoints;

namespace RobotCloudService.Remotes.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

      
        app.MapRemoteEndpoints();
        return app;
    }
}
