using MediatR;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Commands;

public static class UpdateRobot
{
    public record Command(Ulid UserId, Ulid RobotId, string Model, double Speed) : IRequest<DataOrError<Robot>>;
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, DataOrError<Robot>>
    {
        public async Task<DataOrError<Robot>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return Error.NotFound("UpdateRobot.NotFound", "User not found");
            }

            var result = user.UpdateRobot(
                RobotId.Create(command.RobotId),
                Model.Create(command.Model), 
                Speed.Create(command.Speed));
            if (result.IsFailure)
            {
                return result.Error;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
