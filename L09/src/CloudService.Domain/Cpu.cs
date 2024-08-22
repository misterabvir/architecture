namespace CloudService.Domain;

public class Cpu
{
    public Guid CpuId { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal Price { get; set; }


}
