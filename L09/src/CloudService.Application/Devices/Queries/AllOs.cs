using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using MediatR;

namespace CloudService.Application.Devices.Queries;

public static class AllOs
{
    public record Query : IRequest<List<Os>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Os>>
    {
        public async Task<List<Os>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await unitOfWork.Devices.GetOss(cancellationToken);
        }
    }
}