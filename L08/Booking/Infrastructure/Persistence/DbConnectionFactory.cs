using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Booking.Infrastructure.Persistence;

public class DbConnectionFactory(string connectionString)
{
    
    public async Task<DbConnection> CreateConnection()
    {
        var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        return  connection;
    }    
}