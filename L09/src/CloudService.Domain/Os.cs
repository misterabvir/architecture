namespace CloudService.Domain;

public class Os
{
    public Guid OsId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
