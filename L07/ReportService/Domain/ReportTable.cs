namespace ReportService.Domain;

public class ReportTable
{
    public int TableId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Columns { get; set; }= [];
    public List<ReportTableRow> Rows { get; set; } = [];
}
