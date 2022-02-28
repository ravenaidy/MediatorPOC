using Mediator.Shared.AutoMapperExtentions.Contracts;
using MediatR;

namespace Mediator.Core.Account.Commands
{
    public record CreateAccountCommand : IRequest, IMapTo<Models.Account>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
