using Microsoft.Extensions.Caching.Memory;
using ReportService.Domain;

namespace ReportService.Persistence.Services;

public class DataCacheService(DataService decorated, IMemoryCache cache) : IDataService
{
    private readonly TimeSpan _expirationTime = TimeSpan.FromHours(1);
    private const string CompaniesCacheKey = "companies";
    private const string CompanyCacheKeyPrefix = "company_";
    
    public async Task<IEnumerable<Company>> GetCompanies()
    {
        return (await cache!.GetOrCreateAsync(CompaniesCacheKey, 
            async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _expirationTime;
            return await decorated.GetCompanies();
        }))!;
    }

    public async Task<Company?> GetReport(int companyId)
    {
       return await cache.GetOrCreateAsync($"{CompanyCacheKeyPrefix}{companyId}",
            async entry =>
        {
            entry!.AbsoluteExpirationRelativeToNow = _expirationTime;
            return await decorated.GetReport(companyId);
        });
    }
}
