namespace ReportService.Domain;

public class FinancialSummary
{
    public int FinancialSummaryId { get; set; }
    public decimal Revenue { get; set; } 
    public decimal Expenses { get; set; }
    public decimal Profit => Revenue - Expenses;
}