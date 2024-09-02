using MediatR;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Entities;

namespace RobotCloudService.Remotes.Application.Users.Queries;

public static class GetUserLogs
{
    public record Query(Ulid UserId) : IRequest<DataOrError<IEnumerable<Log>>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, DataOrError<IEnumerable<Log>>>
    {
        public async Task<DataOrError<IEnumerable<Log>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(query.UserId, cancellationToken);
            if (user is null)
            {
                return Error.NotFound("GetUserLogs.NotFound", "User not found");
            }
            return user.Logs.ToList();
        }
    }
}
