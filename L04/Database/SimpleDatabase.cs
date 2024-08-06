using L04.Models;

namespace L04.Database;

public class SimpleDatabase
{
    public List<Customer> Customers { get; } = 
    [
        new Customer()
        {
            Login = "login",
            Password = "password"
        }
    ];
    public List<Ticket> Tickets { get; } = [];

    public void Update(Ticket ticket)
    {
        var ticketToUpdate = Tickets.FirstOrDefault(t => t.Id == ticket.Id);
        if (ticketToUpdate is not null)
        {
            Tickets.Remove(ticketToUpdate);
            Tickets.Add(ticket);
        }
    }
    public void Create(Ticket ticket) => Tickets.Add(ticket);
}
