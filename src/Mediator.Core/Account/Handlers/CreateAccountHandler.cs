using AutoMapper;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Core.Account.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Models.Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository ?? throw new ArgumentException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Models.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<Models.Account>(request);
            return await _accountRepository.AddAccount(account);
        }
    }
}
