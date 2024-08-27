using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Domain;

namespace RemoteCleaner.Server.Infrastructure.Persistence;

public class RemoteCleanerDbContext(DbContextOptions<RemoteCleanerDbContext> options)
    : DbContext(options)
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Station> Stations { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RemoteCleanerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}