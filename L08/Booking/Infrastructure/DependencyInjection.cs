using Booking.Application.Common.Repositories;
using Booking.Infrastructure.Emails;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Persistence.Repositories;
using Booking.Infrastructure.Persistence.SqlMappers;
using Dapper;


namespace Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddEmailSender(configuration);
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DbConnection") ?? throw new Exception("Connection string not found");
        services.AddTransient(_ => new DbConnectionFactory(connectionString));
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<BookingRepository>();
        services.AddTransient<IBookingRepository, BookingCacheRepository>();
        return services;
    }
}