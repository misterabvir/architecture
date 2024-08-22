namespace CloudService.Domain;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Username { get; set; }
    public required string Password { get; set; }
    public List<Setup> Setups { get; set; } = [];
}
