namespace Implementation.ModelElements.Primitives;

public class Point3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public static readonly Point3D Zero = new() { X = 0, Y = 0, Z = 0 };
}