using Mediator.Infrastructure.Connections.Contracts;
using System;
using System.Data;

namespace Mediator.Infrastructure.Connections
{
    public class ConnectionFactory<TConnection> : IConnectionFactory where TConnection : IDbConnection, new()
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentException(nameof(connectionString));
        }

        public IDbConnection CreateOpenConnection()
        {
            var connection = new TConnection { ConnectionString = _connectionString };

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new Exception("Error when opening connection to Database");
            }
            return connection;
        }
    }
}
