namespace L04.Models;
public class Customer {
    public Guid CustomerId { get; private set; } = Guid.NewGuid();
    public List<Ticket> Tickets { get; set; } = [];
    public string Password { get; internal set; } = string.Empty;
    public string Login { get; internal set; } = string.Empty;
}
