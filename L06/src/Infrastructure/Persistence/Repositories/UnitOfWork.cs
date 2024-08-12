using Application.Common.Repositories;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(NoteDbContext context, INoteRepository noteRepository) : IUnitOfWork
{
    public INoteRepository Notes => noteRepository;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
