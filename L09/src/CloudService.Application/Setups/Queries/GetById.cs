using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Queries;

public class GetById
{
    public record Query(
        Guid UserId,
        Guid ConfigurationId) : IRequest<Setup>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.ConfigurationId).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Setup>
    {
        public async Task<Setup> Handle(Query command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken)
                ?? throw new NotFoundException("User not found");

            var configuration = user.Setups.FirstOrDefault(c => c.SetupId == command.ConfigurationId)
                ?? throw new NotFoundException("Configuration not found");

            return configuration;
        }
    }
}