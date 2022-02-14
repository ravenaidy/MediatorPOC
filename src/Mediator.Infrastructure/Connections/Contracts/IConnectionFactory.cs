using System.Data;

namespace Mediator.Infrastructure.Connections.Contracts
{
    public interface IConnectionFactory
    {
        IDbConnection CreateOpenConnection();
    }
}
