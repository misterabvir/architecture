namespace ReportService.Domain;

public class Chart
{
    public int ChartId { get; set; }    
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Labels { get; set; } = string.Empty;
    public string Values { get; set; } = string.Empty;
}
