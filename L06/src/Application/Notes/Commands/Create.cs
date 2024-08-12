using Application.Common.Repositories;
using Application.Common.Results;
using Domain.Notes;
using Domain.Notes.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.Notes.Commands;

public static class Create
{
    public record Command(string Title, string Content) : IRequest<Result<Note>>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(Title.MinLength).MaximumLength(Title.MaxLength);
            RuleFor(x => x.Content).NotEmpty().MinimumLength(Content.MinLength).MaximumLength(Content.MaxLength);
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<Note>>
    {

        public async Task<Result<Note>> Handle(Command request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);
            var content = Content.Create(request.Content);
            
            var note = Note.Create(title, content);

            await unitOfWork.Notes.AddAsync(note, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return note;
        }
    }
}