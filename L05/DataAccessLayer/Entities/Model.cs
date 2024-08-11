namespace DataAccessLayer.Entities;

public class Model : Entity
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Texture> Textures { get; set; } = [];
}
