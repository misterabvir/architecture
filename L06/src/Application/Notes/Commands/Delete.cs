using Application.Common.Repositories;
using Application.Common.Results;
using FluentValidation;
using MediatR;

namespace Application.Notes.Commands;

public static class Delete
{
    public record Command(Guid Id) : IRequest<Result>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, Result>
    {

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var note = await unitOfWork.Notes.GetByIdAsync(request.Id, isTrack:true, cancellationToken: cancellationToken);

            if (note is null)
            {
                return Error.NotFound($"Note with id {request.Id} not found");
            }

            await unitOfWork.Notes.RemoveAsync(note, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}