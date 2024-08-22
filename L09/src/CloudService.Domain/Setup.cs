namespace CloudService.Domain;

public class Setup
{
    public Guid SetupId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid CpuId { get; set; } = Guid.NewGuid();
    public Cpu Cpu { get; set; } = null!;
    public Guid RamId { get; set; } = Guid.NewGuid();
    public Ram Ram { get; set; } = null!;
    public Guid RomId { get; set; } = Guid.NewGuid();
    public Rom Rom { get; set; } = null!;
    public Guid IpId { get; set; } = Guid.NewGuid();
    public Ip Ip { get; set; } = null!;
    public Guid OsId { get; set; } = Guid.NewGuid();
    public Os Os { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; } = Status.Offline;
    public void Update(Status status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(Cpu cpu)
    {
        if (CpuId != cpu.CpuId)
        {
            CpuId = cpu.CpuId;
            UpdatedAt = DateTime.UtcNow;
        }

    }

    public void Update(Ram ram)
    {
        if (RamId != ram.RamId)
        {
            RamId = ram.RamId;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Update(Rom rom)
    {
        if (RomId != rom.RomId)
        {
            RomId = rom.RomId;
            UpdatedAt = DateTime.UtcNow;
        }

    }

    public void Update(Ip ip)
    {
        if (IpId != ip.IpId)
        {
            IpId = ip.IpId;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Update(Os os)
    {
        if (OsId != os.OsId)
        {
            OsId = os.OsId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
