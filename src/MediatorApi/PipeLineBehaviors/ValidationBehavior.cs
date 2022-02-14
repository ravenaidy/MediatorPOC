using FluentValidation;
using Mediator.Api.DTO;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Api.PipeLineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ApiResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new System.ArgumentNullException(nameof(validators));
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(val => val.Validate(context))
                .SelectMany(err => err.Errors)
                .Where(err => err != null)
                .ToList();

            if (failures.Any())
            {
                return Task.FromResult(
                    new TResponse 
                    {
                        ErrorMessage = failures.First().ErrorMessage, StatusCode = HttpStatusCode.BadRequest 
                    });
            }

            return next();
        }
    }
}
