using L04.Database;
using L04.Models;

namespace L04.Services;

public class TicketService(SimpleDatabase database, PaymentService paymentService)
{
    private readonly SimpleDatabase _database = database;
    private readonly PaymentService _paymentService = paymentService;

    internal IEnumerable<Ticket> GetTickets(Guid customerId, DateOnly date)
    {
        return _database.Tickets.Where(x => x.CustomerId == customerId && x.Date == date);
    }

    public bool BuyTicket(Guid customerId, string cardNumber)
    {
        Ticket ticket = new (){
            Date = DateOnly.FromDateTime(DateTime.Now),
            QrCode = Guid.NewGuid().ToString(),
            IsValid = true
        };
        
        var price = ticket.Price;
        var isPaid = _paymentService.Transact(ticket.Id, cardNumber, price);
        if (!isPaid)
        {
            return false;
        }

        ticket.CustomerId = customerId;
        _database.Create(ticket);
        return true;
    }

    public bool CheckTicket(string qrCode)
    {
        foreach (var item in _database.Tickets)
        {
            if (item.QrCode.Equals(qrCode, StringComparison.OrdinalIgnoreCase) && item.IsValid)
            {
                item.IsValid = false;
                _database.Update(item);
                return true;
            }
        }
        return false;
    }
}
