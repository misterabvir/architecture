using MediatR;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Commands;

public static class AddRoom
{
    public record Command(Ulid UserId, string Title, double Square) : IRequest<DataOrError<Room>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, DataOrError<Room>>
    {
        public async Task<DataOrError<Room>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return Error.NotFound("AddRoom.NotFound", "User not found");
            }

            var result = user.AddRoom(Title.Create(command.Title), Area.Create(command.Square));
            if (result.IsFailure)
            {
                return result.Error;
            }


            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
