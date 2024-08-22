namespace CloudService.Domain;

public class Ram
{
    public Guid RamId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
