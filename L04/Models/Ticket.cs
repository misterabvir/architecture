namespace L04.Models;
public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public DateOnly Date { get; set; }
    public string QrCode { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;
    public decimal Price { get; } = Random.Shared.Next(50, 100);
}