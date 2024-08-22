namespace CloudService.Domain;

public class Rom
{
    public Guid RomId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    public static Rom[] GetRoms()
    {
        return
        [
            new Rom { Name = "100 GB SSD", Price = 10.00M },
            new Rom { Name = "200 GB SSD", Price = 20.00M },
            new Rom { Name = "500 GB SSD", Price = 50.00M },
            new Rom { Name = "1 TB SSD", Price = 100.00M }
        ];
    }
}
