namespace RobotCloudService.Authentications.Application.Common.Repositories;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
