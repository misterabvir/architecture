namespace Application.Common.Repositories;

public interface IUnitOfWork
{
    INoteRepository Notes { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}