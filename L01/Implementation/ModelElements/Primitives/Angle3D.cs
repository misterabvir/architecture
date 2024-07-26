namespace Implementation.ModelElements.Primitives;

public class Angle3D
{
    private const double MaxValue = 360;
    private double _x, _y, _z;

    public double X { get => _x; set => _x = Normalize(value); }
    public double Y { get => _y; set => _y = Normalize(value); }
    public double Z { get => _z; set => _z = Normalize(value); }

    private double Normalize(double value) => value % MaxValue;

    public static readonly Angle3D Zero = new() { X = 0, Y = 0, Z = 0 };
}