namespace Implementation.ModelElements.Primitives;

public record Texture(string value)
{
    public static readonly Texture Solid = new("solid");
    public static readonly Texture Checker = new("checker");
    public static readonly Texture Gradient = new("gradient");
}