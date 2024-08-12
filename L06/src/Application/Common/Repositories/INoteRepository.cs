using Domain.Notes;

namespace Application.Common.Repositories;

public interface INoteRepository
{
    Task AddAsync(Note note, CancellationToken cancellationToken);
    Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken= default);
    Task<Note?> GetByIdAsync(Guid id, bool isTrack = false, CancellationToken cancellationToken = default);
    Task RemoveAsync(Note note, CancellationToken cancellationToken);
}