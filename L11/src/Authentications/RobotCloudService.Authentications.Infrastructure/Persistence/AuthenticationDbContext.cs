using Microsoft.EntityFrameworkCore;

using RobotCloudService.Authentications.Application.Users;

namespace RobotCloudService.Authentications.Infrastructure.Persistence;

public sealed class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("authentication");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
