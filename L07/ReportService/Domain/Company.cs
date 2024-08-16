namespace ReportService.Domain;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; }= string.Empty;
    public string Address { get; set; }= string.Empty;
    public string TaxId { get; set; }= string.Empty;

    public List<ProductCategory> Categories { get; set; } = [];
    public List<Report> Reports { get; set; } = [];

}
