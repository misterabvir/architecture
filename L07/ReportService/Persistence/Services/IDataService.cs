using ReportService.Domain;

namespace ReportService.Persistence.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company?> GetReport(int companyId);
    }
}