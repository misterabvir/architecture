namespace CloudService.Domain;

public class Os
{
    public Guid OsId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    public static Os[] GetOs()
    {
        return
        [
            new Os { Name = "Linux Ubuntu", Price = 0.00M },
            new Os { Name = "Windows 10", Price = 30.00M },
            new Os { Name = "Windows Server", Price = 50.00M },
            new Os { Name = "Red Hat Enterprise Linux", Price = 25.00M }
        ];
    }
}
