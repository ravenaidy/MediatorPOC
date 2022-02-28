using System;
using System.Text.Json;
using System.Threading.Tasks;
using Mediator.Infrastructure.Repositories.Cache.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace Mediator.Infrastructure.Repositories.Cache
{
    public class CouchbaseCache : ICache
    {
        private readonly IDistributedCache _distributedCache;

        public CouchbaseCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> Get<T>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentException($"{nameof(cacheKey)} cannot be null or empty");
            return JsonSerializer.Deserialize<T>(await _distributedCache.GetStringAsync(cacheKey).ConfigureAwait(false));
        }

        public async Task Set<T>(string cacheKey, T response, int timeToLive = 10) where T : new()
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentException($"{nameof(cacheKey)} cannot be null or empty");
            if (response == null)
                throw new ArgumentException($"{nameof(response)} cannot be null");
            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(0, timeToLive, 0)
            });
        }
    }
}