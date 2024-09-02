using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RobotCloudService.Authentications.Infrastructure.Persistence;

public class AuthenticationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
{
    private const string ConnectionString = "Server=localhost;Port=5432;Database=remote_db;User Id=postgres;Password=postgres;";

    public AuthenticationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AuthenticationDbContext>();
        builder.UseNpgsql(ConnectionString);
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new AuthenticationDbContext(builder.Options);
    }
}
