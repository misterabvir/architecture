using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Commands;

public class Stop
{
    public record Command(
        Guid UserId,
        Guid ConfigurationId) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.ConfigurationId).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken)
                ?? throw new NotFoundException("User not found");

            var configuration = user.Setups.FirstOrDefault(c => c.SetupId == command.ConfigurationId)
                ?? throw new NotFoundException("Configuration not found");

            configuration.Update(Status.Offline);
            try
            {
                await unitOfWork.Users.UpdateAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}