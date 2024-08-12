using Application.Common.Repositories;
using Domain.Notes;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class NoteRepository(NoteDbContext context) : INoteRepository
{
    public async Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Notes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<Note?> GetByIdAsync(Guid id, bool isTrack = false, CancellationToken cancellationToken = default)
    {
        var query = context.Notes.AsQueryable();
        if (isTrack)
        {
            query = query.AsTracking();
        }

        return query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Note note, CancellationToken cancellationToken)
    {
        await context.Notes.AddAsync(note, cancellationToken);
    }

    public async Task RemoveAsync(Note note, CancellationToken cancellationToken)
    {
        await Task.Run(() => context.Notes.Remove(note), cancellationToken);
    }
}
