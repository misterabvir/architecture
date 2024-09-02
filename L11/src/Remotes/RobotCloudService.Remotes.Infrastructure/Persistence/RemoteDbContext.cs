using Microsoft.EntityFrameworkCore;

using RobotCloudService.Remotes.Application.Users;

namespace RobotCloudService.Remotes.Infrastructure.Persistence;

public class RemoteDbContext(DbContextOptions<RemoteDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("remotes");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RemoteDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
