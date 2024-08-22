using CloudService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CloudService.Infrastructure.Contexts;

public class CloudServiceDbContext(DbContextOptions<CloudServiceDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<Ram> Rams { get; set; }
    public DbSet<Rom> Roms { get; set; }
    public DbSet<Os> Oss { get; set; }
    public DbSet<Ip> Ips { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cloud");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CloudServiceDbContext).Assembly);
    }
}
