using System.Threading.Tasks;

namespace Mediator.Infrastructure.Repositories.Cache.Contracts
{
    public interface ICache
    {
        Task<T> Get<T>(string cacheKey);
        Task Set<T>(string cacheKey, T response, int timeToLive = 10) where T : new();
    }
}