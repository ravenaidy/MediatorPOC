using Mediator.Core.Account.Contracts;
using Mediator.Infrastructure.Connections;
using Mediator.Infrastructure.Connections.Contracts;
using Mediator.Infrastructure.Repositories.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace Mediator.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory<SqlConnection>(configuration.GetConnectionString("Mediator")));
            services.AddSingleton<IAccountRepository, AccountRepository>();            
            return services;
        }
    }
}
