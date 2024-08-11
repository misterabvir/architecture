namespace DataAccessLayer.Entities;

public class Texture : Entity
{
    public int ModelId { get; set; }
    public string Pattern { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}