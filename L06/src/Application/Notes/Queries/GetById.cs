using Application.Common.Repositories;
using Application.Common.Results;
using Domain.Notes;
using FluentValidation;
using MediatR;

namespace Application.Notes.Queries;

public static class GetNote
{
    public record Query(Guid Id) : IRequest<Result<Note>>;
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<Note>>
    {
        public async Task<Result<Note>> Handle(Query request, CancellationToken cancellationToken)
        {
            var note = await unitOfWork.Notes.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (note == null)
            {
                return Error.NotFound($"Note with id {request.Id} not found");
            }
            return note;
        }
    }
}