using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using MediatR;

namespace CloudService.Application.Devices.Queries;

public static class AllRom
{
    public record Query : IRequest<List<Rom>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, List<Rom>>
    {
        public async Task<List<Rom>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await unitOfWork.Devices.GetRoms(cancellationToken);
        }
    }
}
