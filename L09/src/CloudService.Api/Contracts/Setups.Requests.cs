namespace CloudService.Api.Contracts;

public static partial class Setups
{
    public static class Requests
    {
        public record Create(Guid CpuId, Guid RamId, Guid RomId, Guid IpId, Guid OsId);
        public record Update(Guid SetupId, Guid? CpuId=null, Guid? RamId = null, Guid? RomId = null, Guid? IpId = null, Guid? OsId = null);
        public record Remove(Guid SetupId);
        public record Run(Guid SetupId);
        public record Stop(Guid SetupId);
    }
}