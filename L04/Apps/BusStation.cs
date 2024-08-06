using L04.Services;

namespace L04.Apps;

public class BusStation(TicketService ticketService)
{
    private readonly TicketService _ticketService = ticketService;

    public bool CheckTicket(string qrCode)
    {
        return _ticketService.CheckTicket(qrCode);
    }
}