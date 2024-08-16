using Dapper;
using ReportService.Domain;
using System.Data.Common;

namespace ReportService.Persistence.Services;

public class DataService(DbConnectionFactory factory) : IDataService
{
    public async Task<IEnumerable<Company>> GetCompanies()
    {

        using var connection = await factory.CreateConnectionAsync();
        var companies = await connection.QueryAsync<Company>("SELECT * FROM Company");
        return companies;
    }


    public async Task<Company?> GetReport(int companyId)
    {

        using var connection = await factory.CreateConnectionAsync();

        Company? company = await GetInfo(companyId, connection);

        if (company != null)
        {
            await GetProducts(companyId, connection, company);
            await GetReports(companyId, connection, company);
        }
        return company;
    }

    private static async Task GetReports(int companyId, DbConnection connection, Company company)
    {
        string reportsSql = @"
                    SELECT 
                        r.ReportId, r.Title, r.CreatedDate,
                        rt.TableId, rt.Title,
                        rtr.RowId, rtr.RowData,
                        ch.ChartId, ch.Title, ch.Type, ch.Labels, ch.'Values',
                        fs.SummaryId, fs.Revenue, fs.Expenses
                    FROM 
                        Report r
                    LEFT JOIN 
                        ReportTable rt ON r.ReportId = rt.ReportId
                    LEFT JOIN 
                        ReportTableRow rtr ON rt.TableId = rtr.TableId
                    LEFT JOIN 
                        Chart ch ON r.ReportId = ch.ReportId
                    LEFT JOIN 
                        FinancialSummary fs ON r.ReportId = fs.ReportId
                    WHERE 
                        r.CompanyId = @CompanyId;
                ";

        var reportDict = new Dictionary<int, Report>();

        await connection.QueryAsync<Report, ReportTable, ReportTableRow, Chart, FinancialSummary, Report>(
            reportsSql,
            (report, reportTable, reportTableRow, chart, financialSummary) =>
            {
                if (!reportDict.TryGetValue(report.ReportId, out var currentReport))
                {
                    currentReport = report;
                    currentReport.Financials = financialSummary;
                    reportDict.Add(currentReport.ReportId, currentReport);
                }

                if (reportTable != null && !currentReport.Tables.Any(t => t.TableId == reportTable.TableId))
                {
                    reportTable.Rows = [];
                    currentReport.Tables.Add(reportTable);
                }

                if (reportTableRow != null)
                {
                    var tbl = currentReport.Tables.First(t => t.TableId == reportTable?.TableId);
                    tbl.Rows.Add(reportTableRow);
                }


                if (!currentReport.Charts.Any(c => c.ChartId == chart.ChartId))
                {
                    currentReport.Charts.Add(chart);
                }

                return currentReport;
            },
            new { CompanyId = companyId },
            splitOn: "TableId,RowId,ChartId,SummaryId"
        );

        company.Reports = reportDict.Values.ToList();
    }

    private static async Task GetProducts(int companyId, DbConnection connection, Company company)
    {
        string categoriesSql = @"
                    SELECT 
                        pc.CategoryId, pc.Name, p.ProductId, p.Name, p.Price, p.Quantity, p.CategoryId
                    FROM 
                        ProductCategory pc
                    JOIN Product p ON p.CategoryId = pc.CategoryId
                    WHERE 
                        pc.CompanyId = @CompanyId;
                ";

        List<ProductCategory> productCategories = [];

        await connection.QueryAsync<ProductCategory, Product, ProductCategory>(categoriesSql,
            (category, product) =>
        {
            var exist = productCategories.FirstOrDefault(c => c.CategoryId == category.CategoryId);

            if (exist is null)
            {
                exist = category;
                productCategories.Add(exist);
            }
            exist.Products.Add(product);
            return exist;
        }, new { CompanyId = companyId }, splitOn: "CategoryId,ProductId");


        company.Categories = productCategories;
    }

    private static async Task<Company?> GetInfo(int companyId, DbConnection connection)
    {
        string companySql = @"
                SELECT 
                    CompanyId, Name, Address, TaxId
                FROM 
                    Company
                WHERE 
                    CompanyId = @CompanyId;
            ";
        var company = await connection.QuerySingleOrDefaultAsync<Company>(companySql, new { CompanyId = companyId });
        return company;
    }
}