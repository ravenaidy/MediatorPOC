using Mediator.Core.Account.Contracts;
using Mediator.Infrastructure.Connections.Contracts;
using Mediator.Infrastructure.Repositories.Dapper;
using System.Threading.Tasks;

namespace Mediator.Infrastructure.Repositories.Account
{
    public class AccountRepository : DapperBaseRepository, IAccountRepository
    {
        public AccountRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<Core.Models.Account> AddAccount(Core.Models.Account account)
        {
            var parameters = new { account.UserName, account.Password };
            var spName = "pr_AddAccount";
            return await ExecuteScalarAsync<Core.Models.Account>(spName, parameters);
        }

        public Task<Core.Models.Account> GetAccount(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserNameExists(string userName)
        {
            var parameters = new { UserName = userName};
            var spName = "pr_AccountExists";
            return await QueryFirstOrDefaultAsync<Core.Models.Account>(spName, parameters) != null;
        }
    }
}
