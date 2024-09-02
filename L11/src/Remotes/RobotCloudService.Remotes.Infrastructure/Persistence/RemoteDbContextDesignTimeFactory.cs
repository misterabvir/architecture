using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RobotCloudService.Remotes.Infrastructure.Persistence;

internal class RemoteDbContextDesignTimeFactory : IDesignTimeDbContextFactory<RemoteDbContext>
{
    private const string ConnectionString = "Server=localhost;Port=5432;Database=remote_db;User Id=postgres;Password=postgres;";
    public RemoteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RemoteDbContext>()
            .UseNpgsql(ConnectionString);

        return new RemoteDbContext(optionsBuilder.Options);
    }
}
