using L04.Database;
using L04.Models;

namespace L04.Services;

public class CustomerService(SimpleDatabase database)
{
    private readonly SimpleDatabase _database = database;

    internal Customer? GetCustomer(string login, string password)
    {
        return _database.Customers.FirstOrDefault(x => x.Login == login && x.Password == password);
    }
}