namespace RemoteCleaner.Server.Domain;


public class Robot
{
    public const double MovingSpeed = 5;
    public const double CleaningSpeed = 1000;
    private const int _batteryCapacity = 100;
    private const double _unChargingSpeed = 0.5;
    private const double _chargingSpeed = 5;

    public State State { get; set; } = State.InBase;
    public int X { get; set; }
    public int Y { get; set; }
    public double Progress { get; set; } = 0;

    private double _charge = 100;

    public double Charge
    {
        get
        {
            if (State.Equals(State.InBase) && _charge < _batteryCapacity)
            {
                _charge += _chargingSpeed;
            }
            else if (!State.Equals(State.InBase) && _charge > 0)
            {
                _charge -= _unChargingSpeed;
            }
            if (_charge < 0) _charge = 0;
            if (_charge > 100) _charge = 100;

            return _charge;
        }
    }
}

