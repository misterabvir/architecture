using L04;

var app = new AppBuilder();

var mobile = app.CreateMobileApp();
mobile.Authorize("login", "password");

var station = app.CreateBusStation();

/**************************************************/

var orderResult = mobile.Buy("1111-1111-1111-1111");
if (!orderResult)
{
    Console.WriteLine("Order failed.");
    return;
}

mobile.SearchTickets(DateOnly.FromDateTime(DateTime.Now));

var tickets = mobile.GetTickets();
Console.WriteLine($"There are {tickets.Count()} ticket(s) purchased");

var check = station.CheckTicket(tickets.First().QrCode);
if (check)
{
    Console.WriteLine("The client gained access to the bus");
}
else
{
    Console.WriteLine("The client did not gain access to the bus");
}

// do it again should be failure
check = station.CheckTicket(tickets.First().QrCode);
if (check)
{
    Console.WriteLine("The client gained access to the bus");
}
else
{
    Console.WriteLine("The client did not gain access to the bus");
}


/* 
 + -------------------------------------------------------------------------------------+
 | OUTPUT                                      | RESULT                                 |
 + -------------------------------------------------------------------------------------+
 |  There are 1 ticket(s) purchased            | -> SUCCESS PAYMENT                     |
 |  The client gained access to the bus        | -> SUCCESS GAIN                        |
 |  The client did not gain access to the bus  | -> FAILURE WHEN TICKET IS ALREADY USED |
 + -------------------------------------------------------------------------------------+  
 |  Order failed.                              | -> FAILURE WHEN PAYMENT FAILED         |
 + -------------------------------------------------------------------------------------+
 */
