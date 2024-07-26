using Implementation.ModelElements.Primitives;
namespace Implementation.ModelElements;

public class PolygonalModel
{
    private readonly List<Polygonal> _polygons = [];
    private readonly List<Texture> _textures = [];

    public IReadOnlyList<Polygonal> Polygons => _polygons.AsReadOnly();
    public IReadOnlyList<Texture> Textures => _textures.AsReadOnly();

    public PolygonalModel(Polygonal polygonal)
    {
        _polygons.Add(polygonal);
    }

    public void AddTexture(Texture texture)
    {
        _textures.Add(texture);
    }

    public void AddPolygonal(Polygonal polygonal)
    {
        _polygons.Add(polygonal);
    }
}    