using Mediator.Core.Account.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Core.Account.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Models.Account>
    {
        public Task<Models.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
