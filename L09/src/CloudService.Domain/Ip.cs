namespace CloudService.Domain;

public class Ip
{
    public Guid IpId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
