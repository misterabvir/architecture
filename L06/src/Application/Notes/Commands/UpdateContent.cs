using Application.Common.Repositories;
using Application.Common.Results;
using Domain.Notes.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.Notes.Commands;

public class UpdateContent
{
    public record Command(Guid Id, string Content) : IRequest<Result>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MinimumLength(Content.MinLength).MaximumLength(Content.MaxLength);
        }
    }

    public class Handler(IUnitOfWork unitOfWork): IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var note = await unitOfWork.Notes.GetByIdAsync(request.Id, isTrack: true, cancellationToken: cancellationToken);

            if (note is null)
            {
                return Error.NotFound($"Note with id {request.Id} not found");
            }

            note.UpdateContent(Content.Create(request.Content));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}