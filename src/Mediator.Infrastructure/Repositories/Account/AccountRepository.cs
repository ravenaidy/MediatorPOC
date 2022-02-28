using Mediator.Core.Account.Contracts;
using Mediator.Infrastructure.Connections.Contracts;
using Mediator.Infrastructure.Repositories.Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Infrastructure.Repositories.Account
{
    public class AccountRepository : DapperBaseRepository, IAccountRepository
    {
        public AccountRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task AddAccount(Core.Models.Account account)
        {
            var parameters = new {UserName = account.Username, account.Password };
            const string spName = "pr_CreateAccount";
            await ExecuteAsync(spName, parameters);
        }

        public async Task<Core.Models.Account> GetAccount(Core.Models.Account account)
        {
            var parameters = new {UserName = account.Username, account.Password };
            const string spName = "pr_GetAccount";
            return await QueryFirstOrDefaultAsync<Core.Models.Account>(spName, parameters);
        }

        public async Task<List<Core.Models.Account>> GetAllAccount()
        {
            const string spName = "pr_GetAllAccounts";
            return (await QueryAsync<Core.Models.Account>(spName)).ToList();
        }

        public async Task<bool> UserNameExists(string userName)
        {
            var parameters = new { UserName = userName};
            const string spName = "pr_AccountExists";
            return await QueryFirstOrDefaultAsync<int>(spName, parameters) > 0;
        }
    }
}
