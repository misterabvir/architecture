using Implementation.ModelElements.Primitives;

namespace Implementation.ModelElements;

public class Camera
{
    public Point3D Location {get; private set;} = Point3D.Zero;
    public Angle3D Angle {get; private set;} =  Angle3D.Zero;

    public void Rotate(Angle3D angle) => Angle = angle;
    public void Move(Point3D point) => Location = point;
}