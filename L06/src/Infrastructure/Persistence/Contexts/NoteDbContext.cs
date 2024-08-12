using Domain.Notes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class NoteDbContext(DbContextOptions<NoteDbContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoteDbContext).Assembly);
    }
}