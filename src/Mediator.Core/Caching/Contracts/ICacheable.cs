namespace Mediator.Core.Caching.Contracts
{
    public interface ICacheable
    {
        public string CacheKey { get; set; }
    }
}