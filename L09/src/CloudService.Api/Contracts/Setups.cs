using CloudService.Domain;

namespace CloudService.Api.Contracts;

public static class Setups
{
    public static Application.Setups.Commands.Add.Command ToCommand(this Requests.Create request, Guid userId) =>
    new(userId, request.CpuId, request.RamId, request.RomId, request.IpId, request.OsId);

    public static Application.Setups.Commands.Update.Command ToCommand(this Requests.Update request, Guid userId) =>
        new(userId, request.SetupId, request.CpuId, request.RamId, request.RomId, request.IpId, request.OsId);

    public static Application.Setups.Commands.Remove.Command ToCommand(this Requests.Remove request, Guid userId) =>
        new(userId, request.SetupId);

    public static Application.Setups.Commands.Run.Command ToCommand(this Requests.Run request, Guid userId) =>
        new(userId, request.SetupId);

    public static Application.Setups.Commands.Stop.Command ToCommand(this Requests.Stop request, Guid userId) =>
        new(userId, request.SetupId);

    public static Responses.Device FromDomain(this Cpu cpu) => new(cpu.Name, cpu.Price);
    public static Responses.Device FromDomain(this Ram ram) => new(ram.Name, ram.Price);
    public static Responses.Device FromDomain(this Rom rom) => new(rom.Name, rom.Price);
    public static Responses.Device FromDomain(this Ip ip) => new(ip.Name, ip.Price);
    public static Responses.Device FromDomain(this Os os) => new(os.Name, os.Price);

    public static Responses.Setup FromDomain(this Setup configuration)
    {
        var devices = new List<Responses.Device>
            {
                FromDomain(configuration.Cpu),
                FromDomain(configuration.Ram),
                FromDomain(configuration.Rom),
                FromDomain(configuration.Ip),
                FromDomain(configuration.Os)
            };

        return new Responses.Setup(
            configuration.SetupId,
            devices,
            devices.Select(s => s.Price).Sum(),
            configuration.Status.Value,
            configuration.CreatedAt.ToLocalTime(),
            configuration.UpdatedAt.ToLocalTime());
    }

    public static List<Responses.Setup> FromDomain(this IEnumerable<Setup> configurations) =>
        configurations.Select(FromDomain).ToList();


    public static class Requests
    {
        public record Create(Guid CpuId, Guid RamId, Guid RomId, Guid IpId, Guid OsId);
        public record Update(Guid SetupId, Guid CpuId, Guid RamId, Guid RomId, Guid IpId, Guid OsId);
        public record Remove(Guid SetupId);
        public record Run(Guid SetupId);
        public record Stop(Guid SetupId);
    }

    public static class Responses
    {
        public record Setup(Guid SetupId, List<Device> Devices, decimal TotalPrice, string Status, DateTime CreatedAt, DateTime UpdatedAt);
        public record Device(string Name, decimal Price);
    }
}