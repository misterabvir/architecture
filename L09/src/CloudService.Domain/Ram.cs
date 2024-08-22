namespace CloudService.Domain;

public class Ram
{
    public Guid RamId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public static Ram[] GetRams()
    {
        return
        [
            new Ram { Name = "2 GB RAM", Price = 10.00M },
            new Ram { Name = "4 GB RAM", Price = 20.00M },
            new Ram { Name = "8 GB RAM", Price = 40.00M },
            new Ram { Name = "16 GB RAM", Price = 80.00M }
        ];
    }
}
