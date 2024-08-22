using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Commands;

public class Remove
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

            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, isTrack: true, includeOrderDetails: true, cancellationToken)
                ?? throw new NotFoundException("User not found");

            var configuration = user.Configs.FirstOrDefault(c => c.SetupId == command.ConfigurationId)
                ?? throw new NotFoundException("Configuration not found");

            user.Configs.Remove(configuration);
            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (System.Exception)
            {

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}