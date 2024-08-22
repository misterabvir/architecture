namespace CloudService.Api.Contracts;

public static partial class Setups
{
    public static class Responses
    {
        public record Setup(Guid SetupId, List<Device> Devices, decimal TotalPrice, string Status, DateTime CreatedAt, DateTime UpdatedAt);
        public record Device(string Name, decimal Price);
    }
}