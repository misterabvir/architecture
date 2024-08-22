using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using MediatR;

namespace CloudService.Application.Devices.Queries;

public static class AllRam
{
    public record Query : IRequest<List<Ram>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Ram>>
    {
        public async Task<List<Ram>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await unitOfWork.Devices.GetRams(cancellationToken);
        }
    }
}
