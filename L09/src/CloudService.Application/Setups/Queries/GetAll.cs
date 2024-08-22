using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Queries;

public class GetAll
{
    public record Query(
        Guid UserId) : IRequest<List<Setup>>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Setup>>
    {
        public async Task<List<Setup>> Handle(Query query, CancellationToken cancellationToken)
        {
           
            var user = await unitOfWork.Users.GetByIdAsync(query.UserId, isTrack: false, includeOrderDetails: true, cancellationToken)
                ?? throw new NotFoundException("User not found");

            return user.Configs;            
        }
    }
}