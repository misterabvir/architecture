using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using MediatR;

namespace CloudService.Application.Devices.Queries;

public static class AllCpu
{
    public record Query : IRequest<List<Cpu>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Cpu>>
    {
        public async Task<List<Cpu>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await unitOfWork.Devices.GetCpus(cancellationToken);
        }
    }
}
