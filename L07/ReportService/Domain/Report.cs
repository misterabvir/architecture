namespace ReportService.Domain;

public class Report
{
    public int ReportId { get; set; }
    public string Title { get; set; }= string.Empty;
    public DateTime CreatedDate { get; set; }
    public List<ReportTable> Tables { get; set; } =[];
    public List<Chart> Charts { get; set; } =[]; 
    public FinancialSummary Financials { get; set; } = null!;
}
