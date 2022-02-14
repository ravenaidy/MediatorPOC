using Mediator.Shared.AutoMapperExtentions.Contracts;
using MediatR;

namespace Mediator.Core.Account.Commands
{
    public record CreateAccountCommand : IRequest<Models.Account>, IMapTo<Models.Account>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
