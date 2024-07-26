namespace Implementation.ModelElements.Primitives;

public record Color(string value)
{
    public static readonly Color White = new("FFFFFF");
    public static readonly Color Black = new("000000");
    public static readonly Color Red = new("FF0000");
    public static readonly Color Green = new("00FF00");
    public static readonly Color Blue = new("0000FF");
    public static readonly Color Yellow = new("FFFF00");
    public static readonly Color Purple = new("FF00FF");
    public static readonly Color Cyan = new("00FFFF");
}









