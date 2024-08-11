namespace DataAccessLayer.Entities;

public class Project : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<Setting> Settings { get; set; } = [];
    public List<Model> Models { get; set; } = [];

}
