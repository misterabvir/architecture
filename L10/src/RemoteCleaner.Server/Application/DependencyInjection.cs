namespace RemoteCleaner.Server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IRobotRemote, RobotRemote>();
        return services;
    }


}
