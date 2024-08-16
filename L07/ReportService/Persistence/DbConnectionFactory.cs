using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace ReportService.Persistence;

public class DbConnectionFactory
{
    public async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = new SqliteConnection("DataSource=Persistence/DB/database.db");
        await connection.OpenAsync();
        return connection;
    } 
}