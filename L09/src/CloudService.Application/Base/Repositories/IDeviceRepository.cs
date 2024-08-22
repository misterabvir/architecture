using CloudService.Domain;

namespace CloudService.Application.Base.Repositories;

public interface IDeviceRepository{
    Task<List<Cpu>> GetCpus(CancellationToken cancellationToken = default);
    Task<List<Ram>> GetRams(CancellationToken cancellationToken = default);
    Task<List<Rom>> GetRoms(CancellationToken cancellationToken = default);
    Task<List<Ip>> GetIps(CancellationToken cancellationToken = default);
    Task<List<Os>> GetOss(CancellationToken cancellationToken = default);

    Task<Cpu?> GetCpuById(Guid id, CancellationToken cancellationToken = default);
    Task<Ram?> GetRamById(Guid id, CancellationToken cancellationToken = default);
    Task<Rom?> GetRomById(Guid id, CancellationToken cancellationToken = default);
    Task<Ip?> GetIpById(Guid id, CancellationToken cancellationToken = default);
    Task<Os?> GetOsById(Guid id, CancellationToken cancellationToken = default);
}