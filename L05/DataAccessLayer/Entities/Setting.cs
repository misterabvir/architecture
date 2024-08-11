namespace DataAccessLayer.Entities;

public class Setting : Entity
{
   public int ProjectId { get; set; }
   public  string Parameter { get; set; } = string.Empty;
   public  string Value { get; set; } = string.Empty;
}
