using L04.Models;
using L04.Services;

namespace L04.Apps;

public class MobileApp(TicketService ticketService, CustomerService customerService)
{
    public Customer Customer { get; set; } = new();
    public readonly TicketService _ticketService = ticketService;
    public readonly CustomerService _customerService = customerService;

    public bool Authorize(string login, string password)
    {
        var result = _customerService.GetCustomer(login, password);
        if (result is not null)
        {
            Customer = result;
            return true;
        }
        return false;
    }

    public IEnumerable<Ticket> GetTickets()
    {
        return Customer.Tickets;
    }

    public void SearchTickets(DateOnly date)
    {
        Customer.Tickets = _ticketService.GetTickets(Customer.CustomerId, date).ToList();
    }

    public bool Buy(string cardNumber)
    {
        return _ticketService.BuyTicket(Customer.CustomerId, cardNumber);
    }
}