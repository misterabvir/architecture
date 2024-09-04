using MediatR;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Commands;

public static class UpdateRoom
{
    public record Command(Ulid UserId, Ulid RoomId, string Title, double Area) : IRequest<DataOrError<Room>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, DataOrError<Room>>
    {
        public async Task<DataOrError<Room>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return Error.NotFound("UpdateRoom.UserNotFound", "User not found");
            }

            var result = user.UpdateRoom(
                RoomId.Create(command.RoomId),
                Title.Create(command.Title),
                Area.Create(command.Area));
            if (result.IsFailure)
            {
                return result.Error;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
