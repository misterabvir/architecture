using Microsoft.OpenApi.Models;

using RobotCloudService.Authentications.Presentation.Endpoints;

namespace RobotCloudService.Authentications.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options=> options.SwaggerDoc("v1", new OpenApiInfo { Title = "Robot Cloud Service Api (Authentication)", Version = "v1" }));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseCors("AllowAll");
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.MapAuthenticationEndpoints();

        return app;
    }


}
