using App.Metrics.Logging;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Mediator.Api.ConfigHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mediator.Api.Dependencies
{
    public static class CouchbaseConfiguration
    {
        public static IServiceCollection AddCouchbaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var couchbaseConfig = new CouchbaseConfig();
            configuration.GetSection("CouchbaseConfig").Bind(couchbaseConfig);
            var sp = services.BuildServiceProvider();
            var logging = sp.GetService<ILoggerFactory>();
            
            services.AddCouchbase(opt =>
            {                
                opt.WithConnectionString(couchbaseConfig.Url)
                    .WithCredentials(couchbaseConfig.Username, couchbaseConfig.Password)
                    .WithBuckets(couchbaseConfig.Bucket)
                    .WithLogging(logging);
            });
            services.AddDistributedCouchbaseCache(couchbaseConfig.Bucket, opt => {});
            return services;
        }
    }
}