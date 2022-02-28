using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mediator.Core.Account.Contracts;
using Mediator.Core.Account.Queries;
using MediatR;

namespace Mediator.Core.Account.Handlers
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<Models.Account>>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAllAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        public async Task<List<Models.Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAllAccount();
        }
    }
}