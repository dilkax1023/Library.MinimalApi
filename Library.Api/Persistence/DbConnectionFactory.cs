using System.Data;
using Library.Api.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Library.Api.Persistence;

public class DbConnectionFactory(IOptions<ConnectionStrings> options) : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateConnectionAsync()
    {
       // Create a connection to the sqlite database
       var connection = new SqliteConnection(options.Value.DefaultConnection);
       await connection.OpenAsync(); 
         return connection;
    }
}