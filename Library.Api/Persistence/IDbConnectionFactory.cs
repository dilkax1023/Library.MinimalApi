using System.Data;

namespace Library.Api.Persistence;

public interface IDbConnectionFactory
{
     Task<IDbConnection> CreateConnectionAsync();
}