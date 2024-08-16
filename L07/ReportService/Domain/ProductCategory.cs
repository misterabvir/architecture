namespace ReportService.Domain;

public class ProductCategory
{
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
}
