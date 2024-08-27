using RemoteCleaner.Server.Domain;

namespace RemoteCleaner.Server.Application;

public class RobotRemote : IRobotRemote
{
    private Robot _robot = new();

    public Action<double>? OnChargeChanged { get; set; }
    public Action<double, string>? OnProgressChanged { get; set; }

    public double ChargeValue => _robot.Charge;

    public async Task MoveASync(double distance, string message)
    {
        _robot.State = State.Going;
        double path = 0.0;

        while (_robot.Charge > 0 && distance > path)
        {
            await Task.Delay(1000);
            path += Robot.MovingSpeed;
            OnChargeChanged?.Invoke(_robot.Charge);
            OnProgressChanged?.Invoke(path / distance, message);
        }
    }

    public async Task CleanAsync(double squares, string message)
    {
        double cleanedSquares = 0.0;
        _robot.State = State.Cleaning;
        while (_robot.Charge > 0 && squares > cleanedSquares)
        {
            await Task.Delay(1000);
            cleanedSquares += Robot.CleaningSpeed;
            OnChargeChanged?.Invoke(_robot.Charge);
            OnProgressChanged?.Invoke(cleanedSquares / squares, message);
        }
    }

    public async Task Charging(string message)
    {

        _robot.State = State.InBase;
        while (_robot.Charge < 100)
        {
            await Task.Delay(1000);
            OnChargeChanged?.Invoke(_robot.Charge);
            OnProgressChanged?.Invoke(_robot.Charge, message);
        }
        OnChargeChanged?.Invoke(_robot.Charge);
    }
}
