using MediatR;

namespace Mediator.Core.Account.Commands
{
    public record CreateAccountCommand(string UserName, string Password) : IRequest<Models.Account>;
}
