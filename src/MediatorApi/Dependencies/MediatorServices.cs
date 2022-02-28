using System.Data.SqlClient;
using Mediator.Core.Account.Contracts;
using Mediator.Infrastructure.Connections;
using Mediator.Infrastructure.Connections.Contracts;
using Mediator.Infrastructure.Repositories.Account;
using Mediator.Infrastructure.Repositories.Cache.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CouchbaseCache = Mediator.Infrastructure.Repositories.Cache.CouchbaseCache;

namespace Mediator.Api.Dependencies
{
    public static class MediatorServices
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSingleton<IConnectionFactory>(sp =>
                new ConnectionFactory<SqlConnection>(configuration.GetConnectionString("Mediator")));
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<ICache, CouchbaseCache>();
            return services;
        }
    }
}
