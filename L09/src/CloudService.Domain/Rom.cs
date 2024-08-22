namespace CloudService.Domain;

public class Rom
{
    public Guid RomId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
