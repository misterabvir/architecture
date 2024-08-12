using Application.Common.Repositories;
using Domain.Notes;
using MediatR;

namespace Application.Notes.Queries;

public static class GetAll
{
    public record Query : IRequest<IEnumerable<Note>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, IEnumerable<Note>>
    {
        public async Task<IEnumerable<Note>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await unitOfWork.Notes.GetAllAsync(cancellationToken);
        }
    }
}