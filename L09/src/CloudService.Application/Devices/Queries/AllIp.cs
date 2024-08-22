using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using MediatR;

namespace CloudService.Application.Devices.Queries;

public static class AllIp
{
    public record Query : IRequest<List<Ip>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Ip>>
    {
        public async Task<List<Ip>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await unitOfWork.Devices.GetIps(cancellationToken);
        }
    }
}
