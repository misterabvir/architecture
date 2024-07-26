using Implementation.ModelElements.Primitives;

namespace Implementation.ModelElements;

public class Flash
{
    public Point3D Location {get; private set;} = Point3D.Zero;
    public Angle3D Angle {get; private set;} =  Angle3D.Zero;

    public Color Color {get; private set;}
    public double Power {get; private set;}

    public Flash(Color color, double power)
    {
        Color = color;
        Power = power;
    }

    public void Rotate(Angle3D angle) => Angle = angle;
    public void Move(Point3D point) => Location = point;
    public void ChangeColor(Color color) => Color = color;
    public void ChangePower(double power) => Power = power;
}