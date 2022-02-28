using System.Threading;
using System.Threading.Tasks;
using Mediator.Core.Caching.Contracts;
using Mediator.Infrastructure.Repositories.Cache.Contracts;
using MediatR;

namespace Mediator.Api.PipeLineBehaviors
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>, ICacheable
        where TResponse: new()
    {
        private readonly ICache _cache;

        public CacheBehavior(ICache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await _cache.Get<TResponse>(request.CacheKey);

            if (response != null) return response;
            
            response = await next();

            if (response != null)
            {
                await _cache.Set(request.CacheKey, response);
            }
            return response;
        }
    }
}