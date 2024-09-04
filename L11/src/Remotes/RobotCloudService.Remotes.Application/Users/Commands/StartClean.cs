
using MediatR;

using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Commands;

public static class StartClean 
{
    public record Command(Ulid UserId, Ulid RoomId, Ulid RobotId) : IRequest<DataOrError<Robot>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, DataOrError<Robot>>
    {
        public async Task<DataOrError<Robot>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return Error.NotFound("StartClean.UserNotFound", "User not found");
            }

            var result = user.StartClean(RobotId.Create(command.RobotId), RoomId.Create(command.RoomId));
            if (result.IsFailure)
            {
                return result.Error;
            }


            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result;

        }
    }
}
