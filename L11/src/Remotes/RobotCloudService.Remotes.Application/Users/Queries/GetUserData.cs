using MediatR;

using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;

namespace RobotCloudService.Remotes.Application.Users.Queries;

public static class GetUserData
{
    public record Query(Ulid UserId) : IRequest<DataOrError<User>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, DataOrError<User>>
    {
        public async Task<DataOrError<User>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(query.UserId, cancellationToken);
            if(user is null)
            {
                return Error.NotFound("GetUserData.UserNotFound", "User not found");
            }
            return user;
        }
    }
}
