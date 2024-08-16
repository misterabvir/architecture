namespace ReportService.Domain;

public class ReportTableRow
{
    public int RowId { get; set; }
    public int TableId { get; set; }
    public string RowData { get; set; } = string.Empty;
}
