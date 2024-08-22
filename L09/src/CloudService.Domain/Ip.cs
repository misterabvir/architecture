namespace CloudService.Domain;

public class Ip
{
    public Guid IpId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public static Ip[] GetIps()
    {
        return
        [
            new Ip { Name = "Dynamic IP", Price = 5.00M },
            new Ip { Name = "Static IP", Price = 15.00M }
        ];
    }
}
