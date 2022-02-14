using Mediator.Core.Account.Commands;
using Mediator.Shared.AutoMapperExtentions.Contracts;

namespace Mediator.Api.AccountEndPoints
{
    public record CreateAccountRequest : IMapTo<CreateAccountCommand>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
