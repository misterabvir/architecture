using L04.Apps;
using L04.Database;
using L04.Services;

namespace L04;

public class AppBuilder
{
    public CustomerService CustomerService { get; }
    public TicketService TicketService { get; }
    public PaymentService PaymentService { get; }
    public SimpleDatabase Database { get; }

    public AppBuilder()
    {
        Database = new SimpleDatabase();
        PaymentService = new PaymentService();
        TicketService = new TicketService(Database, PaymentService);
        CustomerService = new CustomerService(Database);
    }

    public MobileApp CreateMobileApp()
    {
        return new MobileApp(TicketService, CustomerService);
    }

    public BusStation CreateBusStation()
    {
        return new BusStation(TicketService);
    }
}