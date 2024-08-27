

namespace RemoteCleaner.Server.Application
{
    public interface IRobotRemote
    {
        Action<double>? OnChargeChanged { get; set; }
        Action<double, string>? OnProgressChanged { get; set; }
        double ChargeValue { get; }

        Task Charging(string message);
        Task CleanAsync(double squares, string message);
        Task MoveASync(double distance, string message);
    }
}